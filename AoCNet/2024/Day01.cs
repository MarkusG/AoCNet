﻿using AdventOfCodeSupport;

namespace AoC._2024;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var lines = Input.Lines;
        var left = new List<int>();
        var right = new List<int>();
        foreach (var l in lines)
        {
            left.Add(int.Parse(l[..5]));
            right.Add(int.Parse(l[7..]));
        }
        
        var totalDistance = left
            .Order()
            .Zip(right.Order())
            .Select(x => Math.Abs(x.First - x.Second))
            .Sum();

        return totalDistance;
    }

    protected override object InternalPart2()
    {
        var lines = Input.Lines;
        var left = new List<int>();
        var right = new List<int>();
        foreach (var l in lines)
        {
            left.Add(int.Parse(l[..5]));
            right.Add(int.Parse(l[7..]));
        }
        
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