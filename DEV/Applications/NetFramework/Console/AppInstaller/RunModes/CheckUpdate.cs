using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AppInstaller.Core.Results;
using EasySharpStandard.DiskIO.Directories.Core;
using EasySharpStandard.DiskIO.Files.Core;

namespace AppInstaller.RunModes
{
    public class CheckUpdate
    {
        private readonly IDirectoryService directoryService;
        private readonly IFileService fileService;

        public CheckUpdate(IDirectoryService directoryService, IFileService fileService)
        {
            this.directoryService = directoryService;
            this.fileService = fileService;
        }

        public AppInstallerResult Run(string sourceDir, string installDir, List<string> excludeRegex)
        {
            var excludeRegexList = excludeRegex.Select(ex => new Regex(ex)).ToList();

            var sourceDirInfo = new DirectoryInfo(sourceDir);
            var sourceLastUpdateDate = GetLastWriteTimeUtc(sourceDirInfo, excludeRegexList);

            var installDirInfo = new DirectoryInfo(installDir);
            var installLastUpdateDate = GetLastWriteTimeUtc(installDirInfo, excludeRegexList);
            return new AppInstallerResult
            {
                ResultCode = ResultCode.Success,
                Updated = sourceLastUpdateDate > installLastUpdateDate
            };
        }

        private static DateTime GetLastWriteTimeUtc(DirectoryInfo targetDirectoryInfo, IEnumerable<Regex> regex)
        {
            return targetDirectoryInfo.GetFiles("*", SearchOption.AllDirectories)
                .Where(f => f.IsTargetFile(targetDirectoryInfo.FullName, regex))
                .Max(f => f.LastWriteTimeUtc);
        }
    }
}