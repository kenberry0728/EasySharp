using EasySharp.IO;
using System;
using System.IO;
using EasySharp.Reflection;

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


        protected static void TestInitializeBase()
        {
            SourceDirPath.ToDirectoryPath().DeleteDirectory();
            InstallDirPath.ToDirectoryPath().DeleteDirectory();

            var sourceInitialDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "SourceDir");
            var installInitialDirPath = Path.Combine(TestClassRootFolder, TestRootDir, "InstallDir");

            sourceInitialDirPath.ToDirectoryPath().CopyDirectory(SourceDirPath.ToDirectoryPath());
            installInitialDirPath.ToDirectoryPath().CopyDirectory(InstallDirPath.ToDirectoryPath());

            SourceDirPath.ToDirectoryPath().SetLastWriteTimeToAllFiles(StandardDateTime);
            InstallDirPath.ToDirectoryPath().SetLastWriteTimeToAllFiles(StandardDateTime);
        }

        protected static void TestCleanupBase()
        {
            SourceDirPath.ToDirectoryPath().DeleteDirectory();
            InstallDirPath.ToDirectoryPath().DeleteDirectory();
        }
    }
}
