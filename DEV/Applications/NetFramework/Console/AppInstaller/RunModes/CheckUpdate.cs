using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AppInstaller.Core.Results;
using EasySharp.IO;
using EasySharp.Text.RegularExpressions;
using static EasySharp.IO.DirectoryPath;
using static EasySharp.IO.FilePath;

namespace AppInstaller.RunModes
{
    public class CheckUpdate
    {
        private readonly CreateFilePath createFilePath;
        private readonly CreateDirectoryPath createDirectoryPath;

        public CheckUpdate(
            CreateDirectoryPath createDirectoryPath = null)
        {
            this.createFilePath = FilePath.Create;
            this.createDirectoryPath = createDirectoryPath ?? Create;
        }

        public AppInstallerResult Run(string sourceDir, string installDir, List<string> excludeRegex)
        {
            sourceDir = sourceDir.ToDirectoryPath().ToFullDirectoryPath().Value;
            installDir = installDir.ToDirectoryPath().ToFullDirectoryPath().Value;
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
            var directoryPath = this.createDirectoryPath(targetDirectoryPath);
            var files = directoryPath.GetFiles("*", SearchOption.AllDirectories).ToList();
            var fileAndRelativePaths = files.Select(f => new { f, Relativepath = f.GetRelativePath(targetDirectoryPath) }).ToList();
            var filteredFileAndRelativepaths = fileAndRelativePaths.Where(f => !regularExpressions.AnyIsMatch(f.Relativepath)).ToList();
            var result = filteredFileAndRelativepaths.Max(f => this.createFilePath(f.f).GetLastWriteTimeUtc());
            return result;
        }
    }
}