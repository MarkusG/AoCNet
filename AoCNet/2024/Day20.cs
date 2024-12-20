using AdventOfCodeSupport;

namespace AoC._2024;

public class Day20 : AdventBase
{
    private class Node
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Distance { get; set; }
    }

    protected override object InternalPart1()
    {
        var board = Input.ToBoard();

        var unvisited = new HashSet<Node>();
        for (var i = 0; i < board.Height; i++)
        {
            for (var j = 0; j < board.Width; j++)
            {
                if (board[j, i] is '.' or 'E')
                    unvisited.Add(new Node { X = j, Y = i, Distance = int.MaxValue - 1 });
            }
        }

        var start = board.Enumerate().First(t => t.Value == 'S');
        var end = board.Enumerate().First(t => t.Value == 'E');

        var visited = new HashSet<Node>();

        unvisited.Add(new Node { X = start.X, Y = start.Y, Distance = 0 });

        while (unvisited.Count > 0)
        {
            var currentNode = unvisited.MinBy(n => n.Distance)!;

            var neighbors = unvisited.Where(n =>
                (n.X - currentNode.X, n.Y - currentNode.Y) is (0, 1) or (1, 0) or (0, -1) or (-1, 0));

            foreach (var neighbor in neighbors)
            {
                var distanceThroughCurrent = currentNode.Distance + 1;

                if (neighbor.Distance > distanceThroughCurrent)
                    neighbor.Distance = distanceThroughCurrent;
            }

            visited.Add(currentNode);
            unvisited.Remove(currentNode);
        }

        var baseDistance = visited.First(n => n.X == end.X && n.Y == end.Y).Distance;
        var saves = new List<int>();

        foreach (var v in visited)
        {
            foreach (var u in visited)
            {
                if ((u.X - v.X, u.Y - v.Y) is not ((2, 0) or (-2, 0) or (0, 2) or (0, -2) or (1, 1) or (1, -1) or (-1, 1) or (-1, -1)))
                    continue;

                if (v.Distance > u.Distance)
                {
                    var distanceToV = baseDistance - v.Distance;
                    var distanceToU = baseDistance - u.Distance;
                    var save = distanceToU - distanceToV - 2;
                    if (save > 0)
                        saves.Add(save);
                }
            }
        }

        return saves.Count(s => s >= 100);
    }

    private static int LengthOfPathThroughWall(Board<char> board, int startX, int startY, int endX, int endY)
    {
        var unvisited = new HashSet<Node>();
        for (var i = 0; i < board.Height; i++)
        {
            for (var j = 0; j < board.Width; j++)
            {
                if (board[j, i] is '#')
                    unvisited.Add(new Node { X = j, Y = i, Distance = int.MaxValue - 1 });
            }
        }

        var visited = new HashSet<Node>();

        unvisited.Add(new Node { X = startX, Y = startY, Distance = 0 });
        unvisited.Add(new Node { X = endX, Y = endY, Distance = int.MaxValue - 1 });

        while (unvisited.Count > 0)
        {
            var currentNode = unvisited.MinBy(n => n.Distance)!;

            var neighbors = unvisited.Where(n =>
                (n.X - currentNode.X, n.Y - currentNode.Y) is (0, 1) or (1, 0) or (0, -1) or (-1, 0));

            foreach (var neighbor in neighbors)
            {
                var distanceThroughCurrent = currentNode.Distance + 1;

                if (neighbor.Distance > distanceThroughCurrent)
                    neighbor.Distance = distanceThroughCurrent;
            }

            visited.Add(currentNode);
            unvisited.Remove(currentNode);
        }

        return visited.First(n => n.X == endX && n.Y == endY).Distance;
    }

    protected override object InternalPart2()
    {
        var board = Input.ToBoard();

        var unvisited = new HashSet<Node>();
        for (var i = 0; i < board.Height; i++)
        {
            for (var j = 0; j < board.Width; j++)
            {
                if (board[j, i] is '.' or 'E')
                    unvisited.Add(new Node { X = j, Y = i, Distance = int.MaxValue - 1 });
            }
        }

        var start = board.Enumerate().First(t => t.Value == 'S');
        var end = board.Enumerate().First(t => t.Value == 'E');

        var visited = new HashSet<Node>();

        unvisited.Add(new Node { X = start.X, Y = start.Y, Distance = 0 });

        while (unvisited.Count > 0)
        {
            var currentNode = unvisited.MinBy(n => n.Distance)!;

            var neighbors = unvisited.Where(n =>
                (n.X - currentNode.X, n.Y - currentNode.Y) is (0, 1) or (1, 0) or (0, -1) or (-1, 0));

            foreach (var neighbor in neighbors)
            {
                var distanceThroughCurrent = currentNode.Distance + 1;

                if (neighbor.Distance > distanceThroughCurrent)
                    neighbor.Distance = distanceThroughCurrent;
            }

            visited.Add(currentNode);
            unvisited.Remove(currentNode);
        }

        var baseDistance = visited.First(n => n.X == end.X && n.Y == end.Y).Distance;
        var saves = new HashSet<(int UX, int UY, int VX, int VY, int Saved)>();

        foreach (var v in visited)
        {
            foreach (var u in visited)
            {
                if (v.Distance < u.Distance)
                    continue;

                var cheatLength = LengthOfPathThroughWall(board, u.X, u.Y, v.X, v.Y);
                if (cheatLength > 20)
                    continue;

                var distanceToV = baseDistance - v.Distance;
                var distanceToU = baseDistance - u.Distance;
                var save = distanceToU - distanceToV - cheatLength;
                if (save > 0)
                    saves.Add((u.X, u.Y, v.X, v.Y, save));
            }
        }

        foreach (var g in saves.GroupBy(s => s.Saved).Where(g => g.Key >= 50))
            Console.WriteLine($"{g.Count()} saving {g.Key}ps");

        return 0;
    }
}