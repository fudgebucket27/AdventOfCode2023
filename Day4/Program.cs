using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.JavaScript;

var lines = File.ReadAllLines("input.txt");

Part 1
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

//Part 2
Dictionary<int, int> cardCounts = new Dictionary<int, int>();
Queue<int> cardsToProcess = new Queue<int>();

int count = 0;
foreach (var line in lines)
{
    count++;
    var parts = line.Split(':');
    int cardNumber = count;
    cardCounts[cardNumber] = 1; // Each card starts with one count
    cardsToProcess.Enqueue(cardNumber);
}

while (cardsToProcess.Count > 0)
{
    int cardNumber = cardsToProcess.Dequeue();
    var line = lines[cardNumber - 1]; // Assuming card numbers start from 1
    var game = line.Split(':')[1].Split('|');
    var winningNumbers = new HashSet<string>(game[0].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)));
    var numbersToCheck = game[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x));

    int matches = numbersToCheck.Count(number => winningNumbers.Contains(number));

    for (int i = 1; i <= matches; i++)
    {
        int nextCard = cardNumber + i;
        if (nextCard <= lines.Count()) // Ensure we don't go past the last card
        {
            if (!cardCounts.ContainsKey(nextCard))
            {
                cardCounts[nextCard] = 1;
            }
            else
            {
                cardCounts[nextCard]++;
            }

            cardsToProcess.Enqueue(nextCard);
        }
    }
}

int totalCards = cardCounts.Values.Sum();
Console.WriteLine($"Total number of scratchcards: {totalCards}");

