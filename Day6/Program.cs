
//Part 1
using System.Diagnostics;

Stopwatch sw = new Stopwatch();
sw.Start();
var times = new List<long> { 55, 82, 64, 90 };
var distances = new List<long> { 246, 1441, 1012, 1111 };

var finalResults = new Dictionary<long, List<long>>();//
int count = 0;
foreach (var time in times)
{
    List<long> results = new List<long>();
    for (long i = 0; i <= time; i++)
    {
        long distance = 0;
        long speed = 0;
        long remainingTime = time - i;
        distance = i * remainingTime;
        results.Add(distance);
    }
    finalResults.Add(count, results);
    count++;
}

var waysToWin = new List<long>();
for (int i = 0; i < distances.Count; i++)
{
    var wins = finalResults[i].Where(x => x > distances[i]).Count();
    waysToWin.Add(wins);
}
var winAmount = waysToWin.Aggregate((a, x) => a * x);
sw.Stop();
Console.WriteLine($"Ways to win: {winAmount}, Elapsed time: {sw.ElapsedMilliseconds} ms");

//Part 2
Stopwatch sw2  =new Stopwatch();
sw2.Start();
var part2Times = new List<long> { 55826490 };
var part2Distances = new List<long> { 246144110121111 };

var part2FinalResults = new Dictionary<long, List<long>>();//
int part2Count = 0;
foreach (var time in part2Times)
{
    List<long> results = new List<long>();
    for (long i = 0; i <= time; i++)
    {
        long distance = 0;
        long speed = 0;
        long remainingTime = time - i;
        distance = i * remainingTime;
        results.Add(distance);
    }
    part2FinalResults.Add(part2Count, results);
    part2Count++;
}

var part2WaysToWin = new List<long>();
for (int i = 0; i < part2Distances.Count; i++)
{
    var wins = part2FinalResults[i].Where(x => x > part2Distances[i]).Count();
    part2WaysToWin.Add(wins);
}
var win2Amount = part2WaysToWin.Aggregate((a, x) => a * x);
Console.WriteLine($"Ways to win: {win2Amount}, Elapsed time: {sw2.ElapsedMilliseconds} ms");