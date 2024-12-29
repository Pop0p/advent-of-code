using System.Text.RegularExpressions;

var buttonRegex = new Regex(@"Button (A|B): X\+(\d+), Y\+(\d+)");
var prizeRegex = new Regex(@"Prize: X=(\d+), Y=(\d+)");

long minimalCostNoOffset = 0;
long minimalCostWithOffset = 0;

var lines = File.ReadAllLines("input.txt").Where(arg => !string.IsNullOrWhiteSpace(arg)).ToArray();
for (int i = 0; i < lines.Length; i += 3)
{
    var buttonA = buttonRegex.Match(lines[i]);
    var buttonB = buttonRegex.Match(lines[i + 1]);
    var prize = prizeRegex.Match(lines[i + 2]);

    // https://www.youtube.com/watch?v=jBsC34PxzoM
    // https://www.youtube.com/watch?v=vXqlIOX2itM
    if (buttonA.Success && buttonB.Success && prize.Success)
    {
        int Ax = int.Parse(buttonA.Groups[2].Value);
        int Ay = int.Parse(buttonA.Groups[3].Value);

        int Bx = int.Parse(buttonB.Groups[2].Value);
        int By = int.Parse(buttonB.Groups[3].Value);

        long Cx = long.Parse(prize.Groups[1].Value);
        long Cy = long.Parse(prize.Groups[2].Value);

        foreach (bool offset in new[] { false, true })
        {
            long offsetValue = offset ? 10000000000000 : 0;

            long adjustedCx = Cx + offsetValue;
            long adjustedCy = Cy + offsetValue;

            long A = (adjustedCx * By - adjustedCy * Bx) / (Ax * By - Ay * Bx);
            long B = (Ax * adjustedCy - Ay * adjustedCx) / (Ax * By - Ay * Bx);

            if (adjustedCx == Ax * A + Bx * B && adjustedCy == Ay * A + By * B)
            {
                if (!offset)
                    minimalCostNoOffset += 3 * A + B;
                else
                    minimalCostWithOffset += 3 * A + B;
            }
        }
    }
}
Console.WriteLine(minimalCostNoOffset);
Console.WriteLine(minimalCostWithOffset);