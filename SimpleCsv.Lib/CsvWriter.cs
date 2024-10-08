namespace SimpleCsv.Lib;

public class CsvWriter<T>(CsvOptions options, StreamWriter streamWriter)
{
    private readonly StreamWriter Stream = streamWriter;
    private readonly String Headers = string.Empty;

    public void WriteHeaders(ICollection<T> data)
    {
        Stream.WriteLine(GenerateHeader(data.FirstOrDefault() ?? throw new NullReferenceException("The list of records was empty")));
    }

    public void WriteRow(T data)
    {
        Stream.WriteLine(GenerateRow(data));
    }

    public void WriteRows(ICollection<T> data)
    {
        foreach (var line in data)
        {
            Stream.WriteLine(GenerateRow(line));
            Stream.Flush();
        }
    }

    internal string GenerateHeader(T data)
    {
        if (data is null) return "";
        return data.GetType().GetProperties()
            .Select(prop => prop.Name)
            .Aggregate((name, res) => name + options.Delimiter + res);
    }

    internal string GenerateRow(T data)
    {
        if (data is null) return "";
        return data.GetType().GetProperties()
            .Select(prop => prop.GetValue(data)?.ToString() ?? "")
            .Aggregate((name, res) => name + options.Delimiter + res);
    }
}
