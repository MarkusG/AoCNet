using AdventOfCodeSupport;

namespace AoC._2024;

public class Day22 : AdventBase
{
    private static IEnumerable<long> GetSecretNumbers(long secret, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var product = secret * 64;
            secret ^= product;
            secret %= 16777216;
            var quotient = secret / 32;
            secret ^= quotient;
            secret %= 16777216;
            var product2 = secret * 2048;
            secret ^= product2;
            secret %= 16777216;
            yield return secret;
        }
    }

    protected override object InternalPart1()
    {
        return Input.Lines
            .Select(long.Parse)
            .Select(n => GetSecretNumbers(n, 2000).Last())
            .Sum();
    }

    private static void AddTotalsPerSequence(Dictionary<(long, long, long, long), long> totalsPerSequence, long secret, int count)
    {
        var seen = new HashSet<(long, long, long, long)>();
        (long, long, long, long) seq = (0, 0, 0, 0);
        var last = 0L;
        for (var i = 0; i < count; i++)
        {
            var product = secret * 64;
            secret ^= product;
            secret %= 16777216;
            var quotient = secret / 32;
            secret ^= quotient;
            secret %= 16777216;
            var product2 = secret * 2048;
            secret ^= product2;
            secret %= 16777216;

            seq.Item1 = seq.Item2;
            seq.Item2 = seq.Item3;
            seq.Item3 = seq.Item4;
            seq.Item4 = secret % 10 - last;

            last = secret % 10;

            if (i > 3)
            {
                if (seen.Add(seq))
                    totalsPerSequence[seq] = totalsPerSequence.GetValueOrDefault(seq, 0) + secret % 10;
            }
        }
    }

    protected override object InternalPart2()
    {
        var totalsPerSequence = new Dictionary<(long, long, long, long), long>();
        
        foreach (var n in Input.Lines.Select(long.Parse))
            AddTotalsPerSequence(totalsPerSequence, n, 2000);

        return totalsPerSequence.Values.Max();
    }
}