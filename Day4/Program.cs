using System.Runtime.ExceptionServices;

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

points = 0;
var cardNumber = 0;
Dictionary<string,int> originals = new Dictionary<string,int>();
Dictionary<string, int> copies = new Dictionary<string, int>();
foreach (var line in lines)
{
    cardNumber++;
    var matches = 0;
    var game = line.Split(':')[1].Split('|');
    var winningNumbers = game[0].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();
    var numbersTocheck = game[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();

    foreach (var number in numbersTocheck)
    {
        foreach (var winningNumber in winningNumbers)
        {
            if (winningNumber == number)
            {
                matches += 1;
            }
        }
    }
    
    var originalKey = "O:" + cardNumber;
    if (!originals.ContainsKey(originalKey))
    {
        originals.Add(originalKey, 1); // or 1, if you want to count the card itself regardless of matches
        Console.WriteLine($"Adding original card: {cardNumber}");
    }

    Console.WriteLine($"Card {cardNumber} Results: ");
    for (int i = 0; i < matches; i++)
    {
        int currentCard = cardNumber + i;
        
        var copyKey = "C:" + currentCard;

        if (!copies.ContainsKey(copyKey) && cardNumber != 1)
        {
            copies.Add(copyKey, 1); // or 1, if you want to count the card itself regardless of matches
            Console.WriteLine($"Copying card: {currentCard}, Count={copies[copyKey]}");
        }
        else if (copies.ContainsKey(copyKey))
        {
            copies[copyKey] += 1; // or 1, i
            Console.WriteLine($"Copying card: {currentCard}, Count={copies[copyKey]}");
        }
    }
}
foreach(var card in originals)
{
    Console.WriteLine($"Card Originals: {card.Key}, Count: {card.Value}");
}

foreach (var card in copies)
{
    Console.WriteLine($"Card Copies: {card.Key}, Count: {card.Value}");
}
Console.WriteLine("Sum:"+originals.Sum(x=> x.Value));
