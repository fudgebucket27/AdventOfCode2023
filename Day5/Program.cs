using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Include for LINQ functionalities

class Program
{
    static void Main()
    {
        // Read the first line of the file for seeds
        string firstLine = File.ReadLines("input.txt").First();
        var seeds = ExtractSeeds(firstLine);
        Console.WriteLine("Seeds: " + string.Join(" ", seeds));

        // Read the rest of the file for maps
        string input = File.ReadAllText("input.txt");

        var maps = ExtractMaps(input);

        List<int> result = new List<int>();
        foreach (var seed in seeds)
        {
            int conversionResult = seed;
            foreach (var map in maps)
            {
                Console.WriteLine($"{map.Key}: Conversion: {conversionResult}");
                foreach(var line in map.Value)
                {
                    var destinationRangeStart = line[0];
                    var sourceRangeStart = line[1];
                    var sourceRangeLength = line[2];

                    int count = 0;
                    for (int i = sourceRangeStart; i <= sourceRangeStart + sourceRangeLength; i++)
                    {
                        if (i == conversionResult)
                        {
                            conversionResult = destinationRangeStart + count;
                            break;
                        }
                        count++;
                    }

                }

                Console.WriteLine();
            }
            Console.WriteLine($"Conversion: {conversionResult}");
            result.Add(conversionResult);
        }
        Console.WriteLine($"Lowest Location number: {result.Min()}");
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

    static Dictionary<string, List<List<int>>> ExtractMaps(string input)
    {
        var maps = new Dictionary<string, List<List<int>>>();
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
            var lines = mapContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select(int.Parse).ToList()).ToList();
            maps.Add(mapName, lines);
        }

        return maps;
    }
}
