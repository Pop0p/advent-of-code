using helpers;

var test = "987654321111111\n811111111111119\n234234234234278\n818181911112111";
var data = await Helper.GetData (3);


Console.WriteLine (GetJoltage (2));
Console.WriteLine (GetJoltage (12));
return;

UInt64 GetJoltage (Int32 needed)
{
  UInt64 p = 0;
  foreach (var line in Helper.ReadLines (data))
  {
    var batteries = line.ToCharArray ().Select (c => Int32.Parse (c.ToString ())).ToArray ();
    var current   = String.Empty;
    var skip      = 0;
    while (current.Length < needed)
    {
      var remaining         = batteries.Skip (skip).ToArray ();
      var searchLength       = remaining.Length - (needed - current.Length) + 1;
      var searchable        = remaining [..searchLength].ToArray ();
      var highestValueIndex     = searchable.IndexOf (searchable.Max ());
      current += searchable [highestValueIndex].ToString ();
      skip    += highestValueIndex + 1;
    }
    p += UInt64.Parse (current);
  }
  return p;
}