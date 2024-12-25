using AdventOfCodeSupport;

namespace AoC._2024;

public class Day24 : AdventBase
{
    private static readonly Dictionary<string, string> _sources = [];
    private static int _xBits;
    private static int _yBits;
    private static int _zBits;

    protected override void InternalOnLoad()
    {
        foreach (var line in Input.Lines.TakeWhile(l => !string.IsNullOrEmpty(l)))
        {
            var split = line.Split(": ");
            _sources.Add(split[0], split[1]);
        }

        foreach (var line in Input.Lines.SkipWhile(l => !string.IsNullOrEmpty(l)).Skip(1))
        {
            var split = line.Split(" -> ");
            _sources.Add(split[1], split[0]);
        }

        _xBits = _sources.Keys.Count(k => k.StartsWith('x'));
        _yBits = _sources.Keys.Count(k => k.StartsWith('y'));
        _zBits = _sources.Keys.Count(k => k.StartsWith('z'));
    }

    private static ulong Evaluate(string wire)
    {
        var source = _sources[wire];

        if (source is "0")
            return 0;

        if (source is "1")
            return 1;

        var split = source.Split(' ');
        var left = split[0];
        var op = split[1];
        var right = split[2];

        return op switch
        {
            "AND" => Evaluate(left) & Evaluate(right),
            "OR" => Evaluate(left) | Evaluate(right),
            "XOR" => Evaluate(left) ^ Evaluate(right),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    protected override object InternalPart1()
    {
        ulong result = 0;
        for (var i = 0; i < _zBits; i++)
            result |= Evaluate($"z{i:00}") << i;

        return result;
    }

    private static List<string> _faultyWires = [];

    private static ulong EvaluateAndFlag(string wire, bool faulty)
    {
        var source = _sources[wire];
        if (faulty)
            _faultyWires.Add(wire);

        if (source is "0")
            return 0;

        if (source is "1")
            return 1;

        var split = source.Split(' ');
        var left = split[0];
        var op = split[1];
        var right = split[2];

        var value = op switch
        {
            "AND" => EvaluateAndFlag(left, faulty) & EvaluateAndFlag(right, faulty),
            "OR" => EvaluateAndFlag(left, faulty) | EvaluateAndFlag(right, faulty),
            "XOR" => EvaluateAndFlag(left, faulty) ^ EvaluateAndFlag(right, faulty),
            _ => throw new ArgumentOutOfRangeException()
        };

        return value;
    }

    protected override object InternalPart2()
    {
        ulong x = 0;
        for (var i = 0; i < _xBits; i++)
            x |= Evaluate($"x{i:00}") << i;

        ulong y = 0;
        for (var i = 0; i < _yBits; i++)
            y |= Evaluate($"y{i:00}") << i;

        var sum = x + y;

        ulong z = 0;
        for (var i = 0; i < _zBits; i++)
            z |= Evaluate($"z{i:00}") << i;

        Console.WriteLine($"x: {x:b64}");
        Console.WriteLine($"y: {y:b64}");
        Console.WriteLine($"+: {x + y:b64}");
        Console.WriteLine($"z: {z:b64}");
        Console.WriteLine($"^: {(x + y) ^ z:b64}");

        return 0;
    }
}