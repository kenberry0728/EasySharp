using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AppInstaller.Core.Results;
using EasySharp.IO;
using EasySharp.IO.Files.Core;
using EasySharp.IO.Files.Implementation;
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
            sourceDir = sourceDir.ToFullDirectoryName();
            installDir = installDir.ToFullDirectoryName();
            var excludeRegexList = excludeRegex.Select(ex => new Regex(ex, RegexOptions.IgnoreCase)).ToList();
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
            var files = this.directoryService.GetFiles(targetDirectoryPath, "*", SearchOption.AllDirectories).ToList();
            var fileAndRelativePaths = files.Select(f => new { f, Relativepath = f.GetRelativePath(targetDirectoryPath) }).ToList();
            var filteredFileAndRelativepaths = fileAndRelativePaths.Where(f => !regularExpressions.AnyIsMatch(f.Relativepath)).ToList();
            var result = filteredFileAndRelativepaths.Max(f => this.fileService.GetLastWriteTimeUtc(f.f));
            return result;
        }
    }
}