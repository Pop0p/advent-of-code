using helpers;

var test = "L68\nL30\nR48\nL5\nR60\nL55\nL1\nL99\nR14\nL82";

var data = await Helper.GetData(1);

var value  = 50;
var p1     = 0;
var p2     = 0;
foreach (var line in Helper.ReadLines(data))
{
  var direction = line[0];
  var distance  = Int32.Parse(line[1..]);

  switch (direction)
  {
    case 'L' :
    {
      for (var i = distance; i > 0; i --)
      {
        value -= 1;
        if (value == 0)
          p2 += 1;
        else if (value < 0)
          value =  99;
      }
      break;
    }

    case 'R' :
    {
      value += distance;
      if (value > 99)
      {
        p2    += (int)Math.Floor(value / 100f);
        value %= 100;
      }
      break;
    }
  }
  if (value == 0)
    p1++;
    

}

Console.WriteLine(p1);
Console.WriteLine(p2);
Console.WriteLine(value);