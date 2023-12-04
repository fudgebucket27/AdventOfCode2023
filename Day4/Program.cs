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
Dictionary<string,int> cards = new Dictionary<string,int>();
foreach (var line in lines)
{
    cardNumber++;
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
    matches += 1;

    Console.WriteLine($"Card {cardNumber} Results: ");
    for (int i = 0; i < matches; i++)
    {
        int currentCard = cardNumber + i;
        var originalKey = "O:" + currentCard;
        var copyKey = "C:" + currentCard;
        if (!cards.ContainsKey(originalKey))
        {
            cards.Add(originalKey, 1); // or 1, if you want to count the card itself regardless of matches
            Console.WriteLine($"Adding original card: {currentCard}");
        }
        
        if(!cards.ContainsKey(copyKey))
        {
            cards.Add(copyKey, 1); // or 1, if you want to count the card itself regardless of matches
            Console.WriteLine($"Copying card: {currentCard}, Count={cards[copyKey]}");
        }

        if(cards.ContainsKey(copyKey))
        {
            cards[copyKey] += 1; // or 1, i
            Console.WriteLine($"Copying card: {currentCard}, Count={cards[copyKey]}");
        }
    }
}
foreach(var card in cards)
{
    Console.WriteLine($"Card: {card.Key}, Count: {card.Value}");
}
Console.WriteLine("Sum:"+cards.Sum(x=> x.Value));
