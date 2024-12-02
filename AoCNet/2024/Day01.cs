using AdventOfCodeSupport;

namespace AoC._2024;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var lines = Input.Lines;
        var pairs = lines.Select(l => l.Split("   ")).ToList();
        var left = pairs.Select(p => p[0]).Select(int.Parse).ToList();
        var right = pairs.Select(p => p[1]).Select(int.Parse).ToList();
        var totalDistance = left
            .OrderBy(i => i)
            .Zip(right.OrderBy(i => i))
            .Select(x => Math.Abs(x.First - x.Second))
            .Sum();

        return totalDistance;
    }

    protected override object InternalPart2()
    {
        var lines = Input.Lines;
        var pairs = lines.Select(l => l.Split("   ")).ToList();
        var left = pairs.Select(p => p[0]).Select(int.Parse).ToList();
        var right = pairs.Select(p => p[1]).Select(int.Parse).ToList();
        var occurrences = new Dictionary<int, int>();
        foreach (var x in right)
        {
            var currentCount = occurrences.GetValueOrDefault(x);
            occurrences[x] = currentCount + 1;
        }

        var score = left
            .Select(x => x * occurrences.GetValueOrDefault(x))
            .Sum();

        return score;
    }
}