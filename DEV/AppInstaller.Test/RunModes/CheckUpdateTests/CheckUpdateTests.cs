using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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
        private readonly string SourceDirPath = Path.Combine(TestRootDir, "SourceDir");
        private readonly string InstallDirPath = Path.Combine(TestRootDir, "InstallDir");

        [TestMethod]
        public void AllFilesAreTheSameLastWriteTime()
        {
            // Arrange
            var relativeTypePath = typeof(CheckUpdateTests).GetRelativeTypePath();
            var sourceDirPath = Path.Combine(relativeTypePath, SourceDirPath);
            var installDirPath = Path.Combine(relativeTypePath, InstallDirPath);

            var lastWriteTimeToSet = DateTime.Now;
            sourceDirPath.SetLastWriteTimeToAllFiles(lastWriteTimeToSet);
            installDirPath.SetLastWriteTimeToAllFiles(lastWriteTimeToSet);

            var target = new CheckUpdate();

            // Act
            var result = target.Run(sourceDirPath, installDirPath, new List<string>());

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsFalse(result.Updated);
        }

        [TestMethod]
        public void AllFilesAreDifferentLastWriteTime()
        {
            // Arrange
            var relativeTypePath = typeof(CheckUpdateTests).GetRelativeTypePath();
            var sourceDirPath = Path.Combine(relativeTypePath, SourceDirPath);
            var installDirPath = Path.Combine(relativeTypePath, InstallDirPath);

            installDirPath.SetLastWriteTimeToAllFiles(DateTime.Now);
            Thread.Sleep(1);
            sourceDirPath.SetLastWriteTimeToAllFiles(DateTime.Now);

            var target = new CheckUpdate();

            // Act
            var result = target.Run(sourceDirPath, installDirPath, new List<string>());

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsTrue(result.Updated);
        }

        [TestMethod]
        public void OneOfTheFileIsTheDifferentButThatIsExcluded()
        {
            // Arrange
            var relativeTypePath = typeof(CheckUpdateTests).GetRelativeTypePath();
            var sourceDirPath = Path.Combine(relativeTypePath, SourceDirPath);
            var installDirPath = Path.Combine(relativeTypePath, InstallDirPath);
            var updatedFilePath = Path.Combine(sourceDirPath, "a.txt");

            var lastWriteTimeToSet = DateTime.Now;
            sourceDirPath.SetLastWriteTimeToAllFiles(lastWriteTimeToSet);
            installDirPath.SetLastWriteTimeToAllFiles(lastWriteTimeToSet);
            Thread.Sleep(1);
            File.SetLastWriteTime(updatedFilePath, DateTime.Now);

            var target = new CheckUpdate();

            // Act
            var result = target.Run(sourceDirPath, installDirPath, new List<string> {@"a\.txt"});

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsFalse(result.Updated);
        }
    }
}