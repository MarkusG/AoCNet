using AdventOfCodeSupport;

namespace AoC._2024;

public class Day13 : AdventBase
{
    private static (long A, long B) Rref(double[,] matrix)
    {
        var a11 = matrix[0, 0];
        matrix[0, 0] = 1;
        matrix[1, 0] /= a11;
        matrix[2, 0] /= a11;

        var a12 = matrix[0, 1];
        matrix[0, 1] = 0;
        matrix[1, 1] -= a12 * matrix[1, 0];
        matrix[2, 1] -= a12 * matrix[2, 0];

        matrix[2, 1] /= matrix[1, 1];
        matrix[1, 1] = 1;

        var a21 = matrix[1, 0];
        matrix[1, 0] = 0;
        matrix[2, 0] -= a21 * matrix[2, 1];

        long a = (long)matrix[2, 0], b = (long)matrix[2, 1];

        if (matrix[2, 0] - a > .01)
        {
            if (a - matrix[2, 0] < -.99)
                a += 1;
            else
                a = 0;
        }

        if (matrix[2, 1] - b > .01)
        {
            if (b - matrix[2, 1] < -.99)
                b += 1;
            else
                b = 0;
        }

        if (a == 0 || b == 0)
            return (0, 0);
        
        return (a, b);
    }

    protected override object InternalPart1()
    {
        var lines = Input.Lines;
        long tokens = 0;
        foreach (var chunk in lines.Chunk(4))
        {
            var aX = int.Parse(chunk[0][12..14]);
            var aY = int.Parse(chunk[0][18..]);

            var bX = int.Parse(chunk[1][12..14]);
            var bY = int.Parse(chunk[1][18..]);

            var prizeX = 0;
            var prizeY = 0;

            var cursor = 0;

            while (chunk[2][cursor] is < '0' or > '9')
                cursor++;

            while (chunk[2][cursor] is >= '0' and <= '9')
            {
                prizeX *= 10;
                prizeX += chunk[2][cursor] - '0';
                cursor++;
            }

            while (chunk[2][cursor] is < '0' or > '9')
                cursor++;

            while (cursor < chunk[2].Length && chunk[2][cursor] is >= '0' and <= '9')
            {
                prizeY *= 10;
                prizeY += chunk[2][cursor] - '0';
                cursor++;
            }

            var matrix = new double[3, 2];

            matrix[0, 0] = aX;
            matrix[1, 0] = bX;
            matrix[0, 1] = aY;
            matrix[1, 1] = bY;
            matrix[2, 0] = prizeX;
            matrix[2, 1] = prizeY;

            var (a, b) = Rref(matrix);
            if (aX * a + bX * b == prizeX && aY * a + bY * b == prizeY)
                tokens += a * 3 + b;
        }

        return tokens;
    }

    protected override object InternalPart2()
    {
        var lines = Input.Lines;
        long tokens = 0;
        foreach (var chunk in lines.Chunk(4))
        {
            var aX = int.Parse(chunk[0][12..14]);
            var aY = int.Parse(chunk[0][18..]);

            var bX = int.Parse(chunk[1][12..14]);
            var bY = int.Parse(chunk[1][18..]);

            long prizeX = 0;
            long prizeY = 0;

            var cursor = 0;

            while (chunk[2][cursor] is < '0' or > '9')
                cursor++;

            while (chunk[2][cursor] is >= '0' and <= '9')
            {
                prizeX *= 10;
                prizeX += chunk[2][cursor] - '0';
                cursor++;
            }

            while (chunk[2][cursor] is < '0' or > '9')
                cursor++;

            while (cursor < chunk[2].Length && chunk[2][cursor] is >= '0' and <= '9')
            {
                prizeY *= 10;
                prizeY += chunk[2][cursor] - '0';
                cursor++;
            }

            var matrix = new double[3, 2];

            prizeX += 10000000000000;
            prizeY += 10000000000000;

            matrix[0, 0] = aX;
            matrix[1, 0] = bX;
            matrix[0, 1] = aY;
            matrix[1, 1] = bY;
            matrix[2, 0] = prizeX;
            matrix[2, 1] = prizeY;

            var (a, b) = Rref(matrix);
            if (aX * a + bX * b == prizeX && aY * a + bY * b == prizeY)
                tokens += a * 3 + b;
        }

        return tokens;
    }
}