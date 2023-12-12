using System.Text.RegularExpressions;

string[] words = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
string pattern = @"(" + string.Join("|", words) + @")";
int total = 0;
foreach (string line in File.ReadLines("./input.txt"))
{
    var str = Regex.Replace(line, pattern, match => (Array.IndexOf(words, match.Value) + 1).ToString());
    var digits = str.Where(char.IsDigit).ToArray();
    total += int.Parse($"{digits.First()}{digits.Last()}");
}
Console.WriteLine(total);
