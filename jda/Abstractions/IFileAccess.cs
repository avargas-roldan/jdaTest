using System.Collections.Generic;
using System.Threading.Tasks;

namespace jda.Abstractions
{
    public interface IFileAccessor
    {
        Task<string[]> GetImageURIsAsync(string filePath);
        void SaveImageList(Dictionary<string, byte[]> resourceList);
        void SaveFile(byte[] content, string path, string fileName);
        void CheckOrCreateDirectory(string path);
    }
}
