using AdventOfCodeSupport;

namespace AoC._2024;

public class Day06 : AdventBase
{
    protected override object InternalPart1()
    {
        var lines = Input.Lines;
        var width = lines.First().Length;
        var height = lines.Length;

        var board = new char[width, height];

        var guardPos = (0, 0);

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                board[i, j] = lines[j][i];
                if (lines[j][i] == '^')
                    guardPos = (i, j);
            }
        }

        var positions = new HashSet<(int, int)>();
        positions.Add(guardPos);

        while (true)
        {
            try
            {
                while (board[guardPos.Item1, guardPos.Item2 - 1] != '#')
                {
                    guardPos.Item2 -= 1;
                    positions.Add(guardPos);
                }
            }
            catch (IndexOutOfRangeException)
            {
                break;
            }

            try
            {
                while (board[guardPos.Item1 + 1, guardPos.Item2] != '#')
                {
                    guardPos.Item1 += 1;
                    positions.Add(guardPos);
                }
            }
            catch (IndexOutOfRangeException)
            {
                break;
            }

            try
            {
                while (board[guardPos.Item1, guardPos.Item2 + 1] != '#')
                {
                    guardPos.Item2 += 1;
                    positions.Add(guardPos);
                }
            }
            catch (IndexOutOfRangeException)
            {
                break;
            }

            try
            {
                while (board[guardPos.Item1 - 1, guardPos.Item2] != '#')
                {
                    guardPos.Item1 -= 1;
                    positions.Add(guardPos);
                }
            }
            catch (IndexOutOfRangeException)
            {
                break;
            }
        }

        return positions.Count;
    }

    private bool ProducesLoop(char[,] board, (int, int, int) guardPos)
    {
        var positions = new HashSet<(int, int, int)>();

        while (true)
        {
            try
            {
                while (board[guardPos.Item1, guardPos.Item2 - 1] != '#')
                {
                    guardPos.Item2 -= 1;
                    if (!positions.Add(guardPos))
                        return true;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            guardPos.Item3 = (guardPos.Item3 + 1) % 4;

            try
            {
                while (board[guardPos.Item1 + 1, guardPos.Item2] != '#')
                {
                    guardPos.Item1 += 1;
                    if (!positions.Add(guardPos))
                        return true;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            guardPos.Item3 = (guardPos.Item3 + 1) % 4;

            try
            {
                while (board[guardPos.Item1, guardPos.Item2 + 1] != '#')
                {
                    guardPos.Item2 += 1;
                    if (!positions.Add(guardPos))
                        return true;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            guardPos.Item3 = (guardPos.Item3 + 1) % 4;

            try
            {
                while (board[guardPos.Item1 - 1, guardPos.Item2] != '#')
                {
                    guardPos.Item1 -= 1;
                    if (!positions.Add(guardPos))
                        return true;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            guardPos.Item3 = (guardPos.Item3 + 1) % 4;
        }
    }

    protected override object InternalPart2()
    {
        var lines = Input.Lines;
        var width = lines.First().Length;
        var height = lines.Length;

        var board = new char[width, height];

        // 3rd element is orientation: 0 up 1 right 2 down 3 left
        var originalPosition = (0, 0, 0);

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                board[i, j] = lines[j][i];
                if (lines[j][i] == '^')
                    originalPosition = (i, j, 0);
            }
        }

        var obstacles = new HashSet<(int, int)>();

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (i == originalPosition.Item1 && j == originalPosition.Item2)
                    continue;

                var guardPos = originalPosition;
                var prev = board[i, j];

                board[i, j] = '#';
                if (ProducesLoop(board, guardPos))
                    obstacles.Add((i, j));

                board[i, j] = prev;
            }
        }

        return obstacles.Count;
    }
}