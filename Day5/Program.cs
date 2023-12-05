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

        foreach (var seed in seeds)
        {
            foreach (var map in maps)
            {
                Console.WriteLine($"{map.Key}:");
                var destinationRangeStart = map.Value[0];
                var sourceRangeStart = map.Value[1];
                var sourceRangeLength = map.Value[2];

                Console.WriteLine();
            }
        }
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
