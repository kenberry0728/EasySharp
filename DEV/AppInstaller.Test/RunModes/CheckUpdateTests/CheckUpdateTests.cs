using System.Collections.Generic;
using System.IO;
using AppInstaller.Core.Results;
using AppInstaller.RunModes;
using EasySharpStandard.Reflections.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppInstaller.Test.RunModes
{
    [TestClass]
    public class CheckUpdateTests
    {
        [TestMethod]
        public void SameItems()
        {
            var relativeTypePath = typeof(CheckUpdateTests).GetRelativeTypePath();
            var sourceDir = Path.Combine(relativeTypePath, @"SameItems\SourceDir");
            var installDir = Path.Combine(relativeTypePath, @"SameItems\InstallDir");
            
            var target = new CheckUpdate();
            var result = target.Run(sourceDir, installDir, new List<string>());

            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsFalse(result.Updated);
        }

        [TestMethod]
        public void Updated()
        {
            var relativeTypePath = typeof(CheckUpdateTests).GetRelativeTypePath();
            var sourceDir = Path.Combine(relativeTypePath, @"Updated\SourceDir");
            var installDir = Path.Combine(relativeTypePath, @"Updated\InstallDir");

            var target = new CheckUpdate();
            var result = target.Run(sourceDir, installDir, new List<string>());

            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsTrue(result.Updated);
        }
    }
}