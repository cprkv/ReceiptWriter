namespace ReceiptWriter.Tests;

public class DollarsTests
{
    public static readonly TheoryData<decimal, string> ParseOkCases = new()
    {
        { 1.00m, "1" },
        { 1.00m, "1.00" },
        { 1.11m, "1.11" },
        { 1357256.32m, "1357256.32" },
    };

    public static readonly TheoryData<string, decimal> FormatToWordsCases = new()
    {
        { "zero DOLLARS AND zero CENTS", 0.00m },
        { "one DOLLARS AND zero CENTS", 1.00m },
        { "fifty DOLLARS AND fifty CENTS", 50.50m },
        { "zero DOLLARS AND fifty one CENTS", 0.51m },
        { "thirty two DOLLARS AND fifty one CENTS", 32.51m },
        { "three hundred DOLLARS AND fifty one CENTS", 300.51m },
        { "three hundred and fifty DOLLARS AND fifty one CENTS", 350.51m },
        { "three hundred and thirty two DOLLARS AND fifty one CENTS", 332.51m },
        { "one thousand DOLLARS AND fifty one CENTS", 1000.51m },
        { "seven thousand, fifty DOLLARS AND fifty CENTS", 7050.50m },
        { "seven hundred and fifty seven thousand DOLLARS AND zero CENTS", 757000.00m },
        {
            "one million, three hundred and fifty seven thousand, two hundred and fifty six DOLLARS AND thirty two CENTS",
            1357256.32m
        },
        { "one billion, one DOLLARS AND zero CENTS", 1_000_000_001m },
    };

    [Theory]
    [InlineData("")]
    [InlineData(".00")]
    [InlineData(".11")]
    [InlineData("1.111")]
    [InlineData("-1")]
    [InlineData("-1.11")]
    public void TryParseShouldReturnFalseOnInvalidFormat(string value)
    {
        Assert.False(Dollars.TryParse(value, out _));
    }

    [Theory]
    [MemberData(nameof(ParseOkCases))]
    public void TryParseShouldParseAndReturnTrue(decimal expected, string value)
    {
        Assert.True(Dollars.TryParse(value, out var result));
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [MemberData(nameof(FormatToWordsCases))]
    public void FormatToWordsShouldReturnHumanWords(string expected, decimal value)
    {
        Assert.Equal(expected, new Dollars(value).FormatToWords());
    }
}