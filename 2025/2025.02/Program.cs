using helpers;

var test  = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
var data  = await Helper.GetData (2);
var parts = Helper.SplitLine (',', data);


UInt64 p1 = 0;
UInt64 p2 = 0;
foreach (var part in parts)
{
  var ranges = part.Split ('-');

  var start   = UInt64.Parse (ranges [0]);
  var end     = UInt64.Parse (ranges [1]);
  var current = start;

  while (current <= end)
  {
    var str        = current.ToString ();
    var differents = str.Distinct ().Count ();


    if (str.Length % 2 == 0)
    {
      var half       = str.Length / 2;
      var firstPart  = str [..half];
      var secondPart = str [half..];

      if (firstPart == secondPart)
      {
        p1 += current;
        p2 += current;
        current ++;
        continue;
      }
    }
    if (str.Length % differents == 0 && differents != str.Length)
    {
      var chunks = new List <String> ();

      for (var i = 0; i < str.Length; i += differents)
        chunks.Add (str.Substring (i, differents));

      if (chunks.All (chunk => chunk == chunks [0]))
        p2 += current;
    }
    current ++;
  }
}
Console.WriteLine (p1);
Console.WriteLine (p2);