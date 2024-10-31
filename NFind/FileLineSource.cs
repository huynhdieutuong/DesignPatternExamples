namespace NFind;

public class FileLineSource : ILineSource
{
    private readonly string _path;
    private StreamReader? _reader;
    private int number = 0;
    private readonly string _fileName;
    public string Name => _fileName;

    public FileLineSource(string path, string fileName)
    {
        _path = path;
        _fileName = fileName;
    }

    public void Open()
    {
        if (_reader != null) throw new InvalidOperationException();
        _reader = new StreamReader(new FileStream(_path, FileMode.Open, FileAccess.Read));
    }

    public void Close()
    {
        if (_reader != null)
        {
            _reader.Close();
        }
    }

    public Line? ReadLine()
    {
        if (_reader == null) throw new InvalidOperationException();

        var s = _reader.ReadLine();
        if (s == null) return null;

        return new Line { LineNumber = ++number, Text = s };
    }
}