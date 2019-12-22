using jda.Abstractions;
using jda.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace jda.tests
{
    [TestClass]
    public class FileAccessTests
    {
        private IFileAccessor _fileAccess;
        private string _baseDirectory;
        [TestInitialize]
        public async Task InitializeAsync()
        {
            _fileAccess = new FileAccessor(null);
            _baseDirectory = "D:\\jdaImageTest\\";
        }

        [TestMethod]
        public void Should_Check_Or_Create_Directory()
        {
            _fileAccess.CheckOrCreateDirectory(_baseDirectory);
            Assert.IsTrue(Directory.Exists(_baseDirectory));
        }

        public Task<string[]> Should_Get_Image_URL_List_From_Text_File(string filePath)
        {
            throw new NotImplementedException();
        }

        public void Should_Save_File_by_Given_Content(byte[] content, string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public void Should_Save_Image_List_With_Content(Dictionary<string, byte[]> resourceList)
        {
            throw new NotImplementedException();
        }
    }
}
