using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {

        Solve(20, false);
        Solve(10000, true);
    }

    private static void Solve(int rounds, bool veryWorried)
    {
        const string FILE_PATH = "./input.txt";
        List<Monkey> monkeys = new List<Monkey>();
        ulong part_two_mod = 1;
        using (var sr = new StreamReader(FILE_PATH))
        {
            string[] notes = sr.ReadToEnd().Split("\n\n");
            for (int i = 0; i < notes.Length; i++)
            {
                Monkey m = new Monkey();
                string[] note = notes[i].Split("\n").Select(n => n.Trim()).ToArray();
                m.Items = Regex.Replace(note[1], @"[^\d ]+", "").Trim().Split(" ").Select(ulong.Parse).ToList();
                m.Operation = Regex.Replace(note[2], @"Operation: new = old ", "").Trim();
                m.Test = ulong.Parse(Regex.Replace(note[3], @"[^\d]+", "").Trim());
                m.ThrowTo = (int.Parse(Regex.Replace(note[4], @"[^\d]+", "").Trim()), int.Parse(Regex.Replace(note[5], @"[^\d]+", "").Trim()));

                part_two_mod *= m.Test;
                monkeys.Add(m);
            }
        }

        for (int i = 0; i < rounds; i++)
        {
            for (int y = 0; y < monkeys.Count; y++)
            {
                Monkey m = monkeys[y];
                for (int x = 0; x < m.Items.Count; x++)
                {
                    ulong value = m.Items[x];
                    string[] act = m.Operation.Split(" ");
                    switch (act[0])
                    {

                        case "*":
                            value *= act[1] == "old" ? value : ulong.Parse(act[1]);
                            break;
                        case "+":
                            value += act[1] == "old" ? value : ulong.Parse(act[1]);
                            break;
                        default:
                            throw new Exception("Operator not handled");
                    }

                    if (!veryWorried)
                        value /= 3;
                    else
                        value %= part_two_mod;

                    if (value % m.Test == 0)
                        monkeys[m.ThrowTo.Item1].Items.Add(value);
                    else
                        monkeys[m.ThrowTo.Item2].Items.Add(value);


                    m.InspectionCount++;
                }
                m.Items.Clear();
            }

        }

        monkeys.Sort((a, b) => b.InspectionCount.CompareTo(a.InspectionCount));
        Console.WriteLine(monkeys[0].InspectionCount * monkeys[1].InspectionCount);
    }

}


public class Monkey
{
    public List<ulong> Items { get; set; }
    public string Operation { get; set; }
    public ulong Test { get; set; }
    public (int, int) ThrowTo { get; set; }
    public ulong InspectionCount { get; set; }
}
