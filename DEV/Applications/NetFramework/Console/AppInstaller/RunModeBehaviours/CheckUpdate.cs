using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;

namespace AppInstaller.RunModeBehaviours
{
    public class CheckUpdate
    {
        public AppInstallerResult Run(AppInstallerArgument appInstallerArgument)
        {
            var excludeRegexList = appInstallerArgument.ExcludePathRegex.Select(ex => new Regex(ex)).ToList();

            var sourceDirInfo = new DirectoryInfo(appInstallerArgument.SourceDir);
            var sourceLastUpdateDate = GetLastWriteTimeUtc(sourceDirInfo, excludeRegexList);

            var installDirInfo = new DirectoryInfo(appInstallerArgument.InstallDir);
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