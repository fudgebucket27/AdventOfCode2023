using Day3;
using System.Data;
using System.Numerics;

var lines = File.ReadAllLines("input.txt");
var schematics = new List<Schematic>();

int lineCount = 0;
foreach (var line in lines)
{
    lineCount++;
    int x = 0;
    string scanned = "";
    Schematic? schematic = null;

    foreach (var character in line)
    {
        if (character == '.')
        {
            // End the current schematic and reset scanned when encountering a period
            if (schematic != null)
            {
                schematics.Add(schematic);
                schematic = null;
            }
            scanned = "";
        }
        else if (!char.IsDigit(character))
        {
            // When encountering a symbol, end the current schematic and start a new one
            if (schematic != null)
            {
                schematics.Add(schematic);
            }
            scanned = character.ToString();
            var symbolSchematic = new Schematic()
            {
                LineNumber = lineCount,
                IsSymbol = true,
                Text = scanned,
                minX = x,
                minY = lineCount
            };
            schematics.Add(symbolSchematic); // Add symbol schematic immediately
            schematic = null; // Reset schematic for potentially new number
            scanned = "";
        }
        else
        {
            // Continue accumulating digits into the current schematic
            scanned += character;
            if (schematic == null)
            {
                // Start a new schematic if it's the beginning of a number
                schematic = new Schematic()
                {
                    LineNumber = lineCount,
                    IsSymbol = false,
                    Text = scanned,
                    minX = x,
                    minY = lineCount
                };
            }
            else
            {
                schematic.Text = scanned; // Continue adding to existing schematic
            }
        }

        x++;
    }

    // Add the last schematic if it exists and has content
    if (schematic != null && schematic.Text != "")
    {
        schematics.Add(schematic);
    }
}

//Part 1
//Dictionary<string, int>  matched = new Dictionary<string, int>();
//foreach (var currentSchematic in schematics.Where(x => x.IsSymbol == false))
//{
//    //Console.WriteLine("Schematic:" + currentSchematic.Text + ", Line:" + currentSchematic.LineNumber + ", X:" + currentSchematic.minX + ", Y:" + currentSchematic.minY);

//    for (int i = 0; i < currentSchematic.Text.Length; i++)
//    {
//        var currentPosX = currentSchematic.minX + i;
//        var currentPosY = currentSchematic.minY;

//        int westX = currentPosX - 1;
//        int westY = currentPosY;

//        int eastX = currentPosX + 1;
//        int eastY = currentPosY;

//        int northX = currentPosX;
//        int northY = currentPosY - 1;

//        int southX = currentPosX;
//        int southY = currentPosY + 1;

//        int southWestX = currentPosX - 1;
//        int southWestY = currentPosY + 1;

//        int southEastX = currentPosX + 1;
//        int southEastY = currentPosY + 1;

//        int northWestX = currentPosX - 1;
//        int northWestY = currentPosY - 1;

//        int northEastX = currentPosX + 1;
//        int northEastY = currentPosY - 1;

//        foreach (var toMatch in schematics.Where(x => x.IsSymbol == true))
//        {
//            if ((toMatch.minX == westX && toMatch.minY == westY) ||
//                (toMatch.minX == eastX && toMatch.minY == eastY) ||
//                (toMatch.minX == northX && toMatch.minY == northY) ||
//                (toMatch.minX == southX && toMatch.minY == southY) ||
//                (toMatch.minX == southWestX && toMatch.minY == southWestY) ||
//                (toMatch.minX == southEastX && toMatch.minY == southEastY) ||
//                (toMatch.minX == northWestX && toMatch.minY == northWestY) ||
//                (toMatch.minX == northEastX && toMatch.minY == northEastY))
//            {
//                var key = $"{currentSchematic.Text},{currentSchematic.minX},{currentSchematic.minY}";
//                if (!matched.ContainsKey(key))
//                {
//                    matched.Add(key, Int32.Parse(currentSchematic.Text));
//                }
//                Console.WriteLine($"Match found: {currentSchematic.Text} at ({currentSchematic.minX}, {currentSchematic.LineNumber}) matches with {toMatch.Text} at ({toMatch.minX}, {toMatch.minY})");
//            }
//        }
//    }
//}
//var sum = 0;
//foreach(var match in matched)
//{
//    sum += match.Value;
//}
//Console.WriteLine(sum);

//Part 2
Dictionary<string, int> partsDictionary = new Dictionary<string, int>();
Dictionary<string, (int x, int y)> symbolsDictionary = new Dictionary<string, (int x, int y)>();
foreach (var currentSchematic in schematics.Where(x => x.IsSymbol == false))
{
    //Console.WriteLine("Schematic:" + currentSchematic.Text + ", Line:" + currentSchematic.LineNumber + ", X:" + currentSchematic.minX + ", Y:" + currentSchematic.minY);

    for (int i = 0; i < currentSchematic.Text.Length; i++)
    {
        var currentPosX = currentSchematic.minX + i;
        var currentPosY = currentSchematic.minY;

        int westX = currentPosX - 1;
        int westY = currentPosY;

        int eastX = currentPosX + 1;
        int eastY = currentPosY;

        int northX = currentPosX;
        int northY = currentPosY - 1;

        int southX = currentPosX;
        int southY = currentPosY + 1;

        int southWestX = currentPosX - 1;
        int southWestY = currentPosY + 1;

        int southEastX = currentPosX + 1;
        int southEastY = currentPosY + 1;

        int northWestX = currentPosX - 1;
        int northWestY = currentPosY - 1;

        int northEastX = currentPosX + 1;
        int northEastY = currentPosY - 1;

        foreach (var toMatch in schematics.Where(x => x.IsSymbol == true))
        {
            if ((toMatch.minX == westX && toMatch.minY == westY) ||
                (toMatch.minX == eastX && toMatch.minY == eastY) ||
                (toMatch.minX == northX && toMatch.minY == northY) ||
                (toMatch.minX == southX && toMatch.minY == southY) ||
                (toMatch.minX == southWestX && toMatch.minY == southWestY) ||
                (toMatch.minX == southEastX && toMatch.minY == southEastY) ||
                (toMatch.minX == northWestX && toMatch.minY == northWestY) ||
                (toMatch.minX == northEastX && toMatch.minY == northEastY))
            {
                var partsKey = $"{currentSchematic.Text},{currentSchematic.minX},{currentSchematic.minY}";
                if (!partsDictionary.ContainsKey(partsKey))
                {
                    partsDictionary.Add(partsKey, Int32.Parse(currentSchematic.Text));
                }

                var symbolsKey = $"{toMatch.Text},{toMatch.minX},{toMatch.minY}";
                if (!symbolsDictionary.ContainsKey(symbolsKey))
                {
                    symbolsDictionary.Add(symbolsKey, (toMatch.minX, toMatch.minY));
                }
                Console.WriteLine($"Match found: {currentSchematic.Text} at ({currentSchematic.minX}, {currentSchematic.LineNumber}) matches with {toMatch.Text} at ({toMatch.minX}, {toMatch.minY})");
            }
        }
    }
}
