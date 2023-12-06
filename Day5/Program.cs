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
        Dictionary<string, SortedSet<Range>> efficientMaps = new Dictionary<string, SortedSet<Range>>();
        foreach (var mapEntry in maps)
        {
            var sortedRanges = new SortedSet<Range>(new RangeComparer());
            foreach (var line in mapEntry.Value)
            {
                sortedRanges.Add(new Range(line[1], line[1] + line[2], line[0]));
            }
            efficientMaps.Add(mapEntry.Key, sortedRanges);
        }
        List<long> part2Result = new List<long>();
        for (int i = 0; i < seeds.Count; i += 2)
        {
            long startSeed = seeds[i];
            long rangeLength = seeds[i + 1];

            for (long seed = startSeed; seed < startSeed + rangeLength; seed++)
            {
                long conversionResult = seed;
                foreach (var map in efficientMaps)
                {
                    var range = map.Value.FirstOrDefault(r => r.Contains(conversionResult));
                    if (range != null)
                    {
                        conversionResult = range.DestinationStart + (conversionResult - range.Start);
                        break;
                    }
                }
                part2Result.Add(conversionResult);
            }
        }
        Console.WriteLine($"Part 2: Lowest Location number: {part2Result.Min()}");

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
