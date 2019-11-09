using EasySharp.Reflections.Core;
using EasySharp.IO;
using System;
using System.IO;

namespace AppInstaller.Test.RunModes
{
    public class RunModeTestsBase
    {
        protected const string TestRootDir = "TestFilesRoot";
        protected const string UserDataDirFolderName = "UserDataDir";
        protected static readonly string TestClassRootFolder = new DirectoryInfo(typeof(RunModeTestsBase).GetRelativeTypePath()).Parent.FullName;
        protected const string AppInstallerAssemblyName = "AppInstaller.bat";

        protected static readonly string SourceDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "SourceDirTemp");
        protected static readonly string InstallDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "InstallDirTemp");
        protected static readonly DateTime StandardDateTime = DateTime.Now;
        protected static readonly DateTime UpdateDateTime1 = StandardDateTime + TimeSpan.FromDays(1);
        protected static readonly DateTime UpdateDateTime2 = StandardDateTime + TimeSpan.FromDays(2);


        protected void TestInitializeBase()
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

        protected void TestCleanupBase()
        {
            SourceDirPath.DeleteDirectoryRecursively();
            InstallDirPath.DeleteDirectoryRecursively();
        }
    }
}
