using AdventOfCodeSupport;

namespace AoC._2024;

public class Day23 : AdventBase
{
    private static List<(string, string)> _edges = [];
    private static Dictionary<string, HashSet<string>> _neighbors = [];

    protected override object InternalPart1()
    {
        _edges = Input.Lines
            .Select(l => l.Split('-'))
            .Select(s => (s[0], s[1]))
            .ToList();

        foreach (var (v, u) in _edges)
        {
            _neighbors.TryAdd(v, []);
            _neighbors[v].Add(u);

            _neighbors.TryAdd(u, []);
            _neighbors[u].Add(v);
        }

        var triplets = new HashSet<(string, string, string)>();

        foreach (var u in _neighbors.Keys)
        {
            foreach (var v in _neighbors[u])
            {
                foreach (var w in _neighbors[v])
                {
                    if (!_neighbors[w].Contains(u))
                        continue;

                    var ordered = new List<string> { u, v, w };
                    ordered.Sort();

                    triplets.Add((ordered[0], ordered[1], ordered[2]));
                }
            }
        }

        return triplets.Count(t => t.Item1.StartsWith('t') || t.Item2.StartsWith('t') || t.Item3.StartsWith('t'));
    }

    private HashSet<string> ReduceComponent(HashSet<string> component)
    {
        var components = new HashSet<HashSet<string>>(EqualityComparer<HashSet<string>>.Create(
            (a, b) => a is null ? b is null : b != null && a.SequenceEqual(b),
            x =>
            {
                HashCode c = new();
                foreach (var n in x)
                    c.Add(n);
                return c.ToHashCode();
            }
        ));

        foreach (var v in component)
        {
            var reduced = _neighbors[v].Union([v]).Intersect(component).ToHashSet();
            if (reduced.Count < component.Count)
                components.Add(reduced);
        }

        if (components.Count == 0)
            return component;

        return ReduceComponent(components.MaxBy(c => c.Count)!);
    }

    protected override object InternalPart2()
    {
        var components = new HashSet<HashSet<string>>(EqualityComparer<HashSet<string>>.Create(
            (a, b) => a is null ? b is null : b != null && a.SequenceEqual(b),
            x =>
            {
                HashCode c = new();
                foreach (var n in x)
                    c.Add(n);
                return c.ToHashCode();
            }
        ));

        foreach (var u in _neighbors.Keys)
        {
            var reduced = ReduceComponent(_neighbors[u].Union([u]).ToHashSet());
            components.Add(reduced);
        }

        var maximalComponent = components.MaxBy(c => c.Count);

        return string.Join(',', maximalComponent!.Order());
    }
}