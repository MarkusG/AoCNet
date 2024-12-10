using AdventOfCodeSupport;

namespace AoC._2024;

public class Day10 : AdventBase
{
    private static List<(int X, int Y)> GetNextSteps(int[,] board, int width, int height, int x, int y)
    {
        var points = new List<(int X, int Y)>();
        if (x + 1 < width && board[x + 1, y] == board[x, y] + 1)
            points.Add((x + 1, y));
        if (x - 1 >= 0 && board[x - 1, y] == board[x, y] + 1)
            points.Add((x - 1, y));
        if (y + 1 < height && board[x, y + 1] == board[x, y] + 1)
            points.Add((x, y + 1));
        if (y - 1 >= 0 && board[x, y - 1] == board[x, y] + 1)
            points.Add((x, y - 1));
        return points;
    }

    protected override object InternalPart1()
    {
        var width = Input.Lines.First().Length;
        var height = Input.Lines.Length;

        var board = new int[width, height];

        var trailheads = new List<(int X, int Y)>();
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                board[i, j] = Input.Lines[j][i] - '0';
                if (board[i, j] == 0)
                    trailheads.Add((i, j));
            }
        }

        var score = 0;
        foreach (var reachablePoints in trailheads.Select(t => new HashSet<(int X, int Y)> { t }))
        {
            int added;
            do
            {
                added = reachablePoints
                    .SelectMany(p => GetNextSteps(board, width, height, p.X, p.Y))
                    .ToList()
                    .Sum(p => reachablePoints.Add(p) ? 1 : 0);
            } while (added > 0);

            score += reachablePoints.Count(p => board[p.X, p.Y] == 9);
        }

        return score;
    }

    public record Node(int X, int Y, List<Node> Children);

    private static void BuildTree(int[,] board, int width, int height, Node root)
    {
        var (x, y, children) = root;

        if (x + 1 < width && board[x + 1, y] == board[x, y] + 1)
            children.Add(new Node(x + 1, y, []));
        if (x - 1 >= 0 && board[x - 1, y] == board[x, y] + 1)
            children.Add(new Node(x - 1, y, []));
        if (y + 1 < height && board[x, y + 1] == board[x, y] + 1)
            children.Add(new Node(x, y + 1, []));
        if (y - 1 >= 0 && board[x, y - 1] == board[x, y] + 1)
            children.Add(new Node(x, y - 1, []));

        foreach (var n in children)
            BuildTree(board, width, height, n);
    }

    private static IEnumerable<Node> GetLeaves(Node node)
    {
        if (node.Children.Count == 0)
        {
            yield return node;
            yield break;
        }

        foreach (var l in node.Children.SelectMany(GetLeaves))
            yield return l;
    }

    protected override object InternalPart2()
    {
        var width = Input.Lines.First().Length;
        var height = Input.Lines.Length;

        var board = new int[width, height];

        var trailheads = new List<(int X, int Y)>();
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                board[i, j] = Input.Lines[j][i] - '0';
                if (board[i, j] == 0)
                    trailheads.Add((i, j));
            }
        }

        var rank = 0;
        foreach (var node in trailheads.Select(t => new Node(t.X, t.Y, [])))
        {
            BuildTree(board, width, height, node);
            var leaves = GetLeaves(node).ToList();
            rank += leaves.Count(l => board[l.X, l.Y] == 9);
        }

        return rank;
    }
}