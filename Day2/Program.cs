var lines = File.ReadAllLines("input.txt");
foreach (var line in lines)
{
    int gameId = Int32.Parse(line.Split(':')[0].Split(' ')[1]);
    Console.WriteLine(gameId);
}