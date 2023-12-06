using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Include for LINQ functionalities

class Program
{
    static void Main()
    {
        
        string firstLine = File.ReadLines("input.txt").First();
        var seeds = ExtractSeeds(firstLine);
    

        string input = File.ReadAllText("input.txt");
        var maps = ExtractMaps(input);

        //Part 1
        List<long> part1Result = new List<long>();
        foreach (var seed in seeds)
        {
            long conversionResult = seed;
            foreach (var map in maps)
            {
                foreach (var line in map.Value)
                {
                    var destinationRangeStart = line[0];
                    var sourceRangeStart = line[1];
                    var sourceRangeLength = line[2];
                    var sourceRangeEnd = sourceRangeStart + sourceRangeLength;

                    if (conversionResult >= sourceRangeStart && conversionResult <= sourceRangeEnd)
                    {
                        conversionResult = destinationRangeStart + (conversionResult - sourceRangeStart);
                        break; 
                    }
                }
            }
            part1Result.Add(conversionResult);
        }
        Console.WriteLine($"Part 1: Lowest Location number: {part1Result.Min()}");


        //Part 2
        var seedRanges = ExtractSeedsRanges(firstLine);
        List<long> part2Result = new List<long>();
        // Process each range of seeds
        foreach (var range in seedRanges)
        {
            for (long seed = range.Item1; seed < range.Item1 + range.Item2; seed++)
            {
                long conversionResult = seed;
                foreach (var map in maps)
                {
                    foreach (var line in map.Value)
                    {
                        var destinationRangeStart = line[0];
                        var sourceRangeStart = line[1];
                        var sourceRangeLength = line[2];
                        var sourceRangeEnd = sourceRangeStart + sourceRangeLength;

                        if (conversionResult >= sourceRangeStart && conversionResult <= sourceRangeEnd)
                        {
                            conversionResult = destinationRangeStart + (conversionResult - sourceRangeStart);
                            break;
                        }
                    }
                }
                part1Result.Add(conversionResult);
            }
        }
        Console.WriteLine($"Part 2: Lowest Location number: {part2Result.Min()}");

    }

    static List<Tuple<long, long>> ExtractSeedsRanges(string line)
    {
        var match = Regex.Match(line, @"seeds:\s*(.*)");
        var seedRanges = new List<Tuple<long, long>>();

        if (match.Success)
        {
            var parts = match.Groups[1].Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i += 2)
            {
                long start = long.Parse(parts[i]);
                long length = long.Parse(parts[i + 1]);
                seedRanges.Add(Tuple.Create(start, length));
            }
        }
        return seedRanges;
    }

    static List<long> ExtractSeeds(string line)
    {
        var match = Regex.Match(line, @"seeds:\s*(.*)");
        if (match.Success)
        {
            return match.Groups[1].Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(long.Parse).ToList();
        }
        return new List<long>();
    }

    static Dictionary<string, List<List<long>>> ExtractMaps(string input)
    {
        var maps = new Dictionary<string, List<List<long>>>();
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
                                  .Select(long.Parse).ToList()).ToList();
            maps.Add(mapName, lines);
        }

        return maps;
    }
}
