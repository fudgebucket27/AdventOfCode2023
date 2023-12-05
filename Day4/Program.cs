using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.JavaScript;

var lines = File.ReadAllLines("input.txt");

//part 1
double points = 0;
foreach (var line in lines)
{
    var firstMatch = false;
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

    if (matches > 0)
    {
        var power = matches - (double)1;
        points += Math.Pow((double)2, power);
    }
}
Console.WriteLine(points);

points = 0;
var cardNumber = 0;
Dictionary<int, int> cards = new Dictionary<int, int>();
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

    matches += 1;

    var key = cardNumber;
    if (!cards.ContainsKey(key))
    {
        cards.Add(key, 1); // or 1, if you want to count the card itself regardless of matches

    }
    else
    {
        cards[key] += 1;
    }
    Console.WriteLine($"Adding original card: {cardNumber}");

    Console.WriteLine($"Card {cardNumber} Results: ");
    for (int i = 1; i < matches; i++)
    {
        int currentCard = cardNumber + i;

        var currentKey = currentCard;

        if (cardNumber == 1)
        {
            if (!cards.ContainsKey(currentKey))
            {
                cards.Add(currentKey, 1); // or 1, if you want to count the card itself regardless of matches
                Console.WriteLine($"Copying card: {currentCard}, Count={cards[currentKey]}");
            }
            else if (cards.ContainsKey(currentKey))
            {
                cards[currentKey] += cards[currentKey]; // or 1, i
                Console.WriteLine($"Copying card: {currentCard}, Count={cards[currentKey]}");
            }
        }
        else
        {

            if (!cards.ContainsKey(currentKey))
            {
                cards.Add(currentKey, 1); // or 1, if you want to count the card itself regardless of matches
                Console.WriteLine($"Copying card 1: {currentCard}, Count={cards[currentKey]}");
            }
            else if (cards.ContainsKey(currentKey))
            {
                int currentCount = cards[currentKey];
                cards[currentKey] += currentCount; // or 1, i
                Console.WriteLine($"Copying card 2: {currentCard}, Count={cards[currentKey]}");
            }
        }
    }
}
foreach (var card in cards)
{
    Console.WriteLine($"Cards: {card.Key}, Count: {card.Value}");
}

Console.WriteLine("Sum:" + cards.Sum(x => x.Value));
