using AdventOfCodeSupport;

namespace AoC._2024;

public class Day08 : AdventBase
{
    protected override object InternalPart1()
    {
        var board = Input.ToBoard();
        var antennas = new List<(int X, int Y, char Value)>();
        foreach (var (x, y, value) in board.Enumerate())
        {
            if (value != '.')
                antennas.Add((x, y, value));
        }

        var antinodes = new HashSet<(int X, int Y)>();

        foreach (var (x1, y1, value) in antennas)
        {
            foreach (var (x2, y2, _) in antennas.Where(a => a.X != x1 && a.Y != y1 && a.Value == value))
            {
                var (dx, dy) = (x1 - x2, y1 - y2);
                if (board[x2 - dx, y2 - dy] is not null)
                    antinodes.Add((x2 - dx, y2 - dy));
            }
        }

        return antinodes.Count;
    }

    protected override object InternalPart2()
    {
        var board = Input.ToBoard();
        var antennas = new List<(int X, int Y, char Value)>();
        foreach (var (x, y, value) in board.Enumerate())
        {
            if (value != '.')
                antennas.Add((x, y, value));
        }

        var antinodes = new HashSet<(int X, int Y)>();

        foreach (var (x1, y1, value) in antennas)
        {
            foreach (var (x2, y2, _) in antennas.Where(a => a.X != x1 && a.Y != y1 && a.Value == value))
            {
                var (dx, dy) = (x1 - x2, y1 - y2);
                var (x3, y3) = (x2, y2);
                while (board[x3, y3] is not null)
                {
                    antinodes.Add((x3, y3));
                    (x3, y3) = (x3 - dx, y3 - dy);
                }
            }
        }

        return antinodes.Count;
    }
}