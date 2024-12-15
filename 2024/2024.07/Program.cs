long partOne = 0;
long partTwo = 0;
foreach (var line in File.ReadLines("./input.txt"))
{
    var columns = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);
    var testValue = long.Parse(columns[0]);
    var numbers = columns[1].Split(" ");

    var expressions = GenerateExpressions(numbers.Select(long.Parse).ToArray(), ["+", "*"]);
    if (expressions.Any(x => x == testValue))
        partOne += testValue;
    else
    {
        expressions = GenerateExpressions(numbers.Select(long.Parse).ToArray(), ["+", "*", "||"]);
        if (expressions.Any(x => x == testValue))
            partTwo += testValue;
    }
}

Console.WriteLine(partOne);
Console.WriteLine(partTwo + partOne);

static List<long> GenerateExpressions(long[] numbers, string[] operators)
{
    List<long> equations = [];
    Backtrack(0, numbers[0]);
    return equations;

    void Backtrack(long index, long current)
    {
        if (index == numbers.Length - 1)
        {
            equations.Add(current);
            return;
        }

        foreach (var op in operators)
        {
            switch (op)
            {
                case "+":
                    Backtrack(index + 1, current + numbers[index + 1]);
                    break;
                case "*":
                    Backtrack(index + 1, current * numbers[index + 1]);
                    break;
                case "||":
                    Backtrack(index + 1, Concat(current, numbers[index + 1]));
                    break;
            }
        }
    }
}

static long Concat(long num1, long num2)
{
    long numOfDigits = 1;

    while (numOfDigits <= num2)
        numOfDigits *= 10;

    return num1 * numOfDigits + num2;
}
