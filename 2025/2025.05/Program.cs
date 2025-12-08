using helpers;

var data   = await Helper.GetData (5);
var splits = data.Split ([Environment.NewLine + Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
var ranges = Helper.ReadLines (splits [0])
  .Select (x => x.Split ('-').Select (Int64.Parse).ToArray ())
  .OrderBy (x => x [0])
  .ToArray ();
var ids = Helper.ReadLines(splits [1]).Select(Int64.Parse).ToArray();

Int64 p1 = 0;
Int64 p2 = 0;
for (var i = 0; i < ranges.Length; i ++)
{
  var range  = ranges [i];
  var others = ranges.Skip (i + 1).ToArray ();
  
  if (range [1] == -1)
    continue;
  
  var overlaps = others.Where (x => x [0] == range [0] || x [0] <= range [1]).ToArray ();
  p2 += range [1] - range [0] + 1;

  foreach (var overlap in overlaps)
  {
    if (overlap [0] >= range [0] && overlap [1] > range [1])
      overlap [0] = range [1] + 1;
    else if (overlap [0] >= range [0] && overlap [1] <= range [1])
      overlap [1] = -1;
  }
  
  p1 += ids.Count (x => x >= range [0] && x <= range [1]);
}


Console.WriteLine (p1);
Console.WriteLine (p2);