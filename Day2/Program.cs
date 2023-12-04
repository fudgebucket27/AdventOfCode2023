using System.Drawing;

var lines = File.ReadAllLines("input.txt");
var sum = 0;
foreach (var game in lines)
{
    int gameId = Int32.Parse(game.Split(':')[0].Split(' ')[1]);
    string[] gameSets = game.Split(":")[1].Split(';');
    Dictionary<string,int> gameAmounts = new Dictionary<string,int>();
    var amountOfColourSets = gameSets.Length;
    var validColourSets = 0;
    foreach(var set in gameSets)
    {
        string[] colours = set.Split(',');
        foreach (var colour in colours)
        {
            var data = colour.Split(" ");
            var dataAmount = Int32.Parse(data[1]);
            var colourName = data[2];
            if (gameAmounts.ContainsKey(colourName))
            {
                gameAmounts[colourName] += dataAmount;
            }
            else
            {
                gameAmounts.Add(colourName, dataAmount);
            }
        }

        var valid = 0;
        if (gameAmounts.ContainsKey("red"))
        {
            if (gameAmounts["red"] <= 12)
            {
                valid++;
            }
        }

        if (gameAmounts.ContainsKey("green"))
        {
            if (gameAmounts["green"] <= 13)
            {
                valid++;
            }
        }

        if (gameAmounts.ContainsKey("blue"))
        {
            if (gameAmounts["blue"] <= 14)
            {
                valid++;
            }
        }

        if (valid == 3)
        {
            validColourSets++;
        }
        gameAmounts.Clear();
    }

    if(validColourSets == amountOfColourSets)
    {
        sum += gameId;
    }
}
Console.WriteLine(sum);