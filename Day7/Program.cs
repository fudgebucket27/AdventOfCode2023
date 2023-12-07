using Day7;
using System.Numerics;

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

List<Hand> hands = new List<Hand>();
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

    var handOverallStrength = 0;
    if (isFiveOfAKind)
    {
        handOverallStrength += 7;
    }
    else if (isFourOfAKind)
    {
        handOverallStrength += 6;
    }
    else if (isFullHouse)
    {
        handOverallStrength += 5;
    }
    else if (isThreeOfAKind)
    {
        handOverallStrength += 4;
    }
    else if (isTwoPair)
    {
        handOverallStrength += 3;
    }
    else if (isOnePair)
    {
        handOverallStrength += 2;
    }
    else if (isHighCard)
    {
        handOverallStrength += 1;

    }

    Hand hand = new Hand();
    hand.Cards = parts[0];
    hand.Bid = Int32.Parse(parts[1]);
    hand.OverallStrength = handOverallStrength;
    hand.Strength1 = strengths[parts[0][0]];
    hand.Strength2 = strengths[parts[0][1]];
    hand.Strength3 = strengths[parts[0][2]];
    hand.Strength4 = strengths[parts[0][3]];
    hand.Strength5 = strengths[parts[0][4]];
    hands.Add(hand);
}

var sortedHands = hands.OrderBy(h => h.OverallStrength)
                       .ThenBy(h => h.Strength1)
                       .ThenBy(h => h.Strength2)
                       .ThenBy(h => h.Strength3)
                       .ThenBy(h => h.Strength4)
                       .ThenBy(h => h.Strength5)
                       .ToList();

var totalWinnings = 0;
var rank = 0;
foreach (var hand in sortedHands)
{
    rank++;
    //Console.WriteLine($"Cards: {hand.Cards}, Bid: {hand.Bid}, OverallStrength: {hand.OverallStrength}, Strength1: {hand.Strength1}, Strength2: {hand.Strength2}, Strength3: {hand.Strength3}, Strength4: {hand.Strength4}, Strength5: {hand.Strength5}");
    totalWinnings += hand.Bid * rank;
}

Console.WriteLine(totalWinnings);