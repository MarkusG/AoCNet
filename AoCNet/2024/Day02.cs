using AdventOfCodeSupport;

namespace AoC._2024;

public class Day02 : AdventBase
{
    private enum Direction
    {
        Increasing,
        Unknown,
        Decreasing
    }

    private static int? UnsafeIndex(IEnumerable<int> report)
    {
        var prevDirection = Direction.Unknown;
        int? last = null;
        foreach (var (level, idx) in report.Select((level, idx) => (level, idx)))
        {
            if (last is null)
            {
                last = level;
                continue;
            }

            var diff = (level - last)!.Value;

            var direction = diff switch
            {
                > 0 => Direction.Increasing,
                0 => prevDirection,
                < 0 => Direction.Decreasing
            };

            if (direction != prevDirection && prevDirection != Direction.Unknown)
                return idx;

            prevDirection = direction;

            if (Math.Abs(diff) is < 1 or > 3)
                return idx;

            last = level;
        }

        return null;
    }

    private static bool IsSafeWithDampener(int[] report)
    {
        if (UnsafeIndex(report) is { } idx)
        {
            return UnsafeIndex(Dampen(report, idx - 1)) is null || UnsafeIndex(Dampen(report, idx)) is null;
        }

        return true;
    }

    private static IEnumerable<int> Dampen(int[] report, int index)
    {
        for (var i = 0; i < report.Length; i++)
        {
            if (i == index)
                continue;

            yield return report[i];
        }
    }

    private static int[] ToReport(string line)
    {
        return line.Split(' ').Select(int.Parse).ToArray();
    }

    protected override object InternalPart1()
    {
        var lines = Input.Lines;
        return lines
            .Select(ToReport)
            .Count(r => UnsafeIndex(r) is null);
    }

    protected override object InternalPart2()
    {
        var lines = Input.Lines;
        return lines
            .Select(ToReport)
            .Count(IsSafeWithDampener);
    }
}