using Day7;
using System.Numerics;

////Part 1 - DONE
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
    bool isFullHouse = groups.Count == 2 && ((groups[0].Count == 3 && groups[1].Count == 2) || (groups[0].Count == 2 && groups[1].Count == 3));
    bool isThreeOfAKind = groups.Count == 3 && groups.Any(g => g.Count == 3);
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
    totalWinnings += hand.Bid * rank;
}

Console.WriteLine(totalWinnings);

//Part 2 TO DO
var part2Strengths = new Dictionary<char, int>()
{
    { 'A', 14 },
    { 'K', 13 },
    { 'Q', 12 },
    { 'T', 11 },
    { '9', 10 },
    { '8', 9 },
    { '7', 8 },
    { '6', 7 },
    { '5', 6 },
    { '4', 5 },
    { '3', 4 },
    { '2', 3 },
    { 'J', 2 }
};

List<Hand> part2Hands = new List<Hand>();
foreach (var line in input)
{
    var parts = line.Split(' ');
    var groups = parts[0].GroupBy(c => c)
                     .Select(group => new { Label = group.Key, Count = group.Count() })
                     .ToList();

    int jokerCount = parts[0].Count(c => c == 'J');

    bool isFiveOfAKind = groups.Any(g => g.Count + jokerCount >= 5);

    bool isFourOfAKind = !isFiveOfAKind && (groups.Any(g => g.Count + jokerCount == 4));

    bool isFullHouse = !isFourOfAKind && ((groups.Any(g => g.Count == 3) && jokerCount >= 1) ||
                                          (groups.Count(g => g.Count == 2) == 2) ||
                                          (groups.Any(g => g.Count == 2) && jokerCount >= 2));

    bool isThreeOfAKind = !isFullHouse && (groups.Any(g => g.Count + jokerCount == 3));

    bool isTwoPair = !isThreeOfAKind && (groups.Count(g => g.Count == 2) + (jokerCount >= 1 ? 1 : 0) == 2);

    bool isOnePair = !isTwoPair && (groups.Count(g => g.Count == 2) + (jokerCount >= 1 ? 1 : 0) == 1);

    bool isHighCard = !isOnePair && groups.All(g => g.Count == 1);



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
    hand.Strength1 = part2Strengths[parts[0][0]];
    hand.Strength2 = part2Strengths[parts[0][1]];
    hand.Strength3 = part2Strengths[parts[0][2]];
    hand.Strength4 = part2Strengths[parts[0][3]];
    hand.Strength5 = part2Strengths[parts[0][4]];
    part2Hands.Add(hand);
}

var part2SortedHands = part2Hands.OrderBy(h => h.OverallStrength)
                       .ThenBy(h => h.Strength1)
                       .ThenBy(h => h.Strength2)
                       .ThenBy(h => h.Strength3)
                       .ThenBy(h => h.Strength4)
                       .ThenBy(h => h.Strength5)
                       .ToList();

var part2Winnings = 0;
var part2Rank = 0;
foreach (var hand in part2SortedHands)
{
    part2Rank++;
    part2Winnings += hand.Bid * part2Rank;
}
Console.WriteLine(part2Winnings);