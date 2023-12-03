var lines = File.ReadAllLines("input.txt");

//Part 1
var sum = 0;
foreach (var line in lines)
{
    var nums = "";
    var digits = line.Where(Char.IsDigit).ToArray();
    nums += digits[0].ToString() + digits[digits.Length - 1].ToString();
    sum += Int32.Parse(nums);
}
Console.WriteLine(sum);

//Part 2
sum = 0;
var numberStrings = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
var numberIntStrings = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
var numberInts = new List<int> {1,2,3,4,5,6,7,8,9};
foreach (var line in lines)
{
    Dictionary<int, int> numbersFound = new Dictionary<int, int>(); //key = index, value is number
    for (int i = 0; i < numberStrings.Count(); i++)
    {
        var first = line.IndexOf(numberStrings[i]);
        var last = line.LastIndexOf(numberStrings[i]);
        if (first != -1 && !numbersFound.ContainsKey(first))
        {
            numbersFound.Add(first, numberInts[i]);
        }

        if (last != -1 && !numbersFound.ContainsKey(last))
        {
            numbersFound.Add(last, numberInts[i]);
        }
    }
    for (int i = 0; i < numberIntStrings.Count(); i++)
    {
        var first = line.IndexOf(numberIntStrings[i]);
        var last = line.LastIndexOf(numberIntStrings[i]);
        if (first != -1 && !numbersFound.ContainsKey(first))
        {
            numbersFound.Add(first, numberInts[i]);
        }

        if (last != -1 && !numbersFound.ContainsKey(last))
        {
            numbersFound.Add(last, numberInts[i]);
        }
    }
    var minNumberKey = numbersFound.Keys.Min();
    var maxNumberKey = numbersFound.Keys.Max();
    var nums = numbersFound[minNumberKey].ToString() + numbersFound[maxNumberKey].ToString();
    sum += Int32.Parse(nums);
}

Console.WriteLine(sum);