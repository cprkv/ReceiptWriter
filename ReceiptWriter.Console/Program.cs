using ReceiptWriter;

while (true)
{
    Console.WriteLine("Enter dollars (or empty line to exit):");
    var line = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(line))
    {
        Console.WriteLine("Exiting...");
        break;
    }

    if (!Dollars.TryParse(line, out var result))
    {
        Console.WriteLine("Invalid format. Should be 123.45");
        continue;
    }

    Console.WriteLine(result.FormatToWords());
}