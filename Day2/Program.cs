using System.Drawing;

var lines = File.ReadAllLines("input.txt");
var sum = 0;
foreach (var line in lines)
{
    int gameId = Int32.Parse(line.Split(':')[0].Split(' ')[1]);
    string[] sets = line.Split(":")[1].Split(';');
    Dictionary<string,int> colourAmounts = new Dictionary<string,int>();
    foreach(var set in sets)
    {
        string[] colours = set.Split(',');
        foreach (var colour in colours)
        {
            var data = colour.Split(" ");
            var dataAmount = Int32.Parse(data[1]);
            var colourName = data[2];
            if (colourAmounts.ContainsKey(colourName))
            {
                colourAmounts[colourName] += dataAmount;
            }
            else
            {
                colourAmounts.Add(colourName, dataAmount);
            }
        }
    }

    var valid = 0;
    if(colourAmounts.ContainsKey("red"))
    {
        if (colourAmounts["red"] <= 12)
        {
            valid++;
        }
    }

    if (colourAmounts.ContainsKey("green"))
    {
        if (colourAmounts["green"] <= 13)
        {
            valid++;
        }
    }

    if (colourAmounts.ContainsKey("blue"))
    {
        if (colourAmounts["blue"] <= 14)
        {
            valid++;
        }
    }

    if(valid == 3)
    {
        sum += gameId;
    }
}
Console.WriteLine(sum);