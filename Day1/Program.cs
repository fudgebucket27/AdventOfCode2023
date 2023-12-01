var lines = File.ReadAllLines("input.txt");
var sum = 0;
foreach (var line in lines)
{
    var nums = "";
    var digits = line.Where(Char.IsDigit).ToArray();
    nums += digits[0].ToString() + digits[digits.Length - 1].ToString();
    sum += Int32.Parse(nums);
}

sum = 0;
var indexNumberStrings = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
var indexNumberInts = new List<int> {1,2,3,4,5,6,7,8,9};
foreach (var line in lines)
{
    var minIndexNumberString = -1;
    var maxIndexNumberString = -1;
    var minNumberInt = -1;
    var maxNumberInt = -1;
    int count = 0;
    foreach (var number in indexNumberStrings)
    {
        if(line.IndexOf(number) > minIndexNumberString)
        {
            minIndexNumberString = line.IndexOf(number);
            minNumberInt = indexNumberInts[count];
        }

        if(line.LastIndexOf(number) > maxIndexNumberString)
        {
            maxIndexNumberString = line.LastIndexOf(number);
            maxNumberInt = indexNumberInts[count];
        }
        count++;
    }


}

Console.WriteLine(sum);