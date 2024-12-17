using System.Diagnostics;
using AdventOfCodeSupport;

namespace AoC._2024;

public class Computer
{
    public long A { get; set; }

    public long B { get; set; }

    public long C { get; set; }

    public List<long> Output { get; } = [];

    public void Run(IReadOnlyList<int> input)
    {
        for (var ip = 0; ip < input.Count; ip += 2)
        {
            switch (input[ip])
            {
                case 0:
                    A /= 1 << (int)EvaluateCombo(input[ip + 1]);
                    break;
                case 1:
                    B ^= input[ip + 1];
                    break;
                case 2:
                    B = EvaluateCombo(input[ip + 1]) % 8;
                    break;
                case 3 when A != 0:
                    ip = input[ip + 1] - 2;
                    break;
                case 4:
                    B ^= C;
                    break;
                case 5:
                    Output.Add(EvaluateCombo(input[ip + 1]) % 8);
                    break;
                case 6:
                    B = A / (1 << (int)EvaluateCombo(input[ip + 1]));
                    break;
                case 7:
                    C = A / (1 << (int)EvaluateCombo(input[ip + 1]));
                    break;
            }
        }
    }

    private long EvaluateCombo(int operand)
    {
        return operand switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => A,
            5 => B,
            6 => C,
            7 => throw new UnreachableException("combo op 7")
        };
    }
}

public class Day17 : AdventBase
{
    protected override object InternalPart1()
    {
        var a = int.Parse(Input.Lines[0][12..]);
        var b = int.Parse(Input.Lines[1][12..]);
        var c = int.Parse(Input.Lines[2][12..]);

        var program = Input.Lines[4].Split(' ')[1].Split(',').Select(int.Parse).ToList();
        var computer = new Computer
        {
            A = a,
            B = b,
            C = c
        };

        computer.Run(program);
        return string.Join(',', computer.Output);
    }

    protected override object InternalPart2()
    {
        var program = Input.Lines[4].Split(' ')[1].Split(',').Select(int.Parse).ToList();

        long i = 0;
        var cursor = program.Count - 1;
        while (cursor > 0)
        {
            var value = ((i % 8) ^ 4 ^ i / (1 << (int)((i % 8) ^ 1))) % 8;
            if (value == program[cursor])
            {
                i *= 8;
                cursor--;
            }
            else
            {
                i++;
            }
        }

        for (var j = i - 10000; j < i + 1001; j++)
        {
            var computer = new Computer
            {
                A = j,
                B = 0,
                C = 0
            };
            computer.Run(program);
            if (computer.Output.Count == program.Count && computer.Output.Zip(program).All(tuple => tuple.First == tuple.Second))
                return j;
        }

        return i;
    }
}