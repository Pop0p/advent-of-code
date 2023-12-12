var lines = File.ReadAllLines("./input.txt");
// ONE
Console.WriteLine(GetScore("AKQJT98765432", GetGroupedHands(lines, null), lines));
// TWO
Console.WriteLine(GetScore("AKQT98765432J", GetGroupedHands(lines, 'J'), lines));


Dictionary<string, string[]> GetGroupedHands(string[] lines, char? wildcard)
{
    List<(string, string)> hands = new();
    for (int i = 0; i < lines.Length; i++)
    {
        var split = lines[i].Split(" ");
        var dictOfLetters = split[0].GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var groups = dictOfLetters.OrderByDescending(x => x.Value).ToArray();
        string rating = "A";
        switch (groups.Length)
        {
            case 1:
                rating = "A";
                break;
            case 2:
                if (groups[0].Value == 4)
                    rating = wildcard.HasValue && dictOfLetters.ContainsKey(wildcard.Value) ? "A" : "B";
                else
                    rating = wildcard.HasValue && dictOfLetters.ContainsKey(wildcard.Value) ? "A" : "C";
                break;
            case 3:
                if (groups[0].Value == 3)
                    rating = wildcard.HasValue && dictOfLetters.ContainsKey(wildcard.Value) ? "B" : "D";
                else
                {
                    if (wildcard.HasValue && dictOfLetters.ContainsKey(wildcard.Value))
                        rating = dictOfLetters[wildcard.Value] == 2 ? "B" : "C";
                    else
                        rating = "E";
                }
                break;
            case 4:
                rating = wildcard.HasValue && dictOfLetters.ContainsKey(wildcard.Value) ? "D" : "F";
                break;
            case 5:
                rating = wildcard.HasValue && dictOfLetters.ContainsKey(wildcard.Value) ? "F" : "G";
                break;
        }
        if (rating != "")
            hands.Add((split[0], rating));
    }

    return hands.GroupBy(hand => hand.Item2, hand => hand.Item1).ToDictionary(g => g.Key, g => g.ToArray()); ;
}
int GetScore(string order, Dictionary<string, string[]> hands, string[] lines)
{
    string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G" };
    int r = lines.Length;
    int result = 0;
    for (int i = 0; i < letters.Length; i++)
    {
        if (!hands.ContainsKey(letters[i]))
            continue;
        var group = hands[letters[i]].OrderBy(str => string.Concat(str.Select(c => order.IndexOf(c).ToString("D2")))).ToArray();
        for (var x = 0; x < group.Length; x++)
        {
            var score = int.Parse(lines.Where(l => l.Split(" ")[0] == group[x]).First().Split(" ")[1]) * r;
            result += score;
            r--;
        }
    }
    return result;
}