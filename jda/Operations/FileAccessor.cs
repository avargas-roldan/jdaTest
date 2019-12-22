using jda.Abstractions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace jda.Operations
{
    public class FileAccessor : IFileAccessor
    {
        ILogger _logger;
        public FileAccessor(ILogger logger) {
            _logger = logger; 
        }

        public async Task<string[]> GetImageURIsAsync(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath)) {
                string[] lines = await File.ReadAllLinesAsync(filePath);
                if (lines != null && lines.Any()) {
                    var validImages = lines.Where(imageUrl => !string.IsNullOrEmpty(imageUrl)).ToArray();
                    Tracker.LogInformation(_logger, $"Valid image lines: {validImages}");
                    return validImages;
                }
            }
            else Tracker.LogError(_logger, "Void file path");
            return null;
        }

        public void SaveImageList(Dictionary<string, byte[]> resourceList, string path)
        {
            if (resourceList != null && resourceList.Any()) {
                CheckOrCreateDirectory(path);
                foreach (var resource in resourceList)
                {
                    SaveFile(resource.Value, path, resource.Key);
                }
            }
        }

        public void SaveFile(byte[] content, string path, string fileName)
        {
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(fileName) && content != null && content.Length > 0) {
                File.WriteAllBytesAsync($"{path}\\{fileName}", content);
            }
        }

        public void CheckOrCreateDirectory(string path)
        {
            if (!string.IsNullOrEmpty(path)) {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            }
        }
    }
}
