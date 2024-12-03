using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AoC._2024;

public partial class Day03 : AdventBase
{
    protected override object InternalPart1()
    {
        var total = 0;
        foreach (Match match in MulPattern().Matches(Input.Text))
        {
            total += match.Groups.AsReadOnly().Chunk(3).Sum(g => int.Parse(g[1].Value) * int.Parse(g[2].Value));
        }

        return total;
    }

    protected override object InternalPart2()
    {
        var total = 0;
        var enabled = true;
        foreach (Match match in GeneralPattern().Matches(Input.Text))
        {
            foreach (var g in match.Groups.AsReadOnly().Chunk(3))
            {
                if (g[0].Value == "do()")
                    enabled = true;

                if (g[0].Value == "don't()")
                    enabled = false;

                if (g[0].Value.StartsWith("mul") && enabled)
                    total += int.Parse(g[1].Value) * int.Parse(g[2].Value);
            }
        }

        return total;
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MulPattern();

    [GeneratedRegex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")]
    private static partial Regex GeneralPattern();
}