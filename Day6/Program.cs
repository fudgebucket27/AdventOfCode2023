
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
Stopwatch sw2 = new Stopwatch();
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
sw2.Stop();
Console.WriteLine($"Ways to win: {win2Amount}, Elapsed time: {sw2.ElapsedMilliseconds} ms");


//Part 2 This is what CHAT GPT gave me to optimize the code LMAO. Using parabolas. TIL
Stopwatch sw3 = new Stopwatch();
sw3.Start();

var part2TimesGPT = new List<long> { 55826490 };
var part2DistincesGPT = new List<long> { 246144110121111 };

var part2WaysToWinGPT = new List<long>();

foreach (var time in part2TimesGPT)
{
    long maxDistance = time * time / 4; // Maximum distance at the vertex of the parabola
    long recordDistance = part2DistincesGPT[part2WaysToWinGPT.Count];

    if (maxDistance <= recordDistance)
    {
        part2WaysToWinGPT.Add(0);
        continue;
    }

    // Binary search for lower and upper bounds
    long lowerBound = 0, upperBound = time;
    while (lowerBound < upperBound)
    {
        long mid = lowerBound + (upperBound - lowerBound) / 2;
        long distance = mid * (time - mid);

        if (distance > recordDistance)
        {
            upperBound = mid;
        }
        else
        {
            lowerBound = mid + 1;
        }
    }

    long lower = upperBound;
    upperBound = time;
    while (lowerBound < upperBound)
    {
        long mid = lowerBound + (upperBound - lowerBound) / 2;
        long distance = mid * (time - mid);

        if (distance <= recordDistance)
        {
            upperBound = mid;
        }
        else
        {
            lowerBound = mid + 1;
        }
    }

    long upper = lowerBound - 1;
    part2WaysToWinGPT.Add(upper - lower + 1);
}

var win2AmountGPT = part2WaysToWinGPT.Aggregate((a, x) => a * x);
sw3.Stop();
Console.WriteLine($"Ways to win: {win2AmountGPT}, Elapsed time: {sw3.ElapsedMilliseconds} ms");