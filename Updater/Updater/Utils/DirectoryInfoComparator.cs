namespace Updater.Utils
{
    public class DirectoryInfoComparator : IComparer<DirectoryInfo>
    {
        public int Compare(DirectoryInfo x, DirectoryInfo y)
        {
            return -1 * (x.Name.IsNewer(y.Name) ? 1 : -1);
        }
    }
}

