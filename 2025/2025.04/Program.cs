using helpers;

var test = "..@@.@@@@.\n@@@.@.@.@@\n@@@@@.@.@@\n@.@@@@..@.\n@@.@@@@.@@\n.@@@@@@@.@\n.@.@.@.@@@\n@.@@@.@@@@\n.@@@@@@@@.\n@.@.@@@.@.";
var data = await Helper.GetData (4);
var grid = Helper.LineToGrid (data);

Console.WriteLine (ForkLift (grid, false));
Console.WriteLine (ForkLift (grid, true));

return;

static UInt16 ForkLift (Char [] [] map, Boolean remove)
{
  UInt16 p = 0;
  while (true)
  {
    var toRemoved = new List <(Int32, Int32)> ();
    for (var y = 0; y < map.Length; y ++)
    {
      var row = map [y];
      for (var x = 0; x < row.Length; x ++)
      {
        if (row [x] != '@') continue;
        var count = 0;
        foreach (var direction in Helper.Directions)
        {
          var newX = x + direction.Item1;
          var newY = y + direction.Item2;
          if (newY < 0 || newY >= map.Length || newX < 0 || newX >= row.Length)
            continue;
          var cell = map [newY] [newX];
          if (cell == '@') count ++;
        }
        if (count >= 4)
          continue;
        p += 1;
        if (remove)
          toRemoved.Add ((x, y));
      }
    }
    if (toRemoved.Count > 0)
    {
      foreach (var rm in toRemoved)
        map [rm.Item2] [rm.Item1] = '.';
    }
    else
      break;
  }

  return p;
}