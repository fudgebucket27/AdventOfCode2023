var input = File.ReadAllLines("input.txt");

var strengths = new Dictionary<char, int>()
{
    { 'A', 14 },
    { 'K', 13 },
    { 'Q', 12 },
    { 'J', 11 },
    { 'T', 10 },
    { '9', 9 },
    { '8', 8 },
    { '7', 7 },
    { '6', 6 },
    { '5', 5 },
    { '4', 4 },
    { '3', 3 },
    { '2', 2 },
};

List<(string, int, int)> hands = new List<(string, int, int)>();
foreach (var line in input)
{
    var parts = line.Split(' ');
    var groups = parts[0].GroupBy(c => c)
                     .Select(group => new { Label = group.Key, Count = group.Count() })
                     .ToList();

    bool isFiveOfAKind = groups.Any(g => g.Count == 5);
    bool isFourOfAKind = groups.Any(g => g.Count == 4);
    bool isFullHouse = groups.Count == 2 && groups[0].Count == 2 && groups[1].Count == 3;
    bool isThreeOfAKind = groups.Any(g => g.Count == 3);
    bool isTwoPair = groups.Count(g => g.Count == 2) == 2;
    bool isOnePair = groups.Count(g => g.Count == 2) == 1 && groups.Count(g => g.Count == 1) == 3;
    bool isHighCard = groups.All(g => g.Count == 1);

    var handScore = 0;
    if (isFiveOfAKind)
    {
        handScore += 7;
    }
    else if (isFourOfAKind)
    {
        handScore += 6;
    }
    else if (isFullHouse)
    {
        handScore += 5;
    }
    else if (isThreeOfAKind)
    {
        handScore += 4;
    }
    else if (isTwoPair)
    {
        handScore += 3;
    }
    else if (isOnePair)
    {
        handScore += 2;
    }
    else if (isHighCard)
    {
        handScore += 1;

    }

    foreach (var card in parts[0])
    {
        handScore += strengths[card];
    }

    hands.Add((parts[0], Int32.Parse(parts[1]), handScore));
}

hands.Sort((x, y) => x.Item2.CompareTo(y.Item2));
Console.WriteLine("END");