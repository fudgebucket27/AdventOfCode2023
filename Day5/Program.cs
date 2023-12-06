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


        //Part 2 DOESNT WORK GG
        string[] lines = File.ReadAllLines("input.txt");
        var seedRanges = ParseSeedRanges(lines[0]);
        var parsedmaps = ParseMaps(lines.Skip(1).ToArray());

        long minLocation = long.MaxValue;

        foreach (var (start, length) in seedRanges)
        {
            long i = start;
            while (i < start + length)
            {
                long stop = CalculateStop(i, parsedmaps);
                long location = MapSeedToLocation(i, parsedmaps);
                minLocation = Math.Min(minLocation, location);
                i += stop;
            }
        }

        Console.WriteLine($"The lowest location number is: {minLocation}");


    }

    static long CalculateStop(long seed, List<Dictionary<long, (long destStart, long srcStart, long rangeLength)>> maps)
    {
        long stop = long.MaxValue;

        foreach (var map in maps)
        {
            long localStop = long.MaxValue;
            foreach (var entry in map)
            {
                var (destStart, srcStart, rangeLength) = entry.Value;
                if (seed >= srcStart && seed < srcStart + rangeLength)
                {
                    localStop = rangeLength - (seed - srcStart);
                    break;
                }
                else if (srcStart > seed)
                {
                    localStop = srcStart - seed;
                    break;
                }
            }

            stop = Math.Min(stop, localStop);
        }

        return stop;
    }


    static List<(long start, long length)> ParseSeedRanges(string line)
    {
        var parts = line.Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
        var ranges = new List<(long start, long length)>();
        for (long i = 1; i < parts.Length; i += 2)
        {
            ranges.Add((long.Parse(parts[i]), long.Parse(parts[i + 1])));
        }
        return ranges;
    }

    // Adjusted to return the correct type for maps
    static List<Dictionary<long, (long destStart, long srcStart, long rangeLength)>> ParseMaps(string[] lines)
    {
        var maps = new List<Dictionary<long, (long destStart, long srcStart, long rangeLength)>>();
        Dictionary<long, (long, long, long)> currentMap = null;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.Contains("map:"))
            {
                if (currentMap != null)
                {
                    maps.Add(currentMap);
                }
                currentMap = new Dictionary<long, (long, long, long)>();
            }
            else
            {
                var parts = line.Split(' ').Select(long.Parse).ToArray();
                if (parts.Length == 4)
                {
                    long destStart = parts[0];
                    long srcStart = parts[1];
                    long rangeLength = parts[3];
                    for (long i = 0; i < rangeLength; i++)
                    {
                        currentMap[srcStart + i] = (destStart + i, srcStart, rangeLength);
                    }
                }
                else
                {
                    // Handle error or invalid line format
                    //Console.WriteLine($"Invalid line format: {line}");
                }
            }
        }
        if (currentMap != null)
        {
            maps.Add(currentMap); // Add the last map
        }

        return maps;
    }


    static long MapSeedToLocation(long seed, List<Dictionary<long, (long destStart, long srcStart, long rangeLength)>> maps)
    {
        long currentNumber = seed;

        foreach (var map in maps)
        {
            currentNumber = MapNumber(currentNumber, map);
        }

        return currentNumber;
    }

    static long MapNumber(long number, Dictionary<long, (long destStart, long srcStart, long rangeLength)> map)
    {
        if (map.TryGetValue(number, out var entry))
        {
            var (destStart, srcStart, rangeLength) = entry;
            if (number >= srcStart && number < srcStart + rangeLength)
            {
                return destStart + (number - srcStart);
            }
        }

        return number; // If the number isn't in the map, it maps to itself
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
