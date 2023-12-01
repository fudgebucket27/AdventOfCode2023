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
var numberInts = new List<int> {1,2,3,4,5,6,7,8,9};
foreach (var line in lines)
{
    var maxNumberStringIndex = -1;
    var minNumberStringIndex = -1;
    var minNumberStringInt = -1;
    var maxNumberStringInt = -1;
    int count = 0;
    for(int i=0; i < numberStrings.Count(); i++)
    {
        if (line.IndexOf(numberStrings[i]) >= 0 && count == 0)
        {
            maxNumberStringIndex = line.IndexOf(numberStrings[i]);
            minNumberStringInt = numberInts[i];
            count++;
        }

        if(line.LastIndexOf(numberStrings[i]) > maxNumberStringIndex)
        {
            minNumberStringIndex = line.LastIndexOf(numberStrings[i]);
            maxNumberStringInt = numberInts[i];
        }
    }
    Console.WriteLine($"{minNumberStringInt}{maxNumberStringInt}");

}

Console.WriteLine(sum);