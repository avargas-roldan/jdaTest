using jda.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace jda.Operations
{
    public class ImageOperations : IImageOperations
    {
        private readonly HttpClient _httpClient;
        public ImageOperations(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<string> GetFileNameAsync(string uriImagePath)
        {
            if (!string.IsNullOrEmpty(uriImagePath)) {
                if (Uri.TryCreate(uriImagePath, UriKind.Absolute, out Uri imageUri))
                {
                    return Path.GetFileName(imageUri.LocalPath);
                }
            }
            return string.Empty;
        }

        public async Task<Dictionary<string, byte[]>> GetImagesAsync(string[] fullImagePathList)
        {
            if (fullImagePathList != null && fullImagePathList.Any()) {
                if (_httpClient != null) {
                    Dictionary<string, byte[]> images = new Dictionary<string, byte[]>();
                    foreach (string imageUrl in fullImagePathList) {
                        if (IsValidURLPath(imageUrl)) {
                            string fileName = await GetFileNameAsync(imageUrl);
                            if (!images.ContainsKey(fileName)) {
                                var resource = await _httpClient.GetByteArrayAsync(imageUrl);
                                if (!string.IsNullOrEmpty(fileName) && resource != null && resource.Length > 0) images.Add(fileName, resource);
                            }
                        }
                    }
                    if (images.Any()) return images;
                }
            }
            return null;
        }

        public bool IsValidURLPath(string uriImagePath)
        {
            if (!string.IsNullOrEmpty(uriImagePath)) {
                return Uri.TryCreate(uriImagePath, UriKind.Absolute, out Uri uriResult) && 
                       (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }
    }
}
