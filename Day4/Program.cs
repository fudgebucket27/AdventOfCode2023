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
Dictionary<int,int> cards = new Dictionary<int,int>();
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

    if(matches == 0) //if no matches count original 
    {
        matches = 1;
    }

    Console.WriteLine($"Card {cardNumber} Results: ");
    for (int i = 0; i <= matches; i++)
    {
        int currentCard = cardNumber + i;

        if (!cards.ContainsKey(currentCard))
        {
            cards.Add(currentCard, 1); // or 1, if you want to count the card itself regardless of matches
            Console.WriteLine($"Adding original card: {currentCard}");
        }
        else
        {
            cards[currentCard] += 1; // or increment by 1, depending on your requirements
            Console.WriteLine($"Updating card: {currentCard}");
        }
    }
}
Console.WriteLine(cards.Sum(x=> x.Value));
