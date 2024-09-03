namespace SimpleCsv.Lib;

public class CsvOptions(char Delimiter = ',', char Quote = '"')
{
    public char Delimiter { get; } = Delimiter;
    public char Quote { get; } = Quote;
}
