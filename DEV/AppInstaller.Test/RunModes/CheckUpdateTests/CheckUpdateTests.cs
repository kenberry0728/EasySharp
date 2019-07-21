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
        private const string TestRootDir = "TestRoot";
        private readonly string SourceDirPath = Path.Combine(typeof(CheckUpdateTests).GetRelativeTypePath(), TestRootDir, "SourceDir");
        private readonly string InstallDirPath = Path.Combine(typeof(CheckUpdateTests).GetRelativeTypePath(), TestRootDir, "InstallDir");

        [TestInitialize]
        public void TestInitialize()
        {
            var lastWriteTimeToSet = DateTime.Now;
            SourceDirPath.SetLastWriteTimeToAllFiles(lastWriteTimeToSet);
            InstallDirPath.SetLastWriteTimeToAllFiles(lastWriteTimeToSet);
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
            SourceDirPath.SetLastWriteTimeToAllFiles(DateTime.Now);
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
            File.SetLastWriteTime(updatedFilePath, DateTime.Now);

            var target = new CheckUpdate();

            // Act
            var result = target.Run(SourceDirPath, InstallDirPath, new List<string> {@"a\.txt"});

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsFalse(result.Updated);
        }
    }
}