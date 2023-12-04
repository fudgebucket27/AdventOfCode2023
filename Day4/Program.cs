var lines = File.ReadAllLines("input.txt");
var points = 0;
foreach(var line in lines)
{
    var game = line.Split(':')[1].Split('|');
    var winningNumbers = game[0].Trim().Split(' ').Where(x=> !string.IsNullOrEmpty(x) ).ToList();
    var numbersTocheck = game[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();
}