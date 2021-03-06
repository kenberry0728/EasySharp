﻿using System;
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
        public CheckUpdate()
        {
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

        private static DateTime GetLastWriteTimeUtc(string targetDirectoryPath, IEnumerable<Regex> regularExpressions)
        {
            var directoryPath = targetDirectoryPath.ToDirectoryPath();
            var files = directoryPath.GetFiles("*", SearchOption.AllDirectories).ToList();
            var fileAndRelativePaths = files.Select(f => new { f, Relativepath = f.ToFilePath().GetRelativePath(targetDirectoryPath.ToDirectoryPath()).Value }).ToList();
            var filteredFileAndRelativepaths = fileAndRelativePaths.Where(f => !regularExpressions.AnyIsMatch(f.Relativepath)).ToList();
            var result = filteredFileAndRelativepaths.Max(f => f.f.ToFilePath().GetLastWriteTimeUtc());
            return result;
        }
    }
}