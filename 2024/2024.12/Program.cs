var grid = File.ReadLines("./input.txt").Select(x => x.ToCharArray()).ToArray();

var visited = new List<(int, int)>();
var queue = new Queue<(int, int)>();
queue.Enqueue((0, 0));
var regions = new Dictionary<char, List<(int, int)>>();

int size = grid.Length;

while (queue.Count > 0)
{
    var cell = queue.Dequeue();

    if (visited.Contains(cell))
        continue;
    
    var x = cell.Item1;
    var y = cell.Item2;

    if (!regions.TryAdd(grid[y][x], [(y, x)]))
        regions[grid[y][x]].Add((y, x));

    char? east = (x + 1 < size) ? grid[y][x + 1] : null;
    char? south = (y + 1 < size) ? grid[y + 1][x] : null;
    char? west = (x - 1 >= 0) ? grid[y][x - 1] : null;
    char? north = (y - 1 >= 0) ? grid[y - 1][x] : null;


    if (east.HasValue && !visited.Contains((y, x + 1)))
        queue.Enqueue((y, x + 1));

    if (south.HasValue && !visited.Contains((y + 1, x)))
        queue.Enqueue((y + 1, x));

    if (west.HasValue && !visited.Contains((y, x - 1)))
        queue.Enqueue((y, x - 1));

    if (north.HasValue && !visited.Contains((y - 1, x)))
        queue.Enqueue((y - 1, x));

    visited.Add(cell);
}

Console.WriteLine(regions);