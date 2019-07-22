using System;
using System.Collections.Generic;
using System.IO;
using AppInstaller.Core.Results;
using AppInstaller.RunModes;
using EasySharpStandard.DiskIO;
using EasySharpStandard.Reflections.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppInstaller.Test.RunModes
{
    [TestClass]
    public class CheckUpdateTests
    {
        #region Fields

        private const string TestRootDir = "TestFilesRoot";
        private const string UserDataDirFolderName = "UserDataDir";
        private static readonly string TestClassRootFolder = new DirectoryInfo(typeof(CheckUpdateTests).GetRelativeTypePath()).Parent.FullName;

        private static readonly string SourceDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "SourceDirTemp");
        private static readonly string InstallDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "InstallDirTemp");

        private static readonly DateTime StandardDateTime = DateTime.Now;
        private static readonly DateTime UpdateDateTime1 = StandardDateTime + TimeSpan.FromDays(1);
        private static readonly DateTime UpdateDateTime2 = StandardDateTime + TimeSpan.FromDays(2);

        #endregion

        #region Common

        [TestInitialize]
        public void TestInitialize()
        {
            SourceDirPath.DeleteDirectoryRecursively();
            InstallDirPath.DeleteDirectoryRecursively();

            var sourceInitialDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "SourceDir");
            var installInitialDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "InstallDir");

            sourceInitialDirPath.CopyDirectory(SourceDirPath);
            installInitialDirPath.CopyDirectory(InstallDirPath);

            SourceDirPath.SetLastWriteTimeToAllFiles(StandardDateTime);
            InstallDirPath.SetLastWriteTimeToAllFiles(StandardDateTime);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            SourceDirPath.DeleteDirectoryRecursively();
            InstallDirPath.DeleteDirectoryRecursively();
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
            SourceDirPath.SetLastWriteTimeToAllFiles(UpdateDateTime2);
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