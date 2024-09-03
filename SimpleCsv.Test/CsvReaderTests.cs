using SimpleCsv.Lib;

namespace SimpleCsv.Test;

public class CsvReaderTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldParseLine()
    {
        var reader = new CsvReader(new CsvOptions());
        reader.SetHeaders(["Header1", "Header2", "Header3"]);
        var result = reader.ParseLine("Cell1,\"Cell21,Cell22\",Cell3");
        Assert.That
        (
            result,
            Is.EquivalentTo
            (
                new Dictionary<string, string>()
                {
                    { "Header1", "Cell1" },
                    { "Header2", "Cell21,Cell22" },
                    { "Header3", "Cell3" }
                }
            )
        );
    }
    [Test]
    public void ShouldReadLines()
    {
        using (var csvStream = new StreamReader("./test.csv"))
        {
            var reader = new CsvReader(new CsvOptions());
            Assert.That
            (
                reader.ReadLines(csvStream).ToList()[3],
                Is.TypeOf(typeof(Dictionary<string, string>))
            );
        }

    }
}
