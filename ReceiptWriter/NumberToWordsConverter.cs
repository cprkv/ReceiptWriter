using System.Text;

namespace ReceiptWriter;

public static class NumberToWordsConverter
{
    private static readonly string[] SmallNumbers =
    [
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve",
        "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen",
    ];

    private static readonly string[] TensNumbers =
    [
        "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety",
    ];

    private static readonly (int, string)[] BigNumbers =
    [
        (1_000_000_000, "billion"),
        (1_000_000, "million"),
        (1_000, "thousand"),
    ];

    public static string ToWords(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);

        if (value == 0)
            return "zero";

        var stringBuilder = new StringBuilder();
        var hasPrevious   = false;

        foreach (var (divisor, word) in BigNumbers)
        {
            if (WordsUnderThousand(value / divisor, stringBuilder, hasPrevious))
            {
                stringBuilder.Append(' ');
                stringBuilder.Append(word);
                hasPrevious = true;
            }

            value %= divisor;
        }

        WordsUnderThousand(value, stringBuilder, hasPrevious);

        return stringBuilder.ToString();
    }

    private static bool WordsUnderThousand(int value, StringBuilder sb, bool putComma)
    {
        if (value == 0)
            return false;

        if (value >= 100)
        {
            if (putComma)
            {
                sb.Append(", ");
                putComma = false;
            }

            sb.Append(SmallNumbers[value / 100]);
            sb.Append(" hundred");
            value %= 100;

            if (value == 0)
                return true;

            sb.Append(" and ");
        }

        if (value >= 20)
        {
            if (putComma)
            {
                sb.Append(", ");
                putComma = false;
            }

            sb.Append(TensNumbers[value / 10]);
            value %= 10;

            if (value == 0)
                return true;

            sb.Append(' ');
        }

        if (putComma)
            sb.Append(", ");

        sb.Append(SmallNumbers[value]);
        return true;
    }
}