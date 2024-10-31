namespace NFind;
public class LineSourceFactory
{
    public static IEnumerable<ILineSource> CreateInstance(string path, bool skipOfflineFiles = true)
    {
        if (string.IsNullOrEmpty(path))
        {
            return [new ConsoleLineSource()];
        }
        else
        {
            string pattern;
            int idx = path.LastIndexOf(Path.PathSeparator);
            if (idx < 0)
            {
                pattern = path;
                path = ".";
            }
            else
            {
                pattern = path.Substring(idx + 1);
                path = path.Substring(0, idx);
            }

            var dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                var files = dir.GetFiles(pattern);
                if (!skipOfflineFiles)
                {
                    files = files.Where(f => f.Attributes.HasFlag(FileAttributes.Offline)).ToArray();
                }
                return files.Select(f => new FileLineSource(f.FullName, f.Name));
            }
        }

        return [];
    }
}
