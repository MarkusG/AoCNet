using AdventOfCodeSupport;

namespace AoC._2024;

public class Day14 : AdventBase
{
    protected override object InternalPart1()
    {
        var width = 101;
        var height = 103;
        long q1 = 0, q2 = 0, q3 = 0, q4 = 0;
        foreach (var line in Input.Lines)
        {
            var cursor = 2;
            int px = 0, py = 0, vx = 0, vy = 0;
            while (line[cursor] is >= '0' and <= '9')
            {
                px *= 10;
                px += line[cursor++] - '0';
            }

            cursor++;

            while (line[cursor] is >= '0' and <= '9')
            {
                py *= 10;
                py += line[cursor++] - '0';
            }

            cursor += 3;
            var sign = 1;

            while (line[cursor] is >= '0' and <= '9' or '-')
            {
                if (line[cursor] == '-')
                {
                    sign = -1;
                    cursor++;
                    continue;
                }

                vx *= 10;
                vx += sign * (line[cursor++] - '0');
            }

            cursor++;
            sign = 1;
            while (cursor < line.Length && line[cursor] is >= '0' and <= '9' or '-')
            {
                if (line[cursor] == '-')
                {
                    sign = -1;
                    cursor++;
                    continue;
                }

                vy *= 10;
                vy += sign * (line[cursor++] - '0');
            }

            for (var i = 0; i < 100; i++)
            {
                px = (px + vx) % width;
                py = (py + vy) % height;
            }

            if (px < 0)
                px = width + px;

            if (py < 0)
                py = height + py;

            if (px < width / 2 && py < height / 2)
                q1++;
            if (px > width / 2 && py < height / 2)
                q2++;
            if (px < width / 2 && py > height / 2)
                q3++;
            if (px > width / 2 && py > height / 2)
                q4++;
        }

        return q1 * q2 * q3 * q4;
    }

    public class Robot
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Vx { get; set; }

        public int Vy { get; set; }
    }

    protected override object InternalPart2()
    {
        var width = 101;
        var height = 103;
        var board = new char[width, height];
        var robots = new List<Robot>();

        foreach (var line in Input.Lines)
        {
            var cursor = 2;
            int px = 0, py = 0, vx = 0, vy = 0;
            while (line[cursor] is >= '0' and <= '9')
            {
                px *= 10;
                px += line[cursor++] - '0';
            }

            cursor++;

            while (line[cursor] is >= '0' and <= '9')
            {
                py *= 10;
                py += line[cursor++] - '0';
            }

            cursor += 3;
            var sign = 1;

            while (line[cursor] is >= '0' and <= '9' or '-')
            {
                if (line[cursor] == '-')
                {
                    sign = -1;
                    cursor++;
                    continue;
                }

                vx *= 10;
                vx += sign * (line[cursor++] - '0');
            }

            cursor++;
            sign = 1;
            while (cursor < line.Length && line[cursor] is >= '0' and <= '9' or '-')
            {
                if (line[cursor] == '-')
                {
                    sign = -1;
                    cursor++;
                    continue;
                }

                vy *= 10;
                vy += sign * (line[cursor++] - '0');
            }

            robots.Add(new Robot { X = px, Y = py, Vx = vx, Vy = vy });
        }

        for (var i = 0; i < 10000; i++)
        {
            foreach (var r in robots)
            {
                r.X += r.Vx;
                r.X %= width;
                r.Y += r.Vy;
                r.Y %= height;

                if (r.X < 0)
                    r.X = width + r.X;

                if (r.Y < 0)
                    r.Y = height + r.Y;
            }

            var withNeighbor = 0;
            foreach (var r in robots)
            {
                if (robots.Any(rr => r.X - rr.X is 1 or -1 or 0 && r.Y - rr.Y is 1 or -1 or 0 && rr != r))
                    withNeighbor++;
            }

            if (withNeighbor / (double)robots.Count > .5)
            {
                Console.WriteLine(i);
                for (var j = 0; j < height; j++)
                {
                    for (var k = 0; k < width; k++)
                    {
                        if (robots.Any(r => r.X == k && r.Y == j))
                            Console.Write('X');
                        else
                            Console.Write('.');
                    }
                    Console.WriteLine();
                }
            }
        }

        return 0;
    }
}