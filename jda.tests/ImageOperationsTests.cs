using jda.Abstractions;
using jda.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace jda.tests
{
    [TestClass]
    public class ImageOperationsTests
    {
        private IImageOperations _imageOps;
        private IFileAccessor _fileAccess;
        private HttpClient _httpClient;
        private string _URLTest;
        private string _baseDirectory;
        private string _fullImageListFile;
        private string _imageListWithSpaces;
        private string _emptyTextFile;
        private string _invalidURLMixed;
        private string _downloadFolder;

        [TestInitialize]
        public async Task InitializeAsync()
        {
            _httpClient = new HttpClient();
            _imageOps = new ImageOperations(_httpClient);
            _fileAccess = new FileAccessor(null);
            _URLTest = $"http://mywebserver.com/images/271947.jpg";

            _baseDirectory = "../../../../";
            _downloadFolder = $"{_baseDirectory}/Downloads";
            _fullImageListFile = $"{_baseDirectory}/Scenarios/FullImageList.txt";
            _imageListWithSpaces = $"{_baseDirectory}/Scenarios/EmptySpacesFileText.txt";
            _emptyTextFile = $"{_baseDirectory}/Scenarios/EmptyFileText.txt";
            _invalidURLMixed = $"{_baseDirectory}/Scenarios/InvalidURLList.txt";
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            
        }

        [TestMethod, TestCategory("jda.tests.imageops.filename")]
        public void Should_Get_File_Name_of_Given_URL()
        {
            var fileName = _imageOps.GetFileNameAsync(_URLTest).Result;
            Assert.IsTrue(fileName.Equals("271947.jpg"));
        }

        [TestMethod, TestCategory("jda.tests.imageops.validation")]
        public void Should_Detect_If_URL_Is_Valid()
        {
            Assert.IsTrue(_imageOps.IsValidURLPath(_URLTest));
            Assert.IsFalse(_imageOps.IsValidURLPath($"This is not a valid URL."));
        }

        private void TestImageDownloading(string imageListFile) {
            Assert.IsTrue(!string.IsNullOrEmpty(imageListFile));
            var imageSources = _fileAccess.GetImageURIsAsync(imageListFile).Result;
            Assert.IsTrue(imageSources != null && imageSources.Any());
            var images = _imageOps.GetImagesAsync(imageSources).Result;
            Assert.IsTrue(images != null && images.Any());
        }

        [TestMethod, TestCategory("jda.tests.imageops.fullimagelist")]
        public void Should_Get_Image_Resources_By_Given_URL_String_List() => TestImageDownloading(_fullImageListFile);

        [TestMethod, TestCategory("jda.tests.imageops.emptyspaceimagelist")]
        public void Should_Get_Image_Resources_Even_With_Empty_Spaces() => TestImageDownloading(_imageListWithSpaces);

        [TestMethod, TestCategory("jda.tests.imageops.emptyspaceimagelist")]
        public void Should_Not_Download_Resources_If_File_Is_Empty_Or_Invalid()
        {
            var imageSources = _fileAccess.GetImageURIsAsync(_emptyTextFile).Result;
            var images = _imageOps.GetImagesAsync(imageSources).Result;
            Assert.IsTrue(images == null || ! images.Any());

            imageSources = _fileAccess.GetImageURIsAsync(_invalidURLMixed).Result;
            images = _imageOps.GetImagesAsync(imageSources).Result;
            Assert.IsTrue(images == null || !images.Any());
        }
    }
}
