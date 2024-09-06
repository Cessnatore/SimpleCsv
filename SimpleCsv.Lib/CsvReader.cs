using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("SimpleCsv.Test")]
namespace SimpleCsv.Lib;

public class CsvReader(CsvOptions options)
{
    private ICollection<string> Headers = [];
    public IEnumerable<IDictionary<string, string>> ReadLines(StreamReader reader)
    {
        if (!Headers.Any()) Headers = reader.ReadLine()?.Split(options.Delimiter) ?? throw new CsvException("Stream had no lines.");
        while (!reader.EndOfStream)
        {
            yield return ParseLine(reader.ReadLine() ?? throw new CsvException("Encountered a null line in the stream."));
        }
    }

    internal void SetHeaders(ICollection<string> TestHeaders)
    {
        Headers = TestHeaders;
    }
    internal IDictionary<string, string> ParseLine(string line)
    {
        var result = new Dictionary<string, string>();
        var inQuote = false;
        var i = 0;
        var strBuilder = new StringBuilder();
        foreach (var c in line)
        {
            if (inQuote && c == options.Quote) inQuote = false;
            else if (!inQuote && c == options.Quote) inQuote = true;
            if (!inQuote && c == options.Delimiter)
            {
                result.Add(Headers.ElementAt(i), strBuilder.ToString());
                strBuilder.Clear();
                i++;
            }
            else
            {
                if (c != options.Quote) strBuilder.Append(c);
            }
        }
        result.Add(Headers.ElementAt(i), strBuilder.ToString());
        return result;
    }
}

public class CsvException : Exception
{
    public CsvException(string Message) : base(Message) { }
}
