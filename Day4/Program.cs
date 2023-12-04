var lines = File.ReadAllLines("input.txt");

//part 1
double points = 0;
foreach(var line in lines)
{
    var firstMatch = false;
    var matches = 0;
    var game = line.Split(':')[1].Split('|');
    var winningNumbers = game[0].Trim().Split(' ').Where(x=> !string.IsNullOrEmpty(x) ).ToList();
    var numbersTocheck = game[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();

    foreach(var number in numbersTocheck)
    {
       foreach(var winningNumber in winningNumbers)
        {
            if(winningNumber == number)
            {
                matches += 1;
            }
        }
    }

    if(matches > 0)
    {
        var power = matches - (double)1;
        points += Math.Pow((double)2, power);
    }
}
Console.WriteLine(points);

