using AdventOfCodeSupport;

namespace AoC._2024;

public class Day11 : AdventBase
{
    protected override object InternalPart1()
    {
        var stones = Input.Text.Split(' ').Select(ulong.Parse).ToList();
        const int blinks = 25;
        for (var i = 0; i < blinks; i++)
        {
            for (var j = 0; j < stones.Count; j++)
            {
                var stone = stones[j];
                if (stone == 0)
                {
                    stones[j] = 1;
                    continue;
                }

                var digits = (int)Math.Log10(stone) + 1;
                if (digits % 2 == 0)
                {
                    var stoneString = stone.ToString();

                    var stoneOne = ulong.Parse(stoneString[..(digits / 2)]);
                    var stoneTwo = ulong.Parse(stoneString[(digits / 2)..]);

                    stones[j] = stoneTwo;
                    stones.Insert(j++, stoneOne);
                    continue;
                }

                stones[j] = stone * 2024;
            }
        }

        return stones.Count;
    }

    private readonly Dictionary<(ulong, int), ulong> _stoneCounts = new();

    private ulong Stones(ulong n, int maxBlinks, int blink = 0)
    {
        if (_stoneCounts.TryGetValue((n, blink), out var count))
            return count;

        if (blink >= maxBlinks)
        {
            _stoneCounts.TryAdd((n, blink), 1);
            return 1;
        }

        if (n == 0)
        {
            var stones = Stones(1, maxBlinks, blink + 1);
            _stoneCounts.TryAdd((n, blink), stones);
            return stones;
        }

        var digits = (int)Math.Log10(n) + 1;
        if (digits % 2 == 0)
        {
            var stoneString = n.ToString();

            var stoneOne = ulong.Parse(stoneString[..(digits / 2)]);
            var stoneTwo = ulong.Parse(stoneString[(digits / 2)..]);

            var stones = Stones(stoneOne, maxBlinks, blink + 1) + Stones(stoneTwo, maxBlinks, blink + 1);
            _stoneCounts.TryAdd((n, blink), stones);
            return stones;
        }

        var stones2 = Stones(n * 2024, maxBlinks, blink + 1);
        _stoneCounts.TryAdd((n, blink), stones2);
        return stones2;
    }

    protected override object InternalPart2()
    {
        var stones = Input.Text.Split(' ').Select(ulong.Parse).ToList();
        const int blinks = 75;

        ulong count = 0;
        foreach (var n in stones.Select(s => Stones(s, blinks)))
            count += n;

        return count;
    }
}