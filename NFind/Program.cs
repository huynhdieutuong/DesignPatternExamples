namespace NFind;
public class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("FIND: Parameter format not correct");
            return;
        }

        var findOptions = BuildOptions(args);

        var sources = LineSourceFactory.CreateInstance(findOptions.Path, findOptions.SkipOfflineFile);

        foreach (var source in sources)
        {
            ProcessSource(source, findOptions);
        }
    }

    private static void ProcessSource(ILineSource source, FindOptions findOptions)
    {
        var stringComparison = findOptions.IsCaseSensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

        source = new FilteredLineSource(source,
            line => findOptions.FindDontContains
                    ? !line.Text.Contains(findOptions.StringToFind, stringComparison)
                    : line.Text.Contains(findOptions.StringToFind, stringComparison)
            );

        Console.WriteLine($"---------- {source.Name.ToUpper()}");

        try
        {
            source.Open();
            var line = source.ReadLine();

            while (line != null)
            {
                Print(line, findOptions.ShowLineNumber);
                line = source.ReadLine();
            }
        }
        finally
        {
            source.Close();
        }
    }

    private static void Print(Line line, bool showLineNumber)
    {
        if (showLineNumber)
        {
            Console.WriteLine($"[{line.LineNumber}] {line.Text}");
        }
        else
        {
            Console.WriteLine($"{line.Text}");
        }
    }

    public static FindOptions BuildOptions(string[] args)
    {
        var options = new FindOptions();

        foreach (var arg in args)
        {
            switch (arg)
            {
                case "/v":
                    options.FindDontContains = true;
                    break;
                case "/c":
                    options.CountMode = true;
                    break;
                case "/n":
                    options.ShowLineNumber = true;
                    break;
                case "/i":
                    options.IsCaseSensitive = true;
                    break;
                case "/off":
                case "/offline":
                    options.SkipOfflineFile = false;
                    break;
                case "/?":
                    options.HelpMode = true;
                    break;
                default:
                    if (string.IsNullOrEmpty(options.StringToFind))
                    {
                        options.StringToFind = arg;
                    }
                    else if (string.IsNullOrEmpty(options.Path))
                    {
                        options.Path = arg;
                    }
                    else
                    {
                        throw new ArgumentException(arg);
                    }
                    break;
            }
        }
        return options;
    }
}
