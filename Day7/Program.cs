var input = File.ReadAllLines("input.txt");

var strengths = new Dictionary<string, int>()
{
    { "A", 14 },
    { "K", 13 },
    { "Q", 12 },
    { "J", 11 },
    { "T", 10 },
    { "9", 9 },
    { "8", 8 },
    { "7", 7 },
    { "6", 6 },
    { "5", 5 },
    { "4", 4 },
    { "3", 3 },
    { "2", 2 },
};

List<Tuple<string, int>> hands = new List<Tuple<string, int>>();
foreach (var line in input)
{
    var parts = line.Split(' ');
    var handValues = Tuple.Create(parts[0], Int32.Parse(parts[1]));
    hands.Add(handValues);
}

foreach (var hand in hands)
{
    var cards = hand.Item1;
    var bid = hand.Item2;
    var groups = cards.GroupBy(c => c)
                         .Select(group => new { Label = group.Key, Count = group.Count() })
                         .ToList();

    bool isFiveOfAKind = groups.Any(g => g.Count == 5);
    bool isFourOfAKind = groups.Any(g => g.Count == 4);
    bool isFullHouse = groups.Count == 2 && groups[0].Count == 2 && groups[1].Count == 3;
    bool isThreeOfAKind = groups.Any(g => g.Count == 3);
    bool isTwoPair = groups.Count(g => g.Count == 2) == 2;
    bool isOnePair = groups.Count(g => g.Count == 2) == 1 && groups.Count(g => g.Count == 1) == 3;
    bool isHighCard = groups.All(g => g.Count == 1);
}
