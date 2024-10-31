namespace NFind.Tests;

[TestClass()]
public class LineSourceFactoryTests
{
    [TestMethod()]
    public void CreateInstance_WithNullOrEmptyPath_ReturnConsoleLineSource()
    {
        string path = string.Empty;

        var result = LineSourceFactory.CreateInstance(path);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.First(), typeof(ConsoleLineSource));
    }

    [TestMethod()]
    public void CreateInstance_WithValidPath_ReturnFileLineSources()
    {
        // Arrange
        string path = @"C:\TestDirectory";
        Directory.CreateDirectory(path);

        // Create a test file in the directory
        string testFilePath = Path.Combine(path, "test.txt");
        File.Create(testFilePath).Dispose();

        // Act
        var result = LineSourceFactory.CreateInstance(path);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
        Assert.IsInstanceOfType(result.First(), typeof(FileLineSource));

        // Cleanup
        File.Delete(testFilePath);
        Directory.Delete(path);
    }

    [TestMethod]
    public void CreateInstance_WithSkipOfflineFiles_ReturnsOnlyOfflineFiles()
    {
        // Arrange
        string path = @"C:\TestDirectory";
        Directory.CreateDirectory(path);

        // Create test files in the directory
        string offlineFilePath = Path.Combine(path, "offline.txt");
        string onlineFilePath = Path.Combine(path, "online.txt");

        var offlineFile = File.Create(offlineFilePath);
        offlineFile.Dispose();
        File.SetAttributes(offlineFilePath, FileAttributes.Offline);

        var onlineFile = File.Create(onlineFilePath);
        onlineFile.Dispose();

        // Act
        var result = LineSourceFactory.CreateInstance(path, skipOfflineFiles: true);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
        Assert.IsInstanceOfType(result.First(), typeof(FileLineSource));

        // Cleanup
        File.Delete(offlineFilePath);
        File.Delete(onlineFilePath);
        Directory.Delete(path);
    }
}