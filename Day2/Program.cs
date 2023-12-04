using System.Drawing;

var lines = File.ReadAllLines("input.txt");
var sum = 0;
foreach (var game in lines)
{
    int gameId = Int32.Parse(game.Split(':')[0].Split(' ')[1]);
    string[] gameSets = game.Split(":")[1].Split(';');
    Dictionary<string,int> gameAmounts = new Dictionary<string,int>();
    var valid = true;
    foreach (var gameSet in gameSets)
    {
        string[] colourSets = gameSet.Split(',');
        foreach (var colourSet in colourSets)
        {
            var data = colourSet.Split(" ");
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

        if (gameAmounts.ContainsKey("red") && gameAmounts["red"] > 12)
        {
            valid = false;
        }

        if (gameAmounts.ContainsKey("green") && gameAmounts["green"] > 13)
        {
            valid = false;
        }

        if (gameAmounts.ContainsKey("blue") && gameAmounts["blue"] > 14)
        {
            valid = false;
        }
        gameAmounts.Clear();
    }

    if (valid)
    {
        sum += gameId;
    }
}
Console.WriteLine(sum);