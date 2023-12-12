using System.Linq;

internal class Program
{
    const string FILE_PATH = "./input.txt";

    private static void Main(string[] args)
    {
        FixCommunication(4);
        FixCommunication(14);
    }

    private static void FixCommunication(int maxSize)
    {
        using (var sr = new StreamReader(FILE_PATH))
        {
            string line = sr.ReadLine();

            Queue<char> q = new Queue<char>();
            for (int i = 0; i < line.Length; i++)
            {
                if (q.Count >= maxSize)
                    q.Dequeue();


                q.Enqueue(line[i]);

                if (q.Count == maxSize && q.GroupBy(x => x).All(g => g.Count() == 1))
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }

    }
}