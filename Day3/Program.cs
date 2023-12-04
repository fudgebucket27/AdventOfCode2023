using Day3;
using System.Data;

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
                    ScehmaticText = scanned,
                    minX = x,
                    minY = y
                };
            }
            else
            {
                scanned += character;
                schematic.ScehmaticText = scanned;
            }
       }else if (character != '.' && !char.IsDigit(character))
       {
            if(scanned.Count() > 0)
            {
                schematic = new Schematic()
                {
                    LineNumber = lineCount,
                    IsSymbol = false,
                    ScehmaticText = scanned,
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
                ScehmaticText = scanned,
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
    Console.WriteLine("Schematic:" + currentSchematic.ScehmaticText + ", Line:" +currentSchematic.LineNumber + ",X:" + currentSchematic.minX + ",Y:" + currentSchematic.minY);
    for(int i= 0; i < currentSchematic.ScehmaticText.Length; i++)
    {
        foreach(var toMatch in schematics.Where(x=> x.IsSymbol == true && x.ScehmaticText != currentSchematic.ScehmaticText))
        {
            if(currentSchematic.minX - i  == toMatch.minX || currentSchematic.minY - i == toMatch.minY || currentSchematic.minX + i == toMatch.minX || currentSchematic.minY + i == toMatch.minY)
            {
                Console.WriteLine($"Schematic: {currentSchematic.ScehmaticText}, Adjacent to: {toMatch.ScehmaticText}");
            }
        }
    }
}