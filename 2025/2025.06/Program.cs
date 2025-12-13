using helpers;

var test = "123 328  51 64 \n 45 64  387 23 \n  6 98  215 314\n*   +   *   +  ";

List <String> [] problems = null;
var             data     = await Helper.GetData (6);
var             rows     = Helper.ReadLines (data).ToArray ();
for (var x = 0; x < rows.Length - 1; x ++)
{
  var elements = rows [x].Split (' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
  problems ??= new List<String> [elements.Length];
  for (var i = 0; i < elements.Length; i ++)
  {
    problems [i] ??= new List <String> ();
    problems [i].Add (elements [i]);
  }
}
for (var i = 0; i < problems.Length; i ++)
{
  problems [i] = problems [i].OrderByDescending (x => x.ToString ().Length).ToList ();
  
}


var p1   = new List <Int64> ();
var p2   = new List <Int64> ();
var operands = rows [^1].Split (' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
for (var i = 0; i < problems.Length; i ++)
{
  var operand = operands [i];
  switch (operand)
  {
    case "+" :
      p1.Add (problems[i].Select(Int64.Parse).Sum());
      for (var x = 0; x < problems[i].Count; x ++)
      {
        var ell         = new List <string> ();
        var pbs         = problems [i];
        var longestWord = pbs[0].Length;
        for (var p = 0; p < longestWord; p ++)
        {
          foreach (var problem in pbs)
          {
            ell.Add (problem[longestWord - problem.Length + p].ToString());
          }
          p2.Add(ell.Select (Int64.Parse).Sum ());
        }
      }
      break;
    case "*" :
      p1.Add (problems [i].Select(Int64.Parse).Aggregate ((a, b) => a * b));
      break;
  }
}
Console.WriteLine (p1.Sum ());