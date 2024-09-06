using SimpleCsv.Lib;

namespace SimpleCsv.Test;

public class CsvWriterTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldGenerateHeaders()
    {
        var record = new TestRecord("Hello", "Goodbye", "Ded");
        var writer = new CsvWriter<TestRecord>(new CsvOptions(), new StreamWriter("test.txt"));
        Assert.That(writer.GenerateHeader(record), Is.EqualTo("Name,Surname,Rank"));
    }
    [Test]
    public void ShouldGenerateRow()
    {
        var record = new TestRecord("Hello", "Goodbye", "Ded");
        var writer = new CsvWriter<TestRecord>(new CsvOptions(), new StreamWriter("test.txt"));
        Assert.That(writer.GenerateRow(record), Is.EqualTo("Hello,Goodbye,Ded"));
    }
    [Test]
    public void ShouldWriteCsv()
    {
        var recods = new List<TestRecord>
        {
            new TestRecord("1", "Goodbye1", "Ded1"),
            new TestRecord("2", "Goodbye2", "Ded2"),
            new TestRecord("3", "Goodbye3", "Ded3"),
            new TestRecord("4", "Goodbye4", "Ded4"),
            new TestRecord("5", "Goodbye5", "Ded5"),
        };
        using (var ms = new MemoryStream())
        {
            var writer = new CsvWriter<TestRecord>(new CsvOptions(), new StreamWriter(ms));
            writer.WriteHeaders(recods);
            writer.WriteRows(recods);
            Assert.IsTrue(ms.Length > 0);
        }
    }
}
public record TestRecord(string Name, string Surname, string Rank);
