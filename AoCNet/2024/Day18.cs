using System.Diagnostics;
using AdventOfCodeSupport;

namespace AoC._2024;

public class Day18 : AdventBase
{
    public class Node
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Distance { get; set; }

        public override string ToString()
        {
            return $"({X}, {Y}, {Distance})";
        }
    }

    protected override object InternalPart1()
    {
        var board = new char[71][];

        for (var i = 0; i < 71; i++)
        {
            board[i] = new char[71];
            for (var j = 0; j < 71; j++)
                board[i][j] = '.';
        }

        foreach (var (x, y) in Input.Lines.Select(l => l.Split(',')).Select(s => (int.Parse(s[0]), int.Parse(s[1])))
                     .Take(1024))
            board[x][y] = '#';

        var unvisited = new HashSet<Node>();
        for (var i = 0; i < 71; i++)
        {
            for (var j = 0; j < 71; j++)
            {
                if (board[i][j] == '.' && (i, j) is not (0, 0))
                    unvisited.Add(new Node { X = i, Y = j, Distance = int.MaxValue - 1 });
            }
        }


        var visited = new HashSet<Node>();

        unvisited.Add(new Node { X = 0, Y = 0, Distance = 0 });

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

        return visited.First(n => n is { X: 70, Y: 70 }).Distance;
    }

    protected override object InternalPart2()
    {
        var board = new char[71][];

        for (var i = 0; i < 71; i++)
        {
            board[i] = new char[71];
            for (var j = 0; j < 71; j++)
                board[i][j] = '.';
        }

        foreach (var (x, y, idx) in Input.Lines.Select(l => l.Split(','))
                     .Select((s, idx) => (int.Parse(s[0]), int.Parse(s[1]), idx)))
        {
            board[x][y] = '#';
            // through a manual binary search of sorts
            if (idx < 2900)
                continue;

            var unvisited = new HashSet<Node>();
            for (var i = 0; i < 71; i++)
            {
                for (var j = 0; j < 71; j++)
                {
                    if (board[i][j] == '.' && (i, j) is not (0, 0))
                        unvisited.Add(new Node { X = i, Y = j, Distance = int.MaxValue - 1 });
                }
            }

            var visited = new HashSet<Node>();

            unvisited.Add(new Node { X = 0, Y = 0, Distance = 0 });

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

            if (visited.FirstOrDefault(n => n is { X: 70, Y: 70, Distance: not int.MaxValue - 1 }) is null)
                return (x, y);
        }

        throw new UnreachableException();
    }
}