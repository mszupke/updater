using Updater.Options;
using Updater.Utils;

namespace Updater
{
    public class Updater
    {
        private readonly ApplicationUpdaterOptions _options;

        public Updater(ApplicationUpdaterOptions options)
        {
            _options = options;
        }

        public void Update()
        {
            var latestRelease = _options.ReleaseDir.GetLatestVersion();
            Console.WriteLine($"Latest release: {latestRelease.Name}");

            var latestVersion = _options.InstallDir.GetLatestVersion();
            Console.WriteLine($"Latest version: {latestVersion.Name}");

            if (latestVersion.Name.IsNewer(latestRelease.Name))
            {
                Console.WriteLine($"There is a new version available: {latestVersion.Name}");

                var releaseDirectoryInfo = new DirectoryInfo(_options.ReleaseDir);
                releaseDirectoryInfo = releaseDirectoryInfo.CreateSubdirectory(latestVersion.Name);

                CopyFiles(latestVersion, releaseDirectoryInfo);
                RunApplication(releaseDirectoryInfo);
            }
            else
            {
                Console.WriteLine("Current release is a latest version. Nothing to copy");
            }
        }

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (var dir in source.GetDirectories())
            {
                CopyFiles(dir, target.CreateSubdirectory(dir.Name));
            }

            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name));
            }
        }

        private void RunApplication(DirectoryInfo releaseDirectory)
        {
            using (System.Diagnostics.Process pProcess = new System.Diagnostics.Process())
            {
                pProcess.StartInfo.FileName = Path.Combine(releaseDirectory.FullName, _options.AppName);
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                pProcess.StartInfo.CreateNoWindow = false;
                pProcess.Start();
                string output = pProcess.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                pProcess.WaitForExit();
            }
        }
    }
}

