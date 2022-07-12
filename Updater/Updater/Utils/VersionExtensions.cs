using System;
namespace Updater.Utils
{
    public static class VersionExtensions
    {
        public static bool IsNewer(this String oldVersion, String newVersion)
        {
            return oldVersion.CompareTo(newVersion) > 0;
        }

        public static DirectoryInfo GetLatestVersion(this string path)
        {
            if (!Directory.Exists(path))
            {
                throw new Exception($"Directory {path} does not exists");
            }

            var directoryInfo = new DirectoryInfo(path);
            var subdirectories = directoryInfo.GetDirectories();

            if (subdirectories.Length == 0)
            {
                throw new Exception($"Directory  {path} is empty");
            }

            Array.Sort(subdirectories, new DirectoryInfoComparator());
            return subdirectories[0];
        }
    }
}

