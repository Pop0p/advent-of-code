long p1_total = 0;
long p2_total = 0;
foreach (string line in File.ReadLines("./input.txt"))
{
    List<List<long>> differences = new()
    {
        line.Split(" ").Select(long.Parse).Reverse().ToList()
    };
    while (true)
    {
        List<long> digits = differences.Last();
        List<long> diffs = new();
        for (int i = 0; i < digits.Count - 1; i++)
            diffs.Add(digits[i] - digits[i + 1]);

        differences.Add(diffs);


        if (differences.Last().All(x => x == 0))
        {
            long p1_sum = 0;
            long p2_sum = 0;
            for (int x = differences.Count - 1; x >= 0; x--)
            {
                p1_sum += differences[x][0];
                p2_sum = differences[x].Last() - p2_sum;
            }
            p1_total += p1_sum;
            p2_total += p2_sum;
            break;
        }
    }
}
Console.WriteLine(p1_total);
Console.WriteLine(p2_total);