using jda.Abstractions;
using jda.Operations;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace jda
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                IFileAccessor fileAccess = new FileAccessor(null);
                IImageOperations imageOps = new ImageOperations(httpClient);
                string imageListPath = "../../../../Downloads/";
                Console.WriteLine("Please, provide the file with url image list ( UNIT\\FOLDER\\FILE.TXT ): ");
                var file = Console.ReadLine();

                var imageUrlList = await fileAccess.GetImageURIsAsync(file);
                var imageContentList = await imageOps.GetImagesAsync(imageUrlList);
                fileAccess.SaveImageList(imageContentList, imageListPath);
                string directory = Path.GetFullPath(imageListPath);
                Console.WriteLine($"All images were saved on {directory}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops! Something went wrong, please check this out: {ex.Message}");
            }
            
        }
    }
}
