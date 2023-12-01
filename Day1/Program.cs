using Microsoft.VisualBasic;

var lines = File.ReadAllLines("input.txt");
var sum = 0;
//foreach (var line in lines)
//{
//    var nums = "";
//    var digits = line.Where(Char.IsDigit).ToArray();
//    nums += digits[0].ToString() + digits[digits.Length - 1].ToString();
//    sum += Int32.Parse(nums);
//}

sum = 0;
var numberStrings = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
var numberIntStrings = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
var numberInts = new List<int> {1,2,3,4,5,6,7,8,9};
foreach (var line in lines)
{
    Console.WriteLine("Current Line:" + line);
    var maxNumberStringIndex = -1;
    var minNumberStringIndex = -1;
    var minNumberStringInt = -1;
    var maxNumberStringInt = -1;
    int count = 0;
    for(int i=0; i < numberStrings.Count(); i++) 
    {
        var first = line.IndexOf(numberStrings[i]);
        var last = line.LastIndexOf(numberStrings[i]);
        Console.WriteLine($"Current num: {numberStrings[i]}, First: {first}, Last: {last}");
        if(first <= last)
        {
            minNumberStringIndex = first;
            minNumberStringInt = numberInts[i];
        }

        if(last >= first)
        {
            maxNumberStringIndex = last;
            maxNumberStringInt = numberInts[i];
        }
        Console.WriteLine($"{minNumberStringInt},{maxNumberStringInt}");
    }
    Console.WriteLine($"FINAL {minNumberStringInt}{maxNumberStringInt}");

}

Console.WriteLine(sum);