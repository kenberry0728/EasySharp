using System.Collections.Generic;
using System.IO;
using AppInstaller.Core.Results;
using AppInstaller.RunModes;
using EasySharpStandard.DiskIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppInstaller.Test.RunModes
{
    [TestClass]
    public class CleanupTempDirAndRunOriginalAppTests : RunModeTestsBase
    {
        [TestInitialize]
        public void TestInitialize()
        {
            base.TestInitializeBase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            base.TestCleanupBase();
        }

    }
}