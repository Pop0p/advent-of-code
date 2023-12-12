using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

internal class Program
{

    record struct Position(int x, int y);
    public class Square
    {
        public Square(char ch, int v, int d)
        {
            character = ch;
            value = v;
            distance = d;
        }
        public char character { get; set; }
        public int value { get; set; }
        public int distance { get; set; }
    }
    static Position StartKey { get; set; }
    static Position GoalKey { get; set; }
    static int Width { get; set; }
    static int Height { get; set; }

    private static void Main(string[] args)
    {
        const string FILE_PATH = ".input.txt";
        Dictionary<Position, Square> map = new Dictionary<Position, Square>();
        using (var sr = new StreamReader(FILE_PATH))
        {
            string line;
            int y = 0;
            while ((line = sr.ReadLine()) != null)
            {
                char[] c = line.ToCharArray();
                for (int x = 0; x < c.Length; x++)
                {
                    Position p = new Position(x, y);
                    Square n = new Square(line[x], (int)line[x], 0);
                    if (line[x] == 'S')
                    {
                        StartKey = p;
                        n.value = 97;
                    }
                    if (line[x] == 'E')
                    {
                        GoalKey = p;
                        n.value = 122;
                    }

                    map.Add(p, n);
                }
                Width = line.Length;
                y++;
            }
            Height = y;
        }

        // Part one
        Console.WriteLine(Solve(map, GoalKey, StartKey));


        // Part two
        int shortest = int.MaxValue;
        foreach (KeyValuePair<Position, Square> n in map)
        {
            int s = -1;
            if (n.Value.character == 'a')
            {
                s = Solve(map, GoalKey, n.Key);
                if (s < shortest)
                    shortest = s;
            }
        }
        Console.WriteLine(shortest);
    }

    private static int Solve(Dictionary<Position, Square> map, Position start, Position goal)
    {

        Queue<Position> queue = new Queue<Position>();

        Dictionary<Position, bool> visited = new Dictionary<Position, bool>();
        foreach (KeyValuePair<Position, Square> n in map)
            visited.Add(n.Key, false);

        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var element = queue.Dequeue();
            if (visited[element])
                continue;

            var node = map[element];
            Position[] neighbours = new Position[] { new Position(-1, -1), new Position(-1, -1), new Position(-1, -1), new Position(-1, -1) }; // L, R, U, D

            if (element.x > 0)
                neighbours[0] = new Position(element.x - 1, element.y);
            if (element.x < Width - 1)
                neighbours[1] = new Position(element.x + 1, element.y);

            if (element.y > 0)
                neighbours[2] = new Position(element.x, element.y - 1);
            if (element.y < Height - 1)
                neighbours[3] = new Position(element.x, element.y + 1);


            if (neighbours[0].x != -1 && node.value - map[neighbours[0]].value <= 1 && !visited[neighbours[0]]) // L

            {
                queue.Enqueue(neighbours[0]);
                map[neighbours[0]].distance = node.distance + 1;
            }
            if (neighbours[1].x != -1 && node.value - map[neighbours[1]].value <= 1 && !visited[neighbours[1]]) // R

            {
                queue.Enqueue(neighbours[1]);
                map[neighbours[1]].distance = node.distance + 1;
            }
            if (neighbours[2].x != -1 && node.value - map[neighbours[2]].value <= 1 && !visited[neighbours[2]]) // U

            {
                queue.Enqueue(neighbours[2]);
                map[neighbours[2]].distance = node.distance + 1;
            }
            if (neighbours[3].x != -1 && node.value - map[neighbours[3]].value <= 1 && !visited[neighbours[3]]) // D

            {
                queue.Enqueue(neighbours[3]);
                map[neighbours[3]].distance = node.distance + 1;
            }

            if (goal == element)
                return map[goal].distance;


            visited[element] = true;
        }
        
        return int.MaxValue;

    }

}