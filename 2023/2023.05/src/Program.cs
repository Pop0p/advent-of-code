using System.Diagnostics;
using System.Text.RegularExpressions;
string[] lines = File.ReadAllLines("./input.txt");


// Parsing file
List<List<List<long>>> map = new();
var seeds = lines[0].Split(":")[1].Trim().Split(" ").Select(long.Parse).ToArray();
for (int i = 2; i < lines.Length; i++)
{
    var line = lines[i];
    if (string.IsNullOrEmpty(line))
        continue;
    if (line.Contains("map:"))
        map.Add(new List<List<long>>());
    else
        map.Last().Add(line.Split(" ").Select(long.Parse).ToList());
}

// Part one
long[] resultsOne = PartOne(seeds.ToArray(), map, 0);
Console.WriteLine(resultsOne.Min());


// Part two
List<(long, long)> ranges = new();
for (int i = 0; i < seeds.Length; i += 2)
    ranges.Add((seeds[i], seeds[i] + seeds[i + 1] - 1));
List<(long, long)> resultsTwo = PartTwo(ranges, map, 0).OrderBy(t => t.Item1).ToList();
Console.WriteLine(resultsTwo.FirstOrDefault().Item1);


static long[] PartOne(long[] data, List<List<List<long>>> maps, int current)
{
    List<List<long>> actual = maps[current];
    for (int x = 0; x < data.Length; x++)
    {
        for (int i = 0; i < actual.Count; i++)
        {
            long destination = actual[i][0];
            long source = actual[i][1];
            long range = actual[i][2];

            if (data[x] >= source && data[x] <= source + range)
            {
                data[x] = destination + (data[x] - source);
                break;
            }
        }
    }
    return current == maps.Count - 1 ? data : PartOne(data, maps, current += 1);
}
static List<(long, long)> PartTwo(List<(long, long)> ranges, List<List<List<long>>> maps, int current)
{
    List<List<long>> map = maps[current];
    List<(long, long)> matches_unmapped = new();
    List<(long, long)> matches = new();
    List<(long, long)> unmatcheds = new();
    for (int x = 0; x < ranges.Count; x++)
    {
        var range_start = ranges[x].Item1;
        var range_end = ranges[x].Item2;
        bool has_been_matched = false;
        for (int i = 0; i < map.Count; i++)
        {
            long range_length = map[i][2] - 1;
            long map_destination_range_start = map[i][0];
            long map_destination_range_end = map[i][0] + range_length;
            long map_source_range_start = map[i][1];
            long map_source_range_end = map_source_range_start + range_length;

            if (range_start <= map_source_range_end && map_source_range_start <= range_end)
            {
                long start_offset = 0;
                long end_offset = 0;
                if (range_start < map_source_range_start)
                    start_offset = map_source_range_start - range_start;
                if (range_end > map_source_range_end)
                    end_offset = range_end - map_source_range_end;

                var new_destination_start = map_destination_range_start + (range_start + start_offset - map_source_range_start);
                var new_destination_end = new_destination_start + (range_end - range_start) - start_offset - end_offset;

                if (end_offset > 0)
                    unmatcheds.Add((range_end - (end_offset - 1), range_end));
                if (start_offset > 0)
                    unmatcheds.Add((range_start, map_source_range_start - 1));

                matches_unmapped.Add((range_start + start_offset, range_end - end_offset));
                for (int z = unmatcheds.Count - 1; z >= 0; z--)
                {
                    var A = unmatcheds[z];
                    for (int p = matches_unmapped.Count - 1; p >= 0; p--)
                    {
                        var B = matches_unmapped[p];
                        if (A == B)
                        {
                            unmatcheds.Remove(A);
                            break;
                        }

                        if (A.Item1 <= B.Item2 && B.Item1 <= A.Item2)
                        {
                            if (A.Item1 < B.Item1 && A.Item2 < B.Item2)
                            {
                                unmatcheds[z] = (A.Item1, B.Item1 - 1);
                                break;
                            }
                            else if (A.Item1 >= B.Item1 && A.Item2 > B.Item2)
                            {
                                unmatcheds[z] = (B.Item2 + 1, A.Item2);
                                break;
                            }
                            else if (A.Item1 >= B.Item1 && A.Item2 <= B.Item2)
                            {
                                unmatcheds.Remove(A);
                                break;
                            }
                            else if (A.Item1 < B.Item1 && A.Item2 > B.Item2)
                            {
                                unmatcheds[z] = (A.Item1, B.Item1 - 1);
                                unmatcheds.Add((B.Item2 + 1, A.Item2));
                                break;
                            }
                        }
                    }
                }


                matches.Add((new_destination_start, new_destination_end));

                has_been_matched = true;
            }
        }
        if (!has_been_matched)
            unmatcheds.Add(ranges[x]);
    }

    ranges = unmatcheds.Distinct().ToList();
    ranges.AddRange(matches);
    return current == maps.Count - 1 ? ranges : PartTwo(ranges, maps, current += 1);
}