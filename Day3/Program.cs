using Day3;
using System.Data;
using System.Numerics;

var lines = File.ReadAllLines("input.txt");
var schematics = new List<Schematic>();

int x = 0;
int y= 0;

int lineCount = 0;
foreach (var line in lines)
{
    lineCount++;
    string scanned = "";
    Schematic? schematic = null;
    foreach (var character in line)
    {
        if(character == '.')
        {
            scanned = "";
            if(schematic != null)
            {
                schematics.Add(schematic);
                schematic = null;
            }
        }
        else if(character != '.' && char.IsDigit(character))
        {
            if (scanned.Count() == 0)
            {
                scanned += character;
                schematic = new Schematic()
                {
                    LineNumber = lineCount,
                    IsSymbol = false,
                    Text = scanned,
                    minX = x,
                    minY = y
                };
            }
            else
            {
                scanned += character;
                schematic.Text = scanned;
            }
       }else if (character != '.' && !char.IsDigit(character))
       {
            if(scanned.Count() > 0)
            {
                schematic = new Schematic()
                {
                    LineNumber = lineCount,
                    IsSymbol = false,
                    Text = scanned,
                    minX = x,
                    minY = y
                };
                schematics.Add(schematic);
            }
            scanned = "";
            scanned += character;
            schematic = new Schematic()
            {
                LineNumber = lineCount,
                IsSymbol = true,
                Text = scanned,
                minX = x,
                minY = y
            };     
        }
        x++;
    }
    y++;
}

foreach(var currentSchematic in schematics)
{
    Console.WriteLine("Schematic:" + currentSchematic.Text + ", Line:" +currentSchematic.LineNumber + ",X:" + currentSchematic.minX + ",Y:" + currentSchematic.minY);
    for(int i= 0; i < currentSchematic.Text.Length; i++)
    {
        int westX = currentSchematic.minX - (1+i);
        int westY = currentSchematic.minY;
        int eastX = currentSchematic.minX + (1+i);
        int eastY = currentSchematic.minY;
        int northX = currentSchematic.minX;
        int northY = currentSchematic.minY + (1+i);
        int southX = currentSchematic.minX;
        int southY = currentSchematic.minY - (1+i);
        int southWestX = currentSchematic.minX - (1 + i);
        int southWestY = currentSchematic.minY - (1 + i);
        int southEastX = currentSchematic.minX + (1 + i);
        int southEastY = currentSchematic.minY - (1 + i);
        int northWestX = currentSchematic.minX - (1 + i);
        int northWestY = currentSchematic.minY + (1 + i);
        int northEastX = currentSchematic.minX + (1 + i);
        int northEastY = currentSchematic.minY + (1 + i);

        foreach (var toMatch in schematics.Where(x=> x.IsSymbol == true && x.Text != currentSchematic.Text))
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
                Console.WriteLine($"Match found: {currentSchematic.Text} at ({currentSchematic.minX}, {currentSchematic.minY}) matches with {toMatch.Text} at ({toMatch.minX}, {toMatch.minY})");
            }
        }
    }
}