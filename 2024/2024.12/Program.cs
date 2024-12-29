var grid = File.ReadLines("./input.txt").Select(x => x.ToCharArray()).ToArray();

var visited = new List<(int, int)>();
var queue = new Queue<(int, int)>();
(int, int)[] directions = [(-1, 0), (1, 0), (0, -1), (0, +1)];


int totalPartOne = 0;
int totalPartTwo = 0;
for (int yy = 0; yy < grid.Length; yy++)
{
    for (int xx = 0; xx < grid.Length; xx++)
    {
        if (visited.Contains((yy, xx)))
            continue;


        int currentGroupArea = 1;
        int currentGroupPerimeterPartOne = 0;
        int currentGroupPerimeterPartTwo = 0;
        List<(int, int)> currentGroupContacts = [];
        List<char> currentGroupContactsDirection = [];

        queue.Enqueue((yy, xx));
        while (queue.Count > 0)
        {
            var (y, x) = queue.Dequeue();
            var cell = grid[y][x];

            foreach (var direction in directions)
            {
                var newX = x + direction.Item1;
                var newY = y + direction.Item2;

                if (newX == -1 || newY == -1 || newY == grid.Length || newX == grid.Length || grid[newY][newX] != cell)
                {
                    currentGroupPerimeterPartOne += 1;
                    if (direction.Item2 != 0)
                        currentGroupContacts.Add((newY + (y * 10), newX));
                    // currentGroupContactsDirection.Add(direction.Item1 != 0 ? 'V' : 'H');
                }
                else
                {
                    if (!visited.Contains((newY, newX)) && !queue.Contains((newY, newX)))
                    {
                        currentGroupArea += 1;
                        queue.Enqueue((newY, newX));
                    }
                }
            }

            visited.Add((y, x));
        }

        if (currentGroupArea > 1)
        {
            var hl = currentGroupContacts
                .GroupBy(p => p.Item1).ToList(); // Grouper par y
            var horizontalLines = currentGroupContacts
                .GroupBy(p => p.Item1) // Grouper par y
                .SelectMany(g => g
                    .OrderBy(p => p.Item2) // Trier par x
                    .Aggregate(new List<List<(int, int)>>(), (acc, current) =>
                    {
                        if (acc.Count != 0 && acc.Last().Contains(current))
                            return acc;
                        if (acc.Count == 0 || current.Item2 != acc.Last().Last().Item2 + 1)
                            acc.Add(new List<(int, int)> { current }); // Nouvelle séquence
                        else
                            acc.Last().Add(current); // Ajouter à la séquence en cours
                        return acc;
                    }))
                .ToList();
            currentGroupPerimeterPartTwo = horizontalLines.Count() * 2;
        }
        else
        {
            currentGroupPerimeterPartTwo = 4;
        }

        Console.WriteLine(grid[yy][xx] + " : " + currentGroupArea + " * " + currentGroupPerimeterPartTwo);
        totalPartOne += currentGroupArea * currentGroupPerimeterPartOne;
        totalPartTwo += currentGroupArea * currentGroupPerimeterPartTwo;
    }
}

bool AreConnected(List<(int, int)> seq1, List<(int, int)> seq2)
{
    foreach (var p1 in seq1)
    {
        foreach (var p2 in seq2)
        {
            if (Math.Abs(p1.Item1 - p2.Item1) <= 1 && Math.Abs(p1.Item2 - p2.Item2) <= 1)
            {
                return true;
            }
        }
    }

    return false;
}

Console.WriteLine(totalPartOne);
Console.WriteLine(totalPartTwo);