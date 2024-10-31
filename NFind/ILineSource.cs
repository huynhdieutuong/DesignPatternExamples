namespace NFind;
public interface ILineSource
{
    string Name { get; }
    Line? ReadLine();
    void Open();
    void Close();
}
