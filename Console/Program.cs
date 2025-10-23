using StringManager;

if (args.Length != 4)
{
    Console.WriteLine("Usage: <source file> <destination file> <value to match> <new value>");
    Console.ReadLine();
    return;
}

try
{
    string source;
    using (var reader = new StreamReader(args[0]))
    {
        source = reader.ReadToEnd();
    }

    var stringManager = new MyStringManager(source);
    Func<string, string, int[][]> customMatcher = Matchers.KMPMatcher;
    int[][] matchingIndices = stringManager.GetMatchingIndices(args[2], customMatcher);
    string replacedString = stringManager.ReplaceMany(matchingIndices, args[3]);

    // Create directory if it doesn't exist
    string? directory = Path.GetDirectoryName(args[1]);
    if (!string.IsNullOrEmpty(directory))
    {
        Directory.CreateDirectory(directory);
    }

    // Create or overwrite the file
    using var fileStream = new FileStream(args[1], FileMode.Create, FileAccess.Write);
    using var writer = new StreamWriter(fileStream);
    writer.Write(replacedString);

    Console.WriteLine($"Se han hecho {matchingIndices.Length} reemplazos.");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
    Console.ReadLine();
}

