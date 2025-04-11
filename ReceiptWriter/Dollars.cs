using System.Globalization;

namespace ReceiptWriter;

public sealed class Dollars(decimal value)
{
    public decimal Value { get; } = value;

    public static bool TryParse(string dollars, out Dollars result)
    {
        result = new Dollars(0);

        var dot = dollars.IndexOf('.');
        if (dot != -1 && (dot == 0 || dot != dollars.Length - 3))
            return false;

        if (!decimal.TryParse(dollars, CultureInfo.InvariantCulture, out var value))
            return false;

        if (value is < 0 or > 2_000_000_000m)
            return false;

        result = new Dollars(value);
        return true;
    }

    public string FormatToWords()
    {
        var floatPart       = Value * 100 % 100;
        var realPartString  = NumberToWordsConverter.ToWords((int)Value);
        var floatPartString = NumberToWordsConverter.ToWords((int)floatPart);
        return $"{realPartString} DOLLARS AND {floatPartString} CENTS";
    }
}