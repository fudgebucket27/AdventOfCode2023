
//Part 1
//var times = new List<long> { 7, 15, 30 };
//var distances = new List<long> { 9, 40, 200 };

var times = new List<long> { 55, 82, 64, 90 };
var distances = new List<long> { 246, 1441, 1012, 1111 };

var finalResults = new Dictionary<long, List<long>>();//
int count = 0;
foreach(var time in times)
{
    Console.WriteLine("Time:" + time);
    List<long> results = new List<long>();
    for(long i=0; i <= time; i++)
    {
        long distance = 0;
        long speed = 0;
        long remainingTime = time - i;
        distance = i * remainingTime;
        results.Add(distance);
        //Console.WriteLine(distance);
    }
    finalResults.Add(count, results);
    count++;
}

var waysToWin = new List<long>();
for(int i = 0; i < distances.Count; i++)
{
    var wins = finalResults[i].Where(x => x > distances[i]).Count();
    waysToWin.Add(wins);
}
Console.WriteLine(waysToWin.Aggregate((a, x) => a * x));