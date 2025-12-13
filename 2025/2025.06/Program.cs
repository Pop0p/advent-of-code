using helpers;

var p1Rows = Helper.ReadLines(await Helper.GetData(6)).Select(x => x.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)).ToArray();
var p2Rows = Helper.ReadLines(await Helper.GetData(6)).ToArray();

var rows = p2Rows.Length - 1;
var p1Problems = p1Rows.Take(p1Rows.Length - 1).SelectMany(elements => elements).ToArray();
var p2Problems = new List<string>();
for (var x = 0; x < p2Rows.First().Length; x++)
{
    var el = p2Rows.First()[x].ToString();
    for (var y = 1; y < rows; y++)
        el += p2Rows[y][x];
    p2Problems.Add(el);
}

Console.WriteLine(Calculate(p1Rows.Last(), p1Problems, p1Rows.First().Length, false));
Console.WriteLine(Calculate(p1Rows.Last(), p2Problems.ToArray(), rows, true));

return;

static UInt64 Calculate(String[] last, String[] pbs, int rowSize, bool p2)
{
    UInt64 total = 0;
    var lastIndex = 0;
    for (var i = 0; i < last.Length; i++)
    {
        String[] elements;
        if (!p2)
            elements = pbs.Where((_, x) => x % rowSize == i).ToArray();
        else
        {
            var id = pbs.Skip(lastIndex).ToArray().IndexOf(new String(' ', rowSize));
            elements = pbs.Skip(lastIndex).Take(id == -1 ? pbs.Length - lastIndex : id).ToArray();
            lastIndex += id + 1;
        }

        var expression = last[i];

        switch (expression)
        {
            case "+":
                total += elements.Select(UInt64.Parse).Aggregate((x, y) => x + y);
                break;
            case "*":
                total += elements.Select(UInt64.Parse).Aggregate((x, y) => x * y);
                break;
        }
    }

    return total;
}