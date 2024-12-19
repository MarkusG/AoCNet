using AdventOfCodeSupport;

namespace AoC._2024;

public class Day19 : AdventBase
{
    private static bool IsPossible(List<string> towels, string design, Dictionary<long, bool> cache)
    {
        if (design.Length == 0)
            return true;

        if (cache.TryGetValue(design.Length, out var possible))
            return possible;

        foreach (var t in towels.Where(design.StartsWith))
        {
            cache[design.Length] = IsPossible(towels, design[t.Length..], cache);
            if (cache[design.Length])
                return true;
        }

        return false;
    }

    protected override object InternalPart1()
    {
        var towels = Input.Lines[0].Split(", ").ToList();
        return Input.Lines[2..].Count(design => IsPossible(towels, design, []));
    }

    private static long PossibleArrangements(List<string> towels, string design, Dictionary<long, long> cache)
    {
        if (design.Length == 0)
            return 1;

        if (cache.TryGetValue(design.Length, out var possibilities))
            return possibilities;

        foreach (var t in towels.Where(design.StartsWith))
        {
            if (cache.ContainsKey(design.Length))
                cache[design.Length] += PossibleArrangements(towels, design[t.Length..], cache);
            else
                cache[design.Length] = PossibleArrangements(towels, design[t.Length..], cache);
        }

        cache.TryAdd(design.Length, 0);

        return cache[design.Length];
    }

    protected override object InternalPart2()
    {
        var towels = Input.Lines[0].Split(", ").ToList();
        return Input.Lines[2..].Sum(design => PossibleArrangements(towels, design, []));
    }
}