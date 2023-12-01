var lines = File.ReadAllLines("input.txt");
var sum = 0;
foreach (var line in lines)
{
    var nums = "";
    var digits = line.Where(Char.IsDigit).ToArray();
    nums += digits[0].ToString() + digits[digits.Length - 1].ToString();
    sum += Int32.Parse(nums);
}
Console.WriteLine(sum);