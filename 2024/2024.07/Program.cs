long partOne = 0;
long partTwo = 0;

foreach (var line in File.ReadLines("./input.txt"))
{
    var columns = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);
    var testValue = long.Parse(columns[0]);
    var numbers = columns[1].Split(" ");

    if (GenerateExpressions(numbers.Select(long.Parse).ToArray(), ["+", "*"], testValue))
        partOne += testValue;
    else if (GenerateExpressions(numbers.Select(long.Parse).ToArray(), ["+", "*", "||"], testValue))
        partTwo += testValue;
}

Console.WriteLine(partOne);
Console.WriteLine(partTwo + partOne);

static bool GenerateExpressions(long[] numbers, string[] operators, long target)
{
    return Backtrack(0, numbers[0]);

    bool Backtrack(long index, long current)
    {
        if (index == numbers.Length - 1)
        {
            return current == target;
        }

        bool r = false;
        foreach (var op in operators)
        {
            switch (op)
            {
                case "+":
                    r = Backtrack(index + 1, current + numbers[index + 1]);
                    break;
                case "*":
                    r = Backtrack(index + 1, current * numbers[index + 1]);
                    break;
                case "||":
                    r = Backtrack(index + 1, Concat(current, numbers[index + 1]));
                    break;
            }

            if (r)
                return true;
        }

        return false;
    }
}

static long Concat(long num1, long num2)
{
    long numOfDigits = 1;

    while (numOfDigits <= num2)
        numOfDigits *= 10;

    return num1 * numOfDigits + num2;
}