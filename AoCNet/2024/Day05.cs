using System.Collections.Specialized;
using AdventOfCodeSupport;

namespace AoC._2024;

public class Day05 : AdventBase
{
    private static bool ArraysEqual(int[] array1, int[] array2)
    {
        for (var i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
                return false;
        }

        return true;
    }
    
    protected override object InternalPart1()
    {
        var lines = Input.Lines;
        var orderingRules = lines[..1176];
        var pageSets = lines[1177..];

        var rules = new List<(int, int)>();

        foreach (var rule in orderingRules)
        {
            var one = int.Parse(rule.Split('|')[0]);
            var two = int.Parse(rule.Split('|')[1]);
            rules.Add((one, two));
        }

        var pageSetsParsed = pageSets.Select(l => l.Split(',').Select(int.Parse).ToArray());

        var correctSets = new List<int[]>();

        foreach (var set in pageSetsParsed)
        {
            var ordered = set.Order(new Comparer(rules)).ToArray();
            if (ArraysEqual(set, ordered))
                correctSets.Add(set);
        }

        return correctSets.Select(s => s[s.Length / 2]).Sum();
    }

    protected override object InternalPart2()
    {
        var lines = Input.Lines;
        var orderingRules = lines[..1176];
        var pageSets = lines[1177..];

        var rules = new List<(int, int)>();

        foreach (var rule in orderingRules)
        {
            var one = int.Parse(rule.Split('|')[0]);
            var two = int.Parse(rule.Split('|')[1]);
            rules.Add((one, two));
        }

        var pageSetsParsed = pageSets.Select(l => l.Split(',').Select(int.Parse).ToArray());

        var incorrectSets = new List<int[]>();

        foreach (var set in pageSetsParsed)
        {
            var ordered = set.Order(new Comparer(rules)).ToArray();
            if (!ArraysEqual(set, ordered))
                incorrectSets.Add(ordered);
        }

        return incorrectSets.Select(s => s[s.Length / 2]).Sum();
    }
}

public class Comparer : IComparer<int>
{
    private readonly List<(int, int)> _rules;

    public Comparer(List<(int, int)> rules)
    {
        _rules = rules;
    }

    public int Compare(int x, int y)
    {
        if (_rules.Contains((x, y)))
            return -1;
        return 1;
    }
}