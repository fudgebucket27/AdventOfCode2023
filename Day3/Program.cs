using Day3;
using System.Data;
using System.Numerics;

var lines = File.ReadAllLines("input.txt");
var schematics = new List<Schematic>();

int lineCount = 0;
foreach (var line in lines)
{
    lineCount++;
    int x = 0; // Reset x for each line
    string scanned = "";
    Schematic? schematic = null;
    foreach (var character in line)
    {
        if (character == '.')
        {
            scanned = "";
            if (schematic != null)
            {
                schematics.Add(schematic);
                schematic = null;
            }
        }
        else
        {
            if (scanned.Count() == 0 || !char.IsDigit(character))
            {
                if (scanned.Count() > 0)
                {
                    schematics.Add(schematic);
                }
                scanned = character.ToString();
                schematic = new Schematic()
                {
                    LineNumber = lineCount,
                    IsSymbol = !char.IsDigit(character),
                    Text = scanned,
                    minX = x,
                    minY = lineCount // Using lineCount as y-coordinate
                };
            }
            else
            {
                scanned += character;
                schematic.Text = scanned;
            }
        }
        x++;
    }
    if (scanned.Count() > 0)
    {
        schematics.Add(schematic);
    }
}
Dictionary<string, int>  matched = new Dictionary<string, int>();
foreach (var currentSchematic in schematics.Where(x => x.IsSymbol == false))
{
    Console.WriteLine("Schematic:" + currentSchematic.Text + ", Line:" + currentSchematic.LineNumber + ", X:" + currentSchematic.minX + ", Y:" + currentSchematic.minY);

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
            if ((toMatch.minX == westX && toMatch.LineNumber == westY) ||
                (toMatch.minX == eastX && toMatch.LineNumber == eastY) ||
                (toMatch.minX == northX && toMatch.LineNumber == northY) ||
                (toMatch.minX == southX && toMatch.LineNumber == southY) ||
                (toMatch.minX == southWestX && toMatch.LineNumber == southWestY) ||
                (toMatch.minX == southEastX && toMatch.LineNumber == southEastY) ||
                (toMatch.minX == northWestX && toMatch.LineNumber == northWestY) ||
                (toMatch.minX == northEastX && toMatch.LineNumber == northEastY))
            {
                var key = $"{currentSchematic.minX},{currentSchematic.minY}";
                if(!matched.ContainsKey(key))
                {
                    matched.Add(key, Int32.Parse(currentSchematic.Text));
                }
                Console.WriteLine($"Match found: {currentSchematic.Text} at ({currentSchematic.minX}, {currentSchematic.LineNumber}) matches with {toMatch.Text} at ({toMatch.minX}, {toMatch.LineNumber})");
            }
        }
    }
}
var sum = 0;
foreach(var match in matched)
{
    sum += match.Value;
}
Console.WriteLine(sum);