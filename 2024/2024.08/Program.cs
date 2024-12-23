using System.Numerics;


// Read the file
var frequencies = new Dictionary<char, List<Vector2>>();
var lines = File.ReadLines("./input.txt").ToArray();
int rowIndex = 0;
foreach (var row in lines)
{
    var cols = row.ToCharArray();
    for (var colIndex = 0; colIndex < cols.Length; colIndex++)
    {
        var letter = cols[colIndex];
        if (letter.Equals('.'))
            continue;

        frequencies.TryAdd(letter, new List<Vector2>());
        frequencies[letter].Add(new Vector2(colIndex, rowIndex));
    }

    rowIndex += 1;
}

int width = lines[0].Length;
int height = rowIndex;


// Check for antinodes
var antinodesPartA = new HashSet<Vector2>();
var antinodesPartB = new HashSet<Vector2>();
foreach (var (frequency, positions) in frequencies)
{
    for (var i = 0; i < positions.Count - 1; i++)
    {
        var a = positions[i];

        for (var z = i + 1; z < positions.Count; z++)
        {
            var b = positions[z];

            var offset = new Vector2(b.X - a.X,  b.Y - a.Y);

            // Part one
            {
                var antinodeA = new Vector2(a.X - offset.X, a.Y - offset.Y);
                var antinodeB = new Vector2(b.X + offset.X, b.Y + offset.Y);
                if (isInBounds(antinodeA))
                    antinodesPartA.Add(antinodeA);
                if (isInBounds(antinodeB))
                    antinodesPartA.Add(antinodeB);
            }
            // Part two 
            {
                for (var k = 0; ; k++)
                {
                    var before = new Vector2((int)Math.Round(a.X - k * offset.X), (int)Math.Round(a.Y - k * offset.Y));
                    var after = new Vector2((int)Math.Round(a.X + k * offset.X), (int)Math.Round(a.Y + k * offset.Y));

                    bool isBeforeInBounds = isInBounds(before);
                    bool isAfterInBounds = isInBounds(after);
                    
                    if (!isBeforeInBounds && !isAfterInBounds)
                        break;
                    
                    if (isBeforeInBounds)
                        antinodesPartB.Add(before);

                    if (isAfterInBounds)
                        antinodesPartB.Add(after);
                }
            }
        }
    }
}

bool isInBounds(Vector2 node)
{
    if (node.X < 0 || node.X >= width)
        return false;
    if (node.Y < 0 || node.Y >= height)
        return false;
    return true;
}
 

Console.WriteLine(antinodesPartA.Count);
Console.WriteLine(antinodesPartB.Count);