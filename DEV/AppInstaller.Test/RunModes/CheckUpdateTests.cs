using System.Collections.Generic;
using System.IO;
using AppInstaller.Core.Results;
using AppInstaller.RunModes;

using EasySharp.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppInstaller.Test.RunModes
{
    [TestClass]
    public class CheckUpdateTests : RunModeTestsBase
    {
        #region Fields

        #endregion

        #region Common

        [TestInitialize]
        public void TestInitialize()
        {
            TestInitializeBase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestCleanupBase();
        }

        #endregion

        [TestMethod]
        public void AllFilesAreTheSameLastWriteTime()
        {
            // Arrange
            var target = new CheckUpdate();

            // Act
            var result = target.Run(SourceDirPath, InstallDirPath, new List<string>());

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsFalse(result.Updated);
        }

        [TestMethod]
        public void AllFilesAreDifferentLastWriteTime()
        {
            // Arrange
            SourceDirPath.ToDirectoryPath().SetLastWriteTimeToAllFiles(UpdateDateTime2);
            var target = new CheckUpdate();

            // Act
            var result = target.Run(SourceDirPath, InstallDirPath, new List<string>());

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsTrue(result.Updated);
        }

        [TestMethod]
        public void OneOfTheFileIsUpdated()
        {
            // Arrange
            var updatedFilePath = Path.Combine(SourceDirPath, "a.txt");
            File.SetLastWriteTime(updatedFilePath, UpdateDateTime2);

            var target = new CheckUpdate();

            // Act
            var result = target.Run(SourceDirPath, InstallDirPath, new List<string>());

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsTrue(result.Updated);
        }

        [TestMethod]
        public void OneOfTheFileIsUpdatedButThatIsExcluded()
        {
            // Arrange
            var updatedFilePath = Path.Combine(SourceDirPath, "a.txt");
            File.SetLastWriteTime(updatedFilePath, UpdateDateTime2);

            var target = new CheckUpdate();

            // Act
            var result = target.Run(
                SourceDirPath,
                InstallDirPath,
                new List<string> {@"a\.txt"});

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsFalse(result.Updated);
        }

        [TestMethod]
        public void UserFilesAreUpdate()
        {
            // Arrange
            var userDataFilePath = Path.Combine(InstallDirPath, UserDataDirFolderName, "u_a.txt");
            File.SetLastWriteTime(userDataFilePath, UpdateDateTime2);

            var target = new CheckUpdate();

            // Act
            var result = target.Run(
                SourceDirPath, 
                InstallDirPath, 
                new List<string> { UserDataDirFolderName + @"\\" + @".*" });

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsFalse(result.Updated);
        }

        [TestMethod]
        public void UserFileAndSystemFileAreUpdated()
        {
            // Arrange
            var updatedSystemFilePath = Path.Combine(SourceDirPath, "a.txt");
            File.SetLastWriteTime(updatedSystemFilePath, UpdateDateTime1);
            var userDataFilePath = Path.Combine(InstallDirPath, UserDataDirFolderName, "u_a.txt");
            File.SetLastWriteTime(userDataFilePath, UpdateDateTime2);

            var target = new CheckUpdate();

            // Act
            var result = target.Run(
                SourceDirPath,
                InstallDirPath,
                new List<string> { UserDataDirFolderName + @"\\" + @".*" });

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsTrue(result.Updated);
        }
    }
}