using AdventOfCodeSupport;

namespace AoC._2024;

public class Day16 : AdventBase
{
    protected override object InternalPart1()
    {
        var board = Input.ToBoard();

        var startX = board.Enumerate().Single(tuple => tuple.Value == 'S').X;
        var startY = board.Enumerate().Single(tuple => tuple.Value == 'S').Y;
        var start = (X: startX, Y: startY, Direction: 'e');

        var endX = board.Enumerate().Single(tuple => tuple.Value == 'E').X;
        var endY = board.Enumerate().Single(tuple => tuple.Value == 'E').Y;

        var distances = new Dictionary<(int X, int Y, char Direction), int> { { start, 0 } };
        var predecessors = new Dictionary<(int X, int Y, char Distance), (int X, int Y, char Distance)>();
        var visited = new HashSet<(int X, int Y, char Direction)>();

        while (true)
        {
            if (distances.All(kv => visited.Contains(kv.Key)))
                break;
            
            var (u, dist) = distances.Where(kv => !visited.Contains(kv.Key)).MinBy(kv => kv.Value);
            visited.Add(u);

            ((int X, int Y, char Direction) Point, int Distance)[] neighbors = u.Direction switch
            {
                'e' => new[]
                {
                    ((u.X + 1, u.Y, 'e'), 1),
                    ((u.X, u.Y + 1, 's'), 1001),
                    ((u.X, u.Y - 1, 'n'), 1001),
                },
                's' =>
                [
                    ((u.X, u.Y + 1, 's'), 1),
                    ((u.X + 1, u.Y, 'e'), 1001),
                    ((u.X - 1, u.Y, 'w'), 1001)
                ],
                'w' =>
                [
                    ((u.X - 1, u.Y, 'w'), 1),
                    ((u.X, u.Y + 1, 's'), 1001),
                    ((u.X, u.Y - 1, 'n'), 1001),
                ],
                'n' =>
                [
                    ((u.X, u.Y - 1, 'n'), 1),
                    ((u.X + 1, u.Y, 'e'), 1001),
                    ((u.X - 1, u.Y, 'w'), 1001)
                ]
            };

            foreach (var v in neighbors.Where(n => board[n.Point.X, n.Point.Y] != '#'))
            {
                var alt = dist + v.Distance;
                distances.TryAdd(v.Point, int.MaxValue - 1);
                if (alt < distances[v.Point])
                {
                    distances[v.Point] = alt;
                    predecessors[v.Point] = u;
                }
            }
        }

        var distance = distances
            .Where(d => d.Key.X == endX && d.Key.Y == endY)
            .Select(d => d.Value)
            .Min();

        return distance;
    }

    protected override object InternalPart2()
    {
        var board = Input.ToBoard();

        var startX = board.Enumerate().Single(tuple => tuple.Value == 'S').X;
        var startY = board.Enumerate().Single(tuple => tuple.Value == 'S').Y;
        var start = (X: startX, Y: startY, Direction: 'e');

        var endX = board.Enumerate().Single(tuple => tuple.Value == 'E').X;
        var endY = board.Enumerate().Single(tuple => tuple.Value == 'E').Y;

        var distances = new Dictionary<(int X, int Y, char Direction), int> { { start, 0 } };
        var predecessors = new Dictionary<(int X, int Y, char Distance), (int X, int Y, char Distance)>();
        var visited = new HashSet<(int X, int Y, char Direction)>();

        while (true)
        {
            if (distances.All(kv => visited.Contains(kv.Key)))
                break;
            
            var (u, dist) = distances.Where(kv => !visited.Contains(kv.Key)).MinBy(kv => kv.Value);
            visited.Add(u);

            ((int X, int Y, char Direction) Point, int Distance)[] neighbors = u.Direction switch
            {
                'e' => new[]
                {
                    ((u.X + 1, u.Y, 'e'), 1),
                    ((u.X, u.Y + 1, 's'), 1001),
                    ((u.X, u.Y - 1, 'n'), 1001),
                },
                's' =>
                [
                    ((u.X, u.Y + 1, 's'), 1),
                    ((u.X + 1, u.Y, 'e'), 1001),
                    ((u.X - 1, u.Y, 'w'), 1001)
                ],
                'w' =>
                [
                    ((u.X - 1, u.Y, 'w'), 1),
                    ((u.X, u.Y + 1, 's'), 1001),
                    ((u.X, u.Y - 1, 'n'), 1001),
                ],
                'n' =>
                [
                    ((u.X, u.Y - 1, 'n'), 1),
                    ((u.X + 1, u.Y, 'e'), 1001),
                    ((u.X - 1, u.Y, 'w'), 1001)
                ]
            };

            foreach (var v in neighbors.Where(n => board[n.Point.X, n.Point.Y] != '#'))
            {
                var alt = dist + v.Distance;
                distances.TryAdd(v.Point, int.MaxValue - 1);
                if (alt < distances[v.Point])
                {
                    distances[v.Point] = alt;
                    predecessors[v.Point] = u;
                }
            }
        }

        var end = distances.Where(d => d.Key.X == endX && d.Key.Y == endY);

        foreach (var n in end)
        {
            Console.WriteLine($"{n.Key}: {n.Value}");
        }

        return 0;
    }
}