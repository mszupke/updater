using System;
using CommandLine;

namespace Updater.Options
{
    public class ApplicationUpdaterOptions
    {
        [Option('r', "release-dir", Required = true, HelpText = "Release directory path")]
        public string ReleaseDir { get; set; }

        [Option('i', "install-dir", Required = true, HelpText = "Install directory path")]
        public string InstallDir { get; set; }

        [Option('a', "app", Required = true, HelpText = "Application executable name")]
        public string AppName { get; set; }
    }
}

