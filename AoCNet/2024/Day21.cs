using System.Text;
using AdventOfCodeSupport;

namespace AoC._2024;

public class Day21 : AdventBase
{
    private static readonly char[][] _keypad = [['7', '8', '9'], ['4', '5', '6'], ['1', '2', '3'], ['x', '0', 'A']];

    private static string GetSequenceForKeypad(string sequence, int startX, int startY)
    {
        if (sequence is "")
            return "";

        var target = sequence[0];
        var start = (X: startX, Y: startY);
        var targetNode = (X: 0, Y: 0);

        var distances = new Dictionary<(int X, int Y), int>();
        var predecessors = new Dictionary<(int X, int Y), (int X, int Y)>();

        var q = new List<(int X, int Y)>();

        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                distances.Add((j, i), int.MaxValue - 1);
                q.Add((j, i));
                if (_keypad[i][j] == target)
                    targetNode = (j, i);
            }
        }

        q.Remove((3, 0));

        distances[start] = 0;

        while (q.Count > 0)
        {
            var u = q.MinBy(v => distances[v]);
            if (_keypad[u.Y][u.X] == target)
                break;

            q.Remove(u);

            foreach (var v in q.Where(v => (u.X - v.X, u.Y - v.Y) is (1, 0) or (0, 1) or (-1, 0) or (0, -1)))
            {
                var d = distances[u] + 1;

                if (d <= distances[v])
                {
                    distances[v] = d;
                    predecessors[v] = u;
                }
            }
        }

        var sb = new StringBuilder();
        var stack = new Stack<char>(['A']);
        var node = targetNode;
        while (predecessors.TryGetValue(node, out var u))
        {
            var movement = (node.X - u.X, node.Y - u.Y) switch
            {
                (1, 0) => '>',
                (0, 1) => 'v',
                (-1, 0) => '<',
                (0, -1) => '^',
                _ => throw new ArgumentOutOfRangeException()
            };
            stack.Push(movement);
            node = u;
        }

        while (stack.TryPop(out var c))
            sb.Append(c);

        sb.Append(GetSequenceForKeypad(sequence[1..], targetNode.X, targetNode.Y));
        return sb.ToString();
    }

    private static readonly char[][] _dpad = [['x', '^', 'A'], ['<', 'v', '>']];

    private static string GetSequenceForDpad(string sequence, int startX, int startY)
    {
        if (sequence is "")
            return "";

        var target = sequence[0];
        var start = (X: startX, Y: startY);
        var targetNode = (X: 0, Y: 0);

        var distances = new Dictionary<(int X, int Y), int>();
        var predecessors = new Dictionary<(int X, int Y), (int X, int Y)>();

        var q = new List<(int X, int Y)>();

        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                distances.Add((j, i), int.MaxValue - 1);
                q.Add((j, i));
                if (_dpad[i][j] == target)
                    targetNode = (j, i);
            }
        }

        q.Remove((0, 0));

        distances[start] = 0;

        while (q.Count > 0)
        {
            var u = q.MinBy(v => distances[v]);
            if (_dpad[u.Y][u.X] == target)
                break;

            q.Remove(u);

            foreach (var v in q.Where(v => (u.X - v.X, u.Y - v.Y) is (1, 0) or (0, 1) or (-1, 0) or (0, -1)))
            {
                var d = distances[u] + 1;
                if (d < distances[v])
                {
                    distances[v] = d;
                    predecessors[v] = u;
                }
            }
        }

        var sb = new StringBuilder();
        var stack = new Stack<char>(['A']);
        var node = targetNode;
        while (predecessors.TryGetValue(node, out var u))
        {
            var movement = (node.X - u.X, node.Y - u.Y) switch
            {
                (1, 0) => '>',
                (0, 1) => 'v',
                (-1, 0) => '<',
                (0, -1) => '^',
                _ => throw new ArgumentOutOfRangeException()
            };
            stack.Push(movement);
            node = u;
        }

        while (stack.TryPop(out var c))
            sb.Append(c);

        sb.Append(GetSequenceForDpad(sequence[1..], targetNode.X, targetNode.Y));
        return sb.ToString();
    }

    protected override object InternalPart1()
    {
        var sum = 0L;
        foreach (var code in Input.Lines)
        {
            var kpSequence = GetSequenceForKeypad(code, 2, 3);
            var dp1Sequence = GetSequenceForDpad(kpSequence, 2, 0);
            var dp2Sequence = GetSequenceForDpad(dp1Sequence, 2, 0);
            
            Console.WriteLine(kpSequence);
            Console.WriteLine(dp1Sequence);
            Console.WriteLine(dp2Sequence);

            long numeric = long.Parse(code[..3]);
            sum += dp2Sequence.Length * numeric;
            Console.WriteLine();
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        throw new NotImplementedException();
    }
}