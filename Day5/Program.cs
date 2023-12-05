using System.Text.RegularExpressions;

var seeds = ExtractSeeds(File.ReadLines("input.txt").First());
Console.WriteLine("Seeds: " + string.Join(" ", seeds));

var maps = ExtractMaps(File.ReadAllText("input.txt"));
foreach (var map in maps)
{
    Console.WriteLine($"{map.Key}:");
    foreach (var line in map.Value)
    {
        Console.WriteLine(line);
    }
    Console.WriteLine();
}

static List<int> ExtractSeeds(string line)
{
    var match = Regex.Match(line, @"seeds:\s*(.*)");
    if (match.Success)
    {
        return match.Groups[1].Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(int.Parse).ToList();
    }
    return new List<int>();
}

static Dictionary<string, List<string>> ExtractMaps(string input)
{
    var maps = new Dictionary<string, List<string>>();
    var mapTitles = Regex.Matches(input, @"(\w+-to-\w+ map:)");

    foreach (Match title in mapTitles)
    {
        string mapName = title.Groups[1].Value;
        int startIndex = title.Index + title.Length;

        int endIndex = input.Length;
        if (title.NextMatch().Success)
        {
            endIndex = title.NextMatch().Index;
        }

        string mapContent = input.Substring(startIndex, endIndex - startIndex);
        var lines = new List<string>(mapContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries));
        maps.Add(mapName, lines);
    }

    return maps;
}