using AdventOfCodeSupport;

namespace AoC._2024;

public class Day16 : AdventBase
{
    public class Node
    {
        public int X { get; set; }

        public int Y { get; set; }

        // 0 east, 1 south, 2 west, 3 north
        // public int Direction { get; set; }

        public int Distance { get; set; }

        public Node(int x, int y, int direction, int distance)
        {
            X = x;
            Y = y;
            // Direction = direction;
            Distance = distance;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Distance})";
        }
    }

    protected override object InternalPart1()
    {
        var board = Input.ToBoard();

        var startX = board.Enumerate().Single(tuple => tuple.Value == 'S').X;
        var startY = board.Enumerate().Single(tuple => tuple.Value == 'S').Y;

        var endX = board.Enumerate().Single(tuple => tuple.Value == 'E').X;
        var endY = board.Enumerate().Single(tuple => tuple.Value == 'E').Y;

        var unvisitedNodes = board.Enumerate()
            .Where(tuple => tuple.Value is '.' or 'E')
            .Select(tuple => new Node(tuple.X, tuple.Y, 0, int.MaxValue))
            .ToHashSet();

        var visited = new HashSet<Node>();

        unvisitedNodes.Add(new Node(startX, startY, 0, 0));

        while (unvisitedNodes.Count > 0)
        {
            var currentNode = unvisitedNodes.MinBy(n => n.Distance)!;
            Console.WriteLine(currentNode);

            var neighbors = unvisitedNodes.Where(n =>
                (n.X - currentNode.X, n.Y - currentNode.Y) is (0, 1) or (1, 0) or (0, -1) or (-1, 0));
            
            foreach (var neighbor in neighbors)
            {
                var distanceThroughCurrent = currentNode.Distance + 1;

                if (neighbor.Distance > distanceThroughCurrent)
                    neighbor.Distance = distanceThroughCurrent;
            }

            visited.Add(currentNode);
            unvisitedNodes.Remove(currentNode);
        }
        
        return visited.First(n => n.X == endX && n.Y == endY).Distance;
    }

    protected override object InternalPart2()
    {
        throw new NotImplementedException();
    }
}