namespace ReceiptWriter.Tests;

public class NumberToWordsConverterTests
{
    [Theory]
    [InlineData("zero", 0)]
    [InlineData("one", 1)]
    [InlineData("fifty", 50)]
    [InlineData("fifty one", 51)]
    [InlineData("seven hundred", 700)]
    [InlineData("seven hundred and fifty", 750)]
    [InlineData("seven hundred and two", 702)]
    [InlineData("seven hundred and fifty two", 752)]
    [InlineData("one thousand", 1000)]
    [InlineData("seven thousand, fifty", 7050)]
    [InlineData("seven hundred and fifty seven thousand", 757000)]
    [InlineData("one million, three hundred and fifty seven thousand, two hundred and fifty six", 1357256)]
    [InlineData("one million, three hundred thousand, two hundred", 1300200)]
    public void ToWordsShouldReturnHumanWords(string expected, int number)
    {
        Assert.Equal(expected, NumberToWordsConverter.ToWords(number));
    }
}