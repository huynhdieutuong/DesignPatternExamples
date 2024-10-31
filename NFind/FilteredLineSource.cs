namespace NFind;
internal class FilteredLineSource : ILineSource
{
    private readonly ILineSource parent;
    private readonly Func<Line, bool>? f;
    public string Name => parent.Name;

    public FilteredLineSource(ILineSource parent, Func<Line, bool>? f = null)
    {
        this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
        this.f = f;
    }

    public Line? ReadLine()
    {
        if (f == null) return parent.ReadLine();

        var line = parent.ReadLine();
        if (line == null) return null;
        while (line != null && !f(line))
        {
            line = parent.ReadLine();
        }
        return line;
    }

    public void Open()
    {
        parent.Open();
    }

    public void Close()
    {
        parent.Close();
    }
}
