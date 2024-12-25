using AdventOfCodeSupport;

namespace AoC._2024;

public class Day25 : AdventBase
{
    protected override object InternalPart1()
    {
        var blocks = Input.Lines.Chunk(8).Select(c => c[..7]).ToList();
        var keyBlocks = blocks.Where(b => b[0] == ".....").ToList();
        var lockBlocks = blocks.Where(b => b[0] == "#####").ToList();

        var keys = keyBlocks.Select(b =>
        {
            (int, int, int, int, int) key = (0, 0, 0, 0, 0);
            foreach (var l in b)
            {
                if (l[0] == '#')
                    key.Item1++;
                if (l[1] == '#')
                    key.Item2++;
                if (l[2] == '#')
                    key.Item3++;
                if (l[3] == '#')
                    key.Item4++;
                if (l[4] == '#')
                    key.Item5++;
            }

            return key;
        }).ToList();

        var locks = lockBlocks.Select(b =>
        {
            (int, int, int, int, int) @lock = (0, 0, 0, 0, 0);
            foreach (var l in b)
            {
                if (l[0] == '#')
                    @lock.Item1++;
                if (l[1] == '#')
                    @lock.Item2++;
                if (l[2] == '#')
                    @lock.Item3++;
                if (l[3] == '#')
                    @lock.Item4++;
                if (l[4] == '#')
                    @lock.Item5++;
            }

            return @lock;
        }).ToList();

        var count = 0;
        foreach (var k in keys)
        {
            foreach (var l in locks)
            {
                var sum = (k.Item1 + l.Item1,
                        k.Item2 + l.Item2,
                        k.Item3 + l.Item3,
                        k.Item4 + l.Item4,
                        k.Item5 + l.Item5
                    );

                if (sum is (< 8, < 8, < 8, < 8, < 8))
                    count++;
            }
        }

        return count;
    }

    protected override object InternalPart2()
    {
        throw new NotImplementedException();
    }
}