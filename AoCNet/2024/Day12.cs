using AdventOfCodeSupport;

namespace AoC._2024;

public class Day12 : AdventBase
{
    private static HashSet<(int X, int Y)> GetRegion(Board<char> board, int x, int y)
    {
        var q = new Queue<(int X, int Y)>();
        var filled = new HashSet<(int X, int Y)>();

        var type = board[x, y]!.Value;

        q.Enqueue((x, y));

        while (q.Count > 0)
        {
            var node = q.Dequeue();

            if (board[node.X, node.Y] is not { } v)
                continue;

            if (filled.Contains(node))
                continue;

            if (v == type)
            {
                filled.Add((node.X, node.Y));
                q.Enqueue((node.X + 1, node.Y));
                q.Enqueue((node.X - 1, node.Y));
                q.Enqueue((node.X, node.Y + 1));
                q.Enqueue((node.X, node.Y - 1));
            }
        }

        return filled;
    }

    private static int GetPerimeter(Board<char> board, HashSet<(int X, int Y)> region)
    {
        var perimeter = 0;
        foreach (var (x, y) in region)
        {
            var v = board[x, y];

            if (board[x + 1, y] is not { } right || right != v)
                perimeter++;

            if (board[x - 1, y] is not { } left || left != v)
                perimeter++;

            if (board[x, y + 1] is not { } down || down != v)
                perimeter++;

            if (board[x, y - 1] is not { } up || up != v)
                perimeter++;
        }

        return perimeter;
    }

    protected override object InternalPart1()
    {
        var board = Input.ToBoard();

        var cost = 0;

        var filled = new HashSet<(int X, int Y)>();
        foreach (var (x, y, _) in board.Enumerate())
        {
            if (filled.Contains((x, y)))
                continue;

            var region = GetRegion(board, x, y);
            var perimeter = GetPerimeter(board, region);
            cost += region.Count * perimeter;

            foreach (var node in region)
                filled.Add(node);
        }

        return cost;
    }

    private static int GetDiscountedPerimeter(Board<char> board, HashSet<(int X, int Y)> region)
    {
        var perimeter = 0;
        var point = region.First();
        var value = board[point.X, point.Y]!.Value;

        for (var i = 0; i < board.Width; i++)
        {
            for (var j = 0; j < board.Height; j++)
            {
                var edge = 0;
                while (region.Contains((i, j)) && (board[i + 1, j] is not { } vv || vv != value))
                {
                    edge = 1;
                    j++;
                }

                perimeter += edge;
            }
        }

        for (var i = 0; i < board.Width; i++)
        {
            for (var j = 0; j < board.Height; j++)
            {
                var edge = 0;
                while (region.Contains((i, j)) && (board[i - 1, j] is not { } vv || vv != value))
                {
                    edge = 1;
                    j++;
                }

                perimeter += edge;
            }
        }

        for (var i = 0; i < board.Height; i++)
        {
            for (var j = 0; j < board.Width; j++)
            {
                var edge = 0;
                while (region.Contains((j, i)) && (board[j, i + 1] is not { } vv || vv != value))
                {
                    edge = 1;
                    j++;
                }

                perimeter += edge;
            }
        }

        for (var i = 0; i < board.Height; i++)
        {
            for (var j = 0; j < board.Width; j++)
            {
                var edge = 0;
                while (region.Contains((j, i)) && (board[j, i - 1] is not { } vv || vv != value))
                {
                    edge = 1;
                    j++;
                }

                perimeter += edge;
            }
        }

        return perimeter;
    }

    protected override object InternalPart2()
    {
        var board = Input.ToBoard();

        var cost = 0;

        var filled = new HashSet<(int X, int Y)>();
        foreach (var (x, y, _) in board.Enumerate())
        {
            if (filled.Contains((x, y)))
                continue;

            var region = GetRegion(board, x, y);
            var perimeter = GetDiscountedPerimeter(board, region);
            cost += region.Count * perimeter;

            foreach (var node in region)
                filled.Add(node);
        }

        return cost;
    }
}