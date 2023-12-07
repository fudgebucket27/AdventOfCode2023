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
Console.WriteLine("BRA");

