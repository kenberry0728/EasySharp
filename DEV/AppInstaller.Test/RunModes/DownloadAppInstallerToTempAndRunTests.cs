using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using AppInstaller.RunModes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppInstaller.Test.RunModes
{
    [TestClass]
    public class DownloadAppInstallerToTempAndRunTests : RunModeTestsBase
    {
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

        [TestMethod]
        public void Run()
        {
            // Arrange
            var target = this.CreateTarget();
            AppInstallerArgument argument = new AppInstallerArgument()
            {
                InstallDir = InstallDirPath,
                SourceDir = SourceDirPath
            };

            // Act
            var result = target.Run(argument);

            // Assert
            Assert.AreEqual(ResultCode.Success, result.ResultCode);
        }

        #endregion

        #region Helper Methods

        private DownloadAppInstallerToTempAndRun CreateTarget()
        {
            return new DownloadAppInstallerToTempAndRun(AppInstallerAssemblyName);
        }

        #endregion
    }
}