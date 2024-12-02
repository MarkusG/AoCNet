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

    private static bool IsSafe(IEnumerable<int> report)
    {
        var prevDirection = Direction.Unknown;
        int? last = null;
        foreach (var level in report)
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
                return false;

            prevDirection = direction;

            if (Math.Abs(diff) is < 1 or > 3)
                return false;

            last = level;
        }

        return true;
    }

    private static bool IsSafeWithDampener(int[] report)
    {
        for (var i = 0; i < report.Length; i++)
        {
            if (IsSafe(Dampen(report, i)))
                return true;
        }

        return false;
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
            .Count(IsSafe);
    }

    protected override object InternalPart2()
    {
        var lines = Input.Lines;
        return lines
            .Select(ToReport)
            .Count(IsSafeWithDampener);
    }
}