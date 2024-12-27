var memory = File.ReadAllText("./input.txt").Split(" ").Select(ulong.Parse).GroupBy(n => n).ToDictionary(group => group.Key, group => (ulong)group.Count());
for (var i = 0; i < 75; i++)
{
    var nextMemory = new Dictionary<ulong, ulong>();
    
    foreach (var (key, count) in memory.ToList())
    {
        foreach (var result in ApplyRules(key))
        {
            if (!nextMemory.TryAdd(result, count))
                nextMemory[result] += count;
        }
    }

    memory = nextMemory;
}


Console.WriteLine(memory.Values.Where(x => x > 0).Aggregate((a,b) => a+b));
return;

ulong[] ApplyRules(ulong number)
{
    var digits = (int)Math.Floor(Math.Log10(number) + 1);

    if (number == 0)
        return [1];

    if (digits % 2 != 0)
        return [number * 2024];

    var numberOne = number.ToString()[..(digits / 2)];
    var numberTwo = number.ToString()[(digits / 2)..];
    return [uint.Parse(numberOne), uint.Parse(numberTwo)];
}
