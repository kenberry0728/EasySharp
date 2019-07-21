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

        private const string TestRootDir = "TestRoot";
        private static readonly string TestClassRootFolder = typeof(CheckUpdateTests).GetRelativeTypePath();

        private static readonly string SourceInitialDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "SourceDir");
        private static readonly string InstallInitialDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "InstallDir");

        private static readonly string SourceDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "SourceDirTemp");
        private static readonly string InstallDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "InstallDirTemp");

        private static readonly DateTime StandardDateTime = DateTime.Now;
        private static readonly DateTime UpdateDateTime = StandardDateTime + TimeSpan.FromDays(1);

        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            SourceDirPath.DeleteDirectoryRecursively();
            InstallDirPath.DeleteDirectoryRecursively();

            SourceInitialDirPath.CopyDirectory(SourceDirPath);
            InstallInitialDirPath.CopyDirectory(InstallDirPath);

            SourceDirPath.SetLastWriteTimeToAllFiles(StandardDateTime);
            InstallDirPath.SetLastWriteTimeToAllFiles(StandardDateTime);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            SourceDirPath.DeleteDirectoryRecursively();
            InstallDirPath.DeleteDirectoryRecursively();
        }

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
            SourceDirPath.SetLastWriteTimeToAllFiles(UpdateDateTime);
            var target = new CheckUpdate();

            // Act
            var result = target.Run(SourceDirPath, InstallDirPath, new List<string>());

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsTrue(result.Updated);
        }

        [TestMethod]
        public void OneOfTheFileIsTheDifferentButThatIsExcluded()
        {
            // Arrange
            var updatedFilePath = Path.Combine(SourceDirPath, "a.txt");
            File.SetLastWriteTime(updatedFilePath, UpdateDateTime);

            var target = new CheckUpdate();

            // Act
            var result = target.Run(SourceDirPath, InstallDirPath, new List<string> {@"a\.txt"});

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsFalse(result.Updated);
        }
    }
}