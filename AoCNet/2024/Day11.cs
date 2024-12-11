using AdventOfCodeSupport;

namespace AoC._2024;

public class Day11 : AdventBase
{
    private static ulong Stones(ulong n, int maxBlinks, Dictionary<(ulong, int), ulong> cache, int blink = 0)
    {
        if (cache.TryGetValue((n, blink), out var count))
            return count;

        if (blink >= maxBlinks)
        {
            cache.TryAdd((n, blink), 1);
            return 1;
        }

        if (n == 0)
        {
            var stones = Stones(1, maxBlinks, cache, blink + 1);
            cache.TryAdd((n, blink), stones);
            return stones;
        }

        var digits = (int)Math.Log10(n) + 1;
        if (digits % 2 == 0)
        {
            var stoneString = n.ToString();

            var stoneOne = ulong.Parse(stoneString[..(digits / 2)]);
            var stoneTwo = ulong.Parse(stoneString[(digits / 2)..]);

            var stones = Stones(stoneOne, maxBlinks, cache, blink + 1) + Stones(stoneTwo, maxBlinks, cache, blink + 1);
            cache.TryAdd((n, blink), stones);
            return stones;
        }

        var stones2 = Stones(n * 2024, maxBlinks, cache, blink + 1);
        cache.TryAdd((n, blink), stones2);
        return stones2;
    }

    protected override object InternalPart1()
    {
        var stones = Input.Text.Split(' ').Select(ulong.Parse).ToList();

        var cache = new Dictionary<(ulong, int), ulong>();

        return stones.Select(s => Stones(s, 25, cache))
            .Aggregate<ulong, ulong>(0, (current, n) => current + n);
    }

    protected override object InternalPart2()
    {
        var stones = Input.Text.Split(' ').Select(ulong.Parse).ToList();

        var cache = new Dictionary<(ulong, int), ulong>();
        
        return stones.Select(s => Stones(s, 75, cache))
            .Aggregate<ulong, ulong>(0, (current, n) => current + n);
    }
}