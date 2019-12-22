using jda.Abstractions;
using jda.Operations;
using System;
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

                Console.WriteLine("Please, provide the file with url image list ( UNIT\\FOLDER\\FILE.TXT ): ");
                var file = Console.ReadLine();

                var imageUrlList = await fileAccess.GetImageURIsAsync(file);
                var imageContentList = await imageOps.GetImagesAsync(imageUrlList);
                fileAccess.SaveImageList(imageContentList);
                Console.WriteLine($"All images were saved on SOMEFILEPATH");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops! Something went wrong, please check this out: {ex.Message}");
            }
            
        }
    }
}
