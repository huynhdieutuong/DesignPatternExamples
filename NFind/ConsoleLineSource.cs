namespace NFind;

public class ConsoleLineSource : ILineSource
{
    private int number = 0;
    public string Name => string.Empty;

    public ConsoleLineSource()
    {
    }

    public Line? ReadLine()
    {
        var s = Console.ReadLine();

        if (s == null) return null;
        return new Line { LineNumber = ++number, Text = s };
    }

    public void Open()
    {
    }

    public void Close()
    {
    }
}