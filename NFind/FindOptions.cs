namespace NFind;
public class FindOptions
{
    public string StringToFind { get; set; } = string.Empty;
    public bool IsCaseSensitive { get; set; } = false;
    public bool FindDontContains { get; set; } = false;
    public bool CountMode { get; set; } = false;
    public bool ShowLineNumber { get; set; } = false;
    public bool SkipOfflineFile { get; set; } = true;
    public string Path { get; set; } = string.Empty;
    public bool HelpMode { get; set; } = false;
}
