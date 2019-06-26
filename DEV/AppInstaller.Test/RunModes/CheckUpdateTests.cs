using System.Collections.Generic;
using AppInstaller.Core.Results;
using AppInstaller.RunModes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppInstaller.Test.RunModes
{
    [TestClass]
    public class CheckUpdateTests
    {
        [TestMethod]
        public void SameItems()
        {
            var sourceDir = @"RunModes\CheckUpdateTestsResources\SameItems\SourceDir";
            var installDir= @"RunModes\CheckUpdateTestsResources\SameItems\InstallDir";
            
            var target = new CheckUpdate();
            var result = target.Run(sourceDir, installDir, new List<string>());

            Assert.AreEqual(ResultCode.Success, result.ResultCode);
            Assert.IsFalse(result.Updated);
        }
    }
}