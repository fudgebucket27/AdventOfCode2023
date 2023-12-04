var lines = File.ReadAllLines("input.txt");

//Part 1
var sum = 0;
foreach (var game in lines)
{
    int gameId = Int32.Parse(game.Split(':')[0].Split(' ')[1]);
    string[] gameColourSets = game.Split(":")[1].Split(';');
    Dictionary<string,int> gameColourSetAmounts = new Dictionary<string,int>();
    var isGameValid = true;
    foreach (var gameSet in gameColourSets)
    {
        string[] colourSets = gameSet.Split(',');
        foreach (var colourSet in colourSets)
        {
            var data = colourSet.Split(" ");
            var dataAmount = Int32.Parse(data[1]);
            var colourName = data[2];
            if (gameColourSetAmounts.ContainsKey(colourName))
            {
                gameColourSetAmounts[colourName] += dataAmount;
            }
            else
            {
                gameColourSetAmounts.Add(colourName, dataAmount);
            }
        }

        if (gameColourSetAmounts.ContainsKey("red") && gameColourSetAmounts["red"] > 12)
        {
            isGameValid = false;
        }

        if (gameColourSetAmounts.ContainsKey("green") && gameColourSetAmounts["green"] > 13)
        {
            isGameValid = false;
        }

        if (gameColourSetAmounts.ContainsKey("blue") && gameColourSetAmounts["blue"] > 14)
        {
            isGameValid = false;
        }
        gameColourSetAmounts.Clear();
    }

    if (isGameValid)
    {
        sum += gameId;
    }
}
Console.WriteLine(sum);
sum = 0;
//Part 2
foreach (var game in lines)
{
    int gameId = Int32.Parse(game.Split(':')[0].Split(' ')[1]);
    string[] gameColourSets = game.Split(":")[1].Split(';');
    Dictionary<string, int> gameColourSetAmounts = new Dictionary<string, int>();
    foreach (var gameSet in gameColourSets)
    {
        string[] colourSets = gameSet.Split(',');
        foreach (var colourSet in colourSets)
        {
            var data = colourSet.Split(" ");
            var dataAmount = Int32.Parse(data[1]);
            var colourName = data[2];
            if (gameColourSetAmounts.ContainsKey(colourName))
            {
                if (dataAmount > gameColourSetAmounts[colourName])
                {
                    gameColourSetAmounts[colourName] = dataAmount;
                }
            }
            else
            {
                gameColourSetAmounts.Add(colourName, dataAmount);
            }
        }
    }

    var powers = 1;
    foreach(var gameColourSet in gameColourSetAmounts)
    {
        powers *= gameColourSet.Value;
    }
    sum += powers;
}
Console.WriteLine(sum);