using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AppInstaller.Core.Results;
using EasySharpStandard.DiskIO;
using EasySharpStandard.DiskIO.Directories.Core;
using EasySharpStandard.DiskIO.Directories.Implementation;
using EasySharpStandard.DiskIO.Files.Core;
using EasySharpStandard.DiskIO.Files.Implementation;
using EasySharpStandard.RegularExpressions.Core;

namespace AppInstaller.RunModes
{
    public class CheckUpdate
    {
        private readonly IDirectoryService directoryService;
        private readonly IFileService fileService;

        public CheckUpdate(
            IDirectoryService directoryService = null, 
            IFileService fileService = null)
        {
            this.directoryService = directoryService.Resolve();
            this.fileService = fileService.Resolve();
        }

        public AppInstallerResult Run(string sourceDir, string installDir, List<string> excludeRegex)
        {
            sourceDir = this.directoryService.GetFullName(sourceDir);
            installDir = this.directoryService.GetFullName(installDir);
            var excludeRegexList = excludeRegex.Select(ex => new Regex(ex)).ToList();
            var sourceLastUpdateDate = GetLastWriteTimeUtc(sourceDir, excludeRegexList);
            var installLastUpdateDate = GetLastWriteTimeUtc(installDir, excludeRegexList);
            return new AppInstallerResult
            {
                ResultCode = ResultCode.Success,
                Updated = sourceLastUpdateDate > installLastUpdateDate
            };
        }

        private DateTime GetLastWriteTimeUtc(string targetDirectoryPath, IEnumerable<Regex> regularExpressions)
        {
            return this.directoryService.GetFiles(targetDirectoryPath, "*", SearchOption.AllDirectories)
                .Where(f => !regularExpressions.AnyIsMatch(f.GetRelativePath(targetDirectoryPath)))
                .Max(f => this.fileService.GetLastWriteTimeUtc(f));
        }
    }
}