using AdventOfCodeSupport;

namespace AoC._2024;

public class Day07 : AdventBase
{
    private static IEnumerable<long> GetPossibleResults(IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            var testValue = ulong.Parse(line.Split(':')[0]);
            var numbers = line.Split(' ')[1..].Select(ulong.Parse).ToArray();
            for (ulong i = 0; i < (ulong)1 << (numbers.Length - 1); i++)
            {
                var accumulator = numbers[0];
                for (var j = 1; j < numbers.Length; j++)
                {
                    var pow = (ulong)1 << j - 1;
                    if ((pow & i) != 0)
                    {
                        accumulator *= numbers[j];
                    }
                    else
                    {
                        accumulator += numbers[j];
                    }
                }

                if (accumulator == testValue)
                {
                    yield return (long)accumulator;
                    break;
                }
            }
        }
    }

    protected override object InternalPart1()
    {
        var results = GetPossibleResults(Input.Lines);
        return results.Sum();
    }

    private static IEnumerable<long> GetPossibleResultsPart2(IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            var testValue = ulong.Parse(line.Split(':')[0]);
            var numbers = line.Split(' ')[1..].Select(ulong.Parse).ToArray();

            for (var i = 0; i < Math.Pow(3, numbers.Length - 1); i++)
            {
                var places = new int[numbers.Length - 1];
                var ii = 0;
                var k = i;
                while (k > 0)
                {
                    places[ii++] = k % 3;
                    k /= 3;
                }

                var accumulator = numbers[0];
                for (var j = 1; j < numbers.Length; j++)
                {
                    if (places[j - 1] == 0)
                    {
                        accumulator *= numbers[j];
                    }
                    else if (places[j - 1] == 1)
                    {
                        accumulator += numbers[j];
                    }
                    else if (places[j - 1] == 2)
                    {
                        accumulator = ulong.Parse($"{accumulator}{numbers[j]}");
                    }
                }

                if (accumulator == testValue)
                {
                    yield return (long)accumulator;
                    break;
                }
            }
        }
    }

    protected override object InternalPart2()
    {
        var results = GetPossibleResultsPart2(Input.Lines);
        return results.Sum();
    }
}