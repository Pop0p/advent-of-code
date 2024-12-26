var grid = File.ReadAllLines("./input.txt")
    .Select(x => x
        .ToCharArray()
        .Select(c =>
            int.Parse(c.ToString()))
        .ToArray())
    .ToArray();

var directions = new (int, int)[] { new(-1, 0), new(1, 0), new(0, -1), new(0, 1) };
var partOneScore = 0;
var partTwoScore = 0;

for (var y = 0; y < grid.Length; y++)
{
    for (var x = 0; x < grid[y].Length; x++)
    {
        if (grid[y][x] != 0)
            continue;
        partOneScore += Bfs((x, y), true);
        partTwoScore += Bfs((x, y), false);
    }
}


int Bfs((int, int) start, bool oneVisit)
{
    var visited = new HashSet<(int, int)> { start };
    var queue = new Queue<(int, int)>();
    queue.Enqueue(start);
    var total = 0;
    while (queue.Count > 0)
    {
        var (x, y) = queue.Dequeue();
        foreach (var (dx, dy) in directions)
        {
            int newX = x + dx;
            int newY = y + dy;
            
            if (newX < 0 || newX >= grid[0].Length || newY < 0 || newY >= grid.Length)
                continue;
            
            var currentCell = grid[y][x];
            var neighbourCell = grid[newY][newX];

            if (neighbourCell - currentCell != 1 || (oneVisit && visited.Contains((newX, newY))))
                continue;

            if (oneVisit)
                visited.Add((newX, newY));

            if (neighbourCell == 9)
                total += 1;
            else
                queue.Enqueue((newX, newY));
        }
    }

    return total;
}

Console.WriteLine(partOneScore);
Console.WriteLine(partTwoScore);
Console.ReadKey();