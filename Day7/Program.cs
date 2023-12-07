var input = File.ReadAllLines("input.txt");

List<Tuple<string, int>> hands = new List<Tuple<string, int>>();
foreach(var line in input)
{
    var parts = line.Split(' ');
    var handValues = Tuple.Create(parts[0], Int32.Parse(parts[1]));
    hands.Add(handValues);
}
Console.WriteLine("BRA");