List<int> firstColumn = [], secondColumn = [];
int totalDifferences = 0, totalProducts = 0;
foreach (var line in File.ReadLines("./input.txt"))
{
    var columns = line.Split("   ", StringSplitOptions.RemoveEmptyEntries);
    firstColumn.Add(int.Parse(columns[0]));
    secondColumn.Add(int.Parse(columns[1]));
}

firstColumn.Sort();
secondColumn.Sort();

var frequencies = secondColumn.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
for (var i = 0; i < firstColumn.Count; i++)
{
    totalDifferences += Math.Abs(firstColumn[i] - secondColumn[i]);
    totalProducts += firstColumn[i] * frequencies.GetValueOrDefault(firstColumn[i], 0);
}

Console.WriteLine($"Part one : {totalDifferences}, Part two : {totalProducts}");