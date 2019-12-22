using System.Collections.Generic;
using System.Threading.Tasks;

namespace jda.Abstractions
{
    public interface IImageOperations
    {
        bool IsValidURLPath(string uriImagePath);
        Task<string> GetFileNameAsync(string uriImagePath);
        Task<Dictionary<string, byte[]>> GetImagesAsync(string[] fullImagePathList);
    }
}
