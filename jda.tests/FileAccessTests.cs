using jda.Abstractions;
using jda.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace jda.tests
{
    [TestClass]
    public class FileAccessTests
    {
        private Random _random;
        private IFileAccessor _fileAccess;
        private string _baseDirectory;
        private string _creationDirectoryFolder;
        private string _fullImageTextFilePath;
        private string _saveFileTestPath;

        [TestInitialize]
        public async Task InitializeAsync()
        {
            _random = new Random();
            _fileAccess = new FileAccessor(null);
            _baseDirectory = "../../../../";
            _creationDirectoryFolder = $"{_baseDirectory}/jdaimagetestcreation";
            _fullImageTextFilePath = $"{_baseDirectory}/Scenarios/FullImageList.txt";
            _saveFileTestPath = $"{_baseDirectory}";
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            if (Directory.Exists(_creationDirectoryFolder)) Directory.Delete(_creationDirectoryFolder);
            if (File.Exists($"{_saveFileTestPath}/FileSavingTest.txt")) File.Delete($"{_saveFileTestPath}/FileSavingTest.txt");
            if (File.Exists($"{_saveFileTestPath}/ImageListFileTest1.txt")) File.Delete($"{_saveFileTestPath}/ImageListFileTest1.txt");
            if (File.Exists($"{_saveFileTestPath}/ImageListFileTest2.txt")) File.Delete($"{_saveFileTestPath}/ImageListFileTest2.txt");
            if (File.Exists($"{_saveFileTestPath}/ImageListFileTest3.txt")) File.Delete($"{_saveFileTestPath}/ImageListFileTest3.txt");
        }

        [TestMethod, TestCategory("jda.tests.fileaccess.directory")]
        public void Should_Check_Or_Create_Directory()
        {
            _fileAccess.CheckOrCreateDirectory(_creationDirectoryFolder);
            Assert.IsTrue(Directory.Exists(_creationDirectoryFolder));
        }

        [TestMethod, TestCategory("jda.tests.fileaccess.filereading")]
        public void Should_Get_Image_URL_List_From_Text_File()
        {
            var fileContent = _fileAccess.GetImageURIsAsync(_fullImageTextFilePath).Result;
            Assert.IsTrue(fileContent != null && fileContent.Any());
        }

        [TestMethod, TestCategory("jda.tests.fileaccess.filewritting")]
        public void Should_Save_File_by_Given_Content()
        {            
            byte[] _content = new byte[10];
            _random.NextBytes(_content);
            _fileAccess.SaveFile(_content, _saveFileTestPath, "FileSavingTest.txt");
            Assert.IsTrue(File.Exists($"{_saveFileTestPath}/FileSavingTest.txt"));
        }

        [TestMethod, TestCategory("jda.tests.fileaccess.filelistwritting")]
        public void Should_Save_Image_List_With_Content()
        {
            Dictionary<string, byte[]> _fakeImageList = new Dictionary<string, byte[]>();
            byte[] _content = new byte[10];
            _random.NextBytes(_content);
            _fakeImageList.Add("ImageListFileTest1.txt", _content);
            _fakeImageList.Add("ImageListFileTest2.txt", _content);
            _fakeImageList.Add("ImageListFileTest3.txt", _content);
            _fileAccess.SaveImageList(_fakeImageList, _saveFileTestPath);

            Assert.IsTrue(File.Exists($"{_saveFileTestPath}/ImageListFileTest1.txt"));
            Assert.IsTrue(File.Exists($"{_saveFileTestPath}/ImageListFileTest2.txt"));
            Assert.IsTrue(File.Exists($"{_saveFileTestPath}/ImageListFileTest3.txt"));
        }
    }
}
