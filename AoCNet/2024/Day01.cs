using System.Numerics;
using System.Runtime.Intrinsics;
using AdventOfCodeSupport;

namespace AoC._2024;

public class Day01 : AdventBase
{
    private static int[] _left = null!;
    private static int[] _right = null!;

    protected override void InternalOnLoad()
    {
        _left = new int[1000];
        _right = new int[1000];

        var blockIdx = 0;
        var blockSize = Vector256<int>.Count;

        do
        {
            var left = Vector256<int>.Zero;
            for (var i = 0; i < 5; i++)
            {
                left *= 10;
                var next = Vector256.Create(
                    Input.Bytes[blockIdx * blockSize * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 1) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 2) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 3) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 4) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 5) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 6) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 7) * 14 + i] - '0'
                );
                left += next;
            }

            var right = Vector256<int>.Zero;

            for (var i = 8; i < 13; i++)
            {
                right *= 10;
                var next = Vector256.Create(
                    Input.Bytes[blockIdx * blockSize * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 1) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 2) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 3) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 4) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 5) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 6) * 14 + i] - '0',
                    Input.Bytes[(blockIdx * blockSize + 7) * 14 + i] - '0'
                );
                right += next;
            }

            left.CopyTo(_left, blockIdx * blockSize);
            right.CopyTo(_right, blockIdx * blockSize);
        } while (++blockIdx * blockSize < 1000);

        Array.Sort(_left);
        Array.Sort(_right);
    }

    protected override object InternalPart1()
    {
        var result = Vector<int>.Zero;
        var index = 0;

        do
        {
            var left = new Vector<int>(_left, index);
            var right = new Vector<int>(_right, index);
            result += Vector.Abs(left - right);

            index += Vector<int>.Count;
        } while (index < _left.Length);

        return Vector.Sum(result);
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