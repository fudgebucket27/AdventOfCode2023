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
                    X = x,
                    Y = y
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
                    X = x,
                    Y = y
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
                X = x,
                Y = y
            };     
        }
        x++;
    }
    y++;
}

foreach(var schematic in schematics)
{
    Console.WriteLine("Schematic:" + schematic.ScehmaticText + ", Line:" +schematic.LineNumber + ",X:" + schematic.X + ",Y:" + schematic.Y);
}