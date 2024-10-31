namespace NFind.Tests;

[TestClass()]
public class ProgramTests
{
    [TestMethod()]
    public void BuildOptionsTest()
    {
        string[] args = ["/v", "/c", "/n", "/i"];

        var options = Program.BuildOptions(args);

        Assert.IsNotNull(options);
        Assert.IsTrue(options.FindDontContains);
        Assert.IsTrue(options.CountMode);
        Assert.IsTrue(options.ShowLineNumber);
        Assert.IsTrue(options.IsCaseSensitive);
    }
}