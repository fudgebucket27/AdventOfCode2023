var times = new List<long> { 7, 15, 30 };
var distances = new List<long> { 9, 40, 200 };

foreach(var time in times)
{
    Console.WriteLine("Time:" + time);
    
    for(long i=0; i <= time; i++)
    {
        long distance = 0;
        long speed = 0;
        long remainingTime = time - i;
        distance = i * remainingTime;
        Console.WriteLine(distance);
    }
}