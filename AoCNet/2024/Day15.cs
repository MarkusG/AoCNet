using System.Diagnostics;
using AdventOfCodeSupport;

namespace AoC._2024;

public class Day15 : AdventBase
{
    private static void Print(char[,] board, int width, int height)
    {
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
                Console.Write(board[j, i]);
            Console.WriteLine();
        }
    }

    private static bool TryMove(char[,] board, int x, int y, int dx, int dy)
    {
        if (board[x + dx, y + dy] == '#')
            return false;

        if (board[x + dx, y + dy] == '.')
        {
            board[x + dx, y + dy] = board[x, y];
            board[x, y] = '.';
            return true;
        }

        if (board[x + dx, y + dy] == 'O')
        {
            if (TryMove(board, x + dx, y + dy, dx, dy))
            {
                board[x + dx, y + dy] = board[x, y];
                board[x, y] = '.';
                return true;
            }
        }

        return false;
    }

    protected override object InternalPart1()
    {
        var width = Input.Lines.First(l => l[0] == '#').Length;
        var height = Input.Lines.Count(l => l.FirstOrDefault() == '#');

        var board = new char[width, height];

        var (robotX, robotY) = (0, 0);

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                board[j, i] = Input.Lines[i][j];
                if (Input.Lines[i][j] == '@')
                    (robotX, robotY) = (j, i);
            }
        }

        foreach (var d in Input.Lines[(height + 1)..].SelectMany(c => c))
        {
            switch (d)
            {
                case '^':
                    if (TryMove(board, robotX, robotY, 0, -1))
                        robotY -= 1;
                    break;
                case '>':
                    if (TryMove(board, robotX, robotY, 1, 0))
                        robotX += 1;
                    break;
                case 'v':
                    if (TryMove(board, robotX, robotY, 0, 1))
                        robotY += 1;
                    break;
                case '<':
                    if (TryMove(board, robotX, robotY, -1, 0))
                        robotX -= 1;
                    break;
            }
        }

        long answer = 0;
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (board[j, i] == 'O')
                    answer += j + 100 * i;
            }
        }

        return answer;
    }

    private static bool TryMovePart2(char[,] board, int x, int y, int dx, int dy)
    {
        var target = board[x + dx, y + dy];

        if (target == '#')
            return false;

        if (target == '.')
        {
            board[x + dx, y + dy] = board[x, y];
            board[x, y] = '.';
            return true;
        }

        if (target is '[' or ']')
        {
            if (dx != 0)
            {
                if (TryMovePart2(board, x + dx, y, dx, dy))
                {
                    board[x + dx, y + dy] = board[x, y];
                    board[x, y] = '.';
                    return true;
                }

                return false;
            }

            if (dy != 0)
            {
                if (TryMoveBoxVertically(board, x, y + dy, dy))
                {
                    board[x, y + dy] = board[x, y];
                    board[x, y] = '.';
                    return true;
                }

                return false;
            }
        }

        throw new UnreachableException();
    }

    private static bool TryMoveBoxVertically(char[,] board, int x, int y, int dy)
    {
        var dx = board[x, y] switch
        {
            '[' => 1,
            ']' => -1,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (board[x, y + dy] == '#' || board[x + dx, y + dy] == '#')
            return false;

        if (board[x, y + dy] == '.' && board[x + dx, y + dy] == '.')
        {
            board[x, y + dy] = board[x, y];
            board[x + dx, y + dy] = board[x + dx, y];
            board[x, y] = '.';
            board[x + dx, y] = '.';
            return true;
        }

        if (dx > 0 && board[x, y + dy] == '[' && board[x + dx, y + dy] == ']')
        {
            if (CheckMoveBoxVertically(board, x, y + dy, dy))
            {
                TryMoveBoxVertically(board, x, y + dy, dy);
                board[x, y + dy] = board[x, y];
                board[x + dx, y + dy] = board[x + dx, y];
                board[x, y] = '.';
                board[x + dx, y] = '.';
                return true;
            }

            return false;
        }

        if (dx < 0 && board[x, y + dy] == ']' && board[x + dx, y + dy] == '[')
        {
            if (CheckMoveBoxVertically(board, x, y + dy, dy))
            {
                TryMoveBoxVertically(board, x, y + dy, dy);
                board[x, y + dy] = board[x, y];
                board[x + dx, y + dy] = board[x + dx, y];
                board[x, y] = '.';
                board[x + dx, y] = '.';
                return true;
            }

            return false;
        }
        
        if (board[x, y + dy] is '[' or ']' && board[x + dx, y + dy] is '[' or ']')
        {
            if (CheckMoveBoxVertically(board, x, y + dy, dy) && CheckMoveBoxVertically(board, x + dx, y + dy, dy))
            {
                TryMoveBoxVertically(board, x, y + dy, dy);
                TryMoveBoxVertically(board, x + dx, y + dy, dy);
                board[x, y + dy] = board[x, y];
                board[x + dx, y + dy] = board[x + dx, y];
                board[x, y] = '.';
                board[x + dx, y] = '.';
                return true;
            }

            return false;
        }

        if (board[x, y + dy] is '[' or ']' && board[x + dx, y + dy] is '.')
        {
            if (CheckMoveBoxVertically(board, x, y + dy, dy))
            {
                TryMoveBoxVertically(board, x, y + dy, dy);
                board[x, y + dy] = board[x, y];
                board[x + dx, y + dy] = board[x + dx, y];
                board[x, y] = '.';
                board[x + dx, y] = '.';
                return true;
            }

            return false;
        }

        if (board[x, y + dy] is '.' && board[x + dx, y + dy] is '[' or ']')
        {
            if (CheckMoveBoxVertically(board, x + dx, y + dy, dy))
            {
                TryMoveBoxVertically(board, x + dx, y + dy, dy);
                board[x, y + dy] = board[x, y];
                board[x + dx, y + dy] = board[x + dx, y];
                board[x, y] = '.';
                board[x + dx, y] = '.';
                return true;
            }

            return false;
        }

        throw new UnreachableException();
    }

    private static bool CheckMoveBoxVertically(char[,] board, int x, int y, int dy)
    {
        var dx = board[x, y] switch
        {
            '[' => 1,
            ']' => -1,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (board[x, y + dy] == '#' || board[x + dx, y + dy] == '#')
            return false;

        if (board[x, y + dy] == '.' && board[x + dx, y + dy] == '.')
            return true;

        if (board[x, y + dy] is '[' or ']' && board[x + dx, y + dy] is '[' or ']')
            return CheckMoveBoxVertically(board, x, y + dy, dy) && CheckMoveBoxVertically(board, x + dx, y + dy, dy);

        if (board[x, y + dy] is '[' or ']' && board[x + dx, y + dy] is '.')
            return CheckMoveBoxVertically(board, x, y + dy, dy);

        if (board[x, y + dy] is '.' && board[x + dx, y + dy] is '[' or ']')
            return CheckMoveBoxVertically(board, x + dx, y + dy, dy);

        throw new UnreachableException();
    }

    protected override object InternalPart2()
    {
        var width = Input.Lines.First(l => l[0] == '#').Length * 2;
        var height = Input.Lines.Count(l => l.FirstOrDefault() == '#');

        var board = new char[width, height];

        var (robotX, robotY) = (0, 0);

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                switch (Input.Lines[i][j / 2])
                {
                    case '#':
                        board[j, i] = '#';
                        board[++j, i] = '#';
                        break;
                    case 'O':
                        board[j, i] = '[';
                        board[++j, i] = ']';
                        break;
                    case '.':
                        board[j, i] = '.';
                        board[++j, i] = '.';
                        break;
                    case '@':
                        board[j, i] = '@';
                        (robotX, robotY) = (j, i);
                        board[++j, i] = '.';
                        break;
                }
            }
        }

        foreach (var d in Input.Lines[(height + 1)..].SelectMany(c => c))
        {
            switch (d)
            {
                case '^':
                    if (TryMovePart2(board, robotX, robotY, 0, -1))
                        robotY -= 1;
                    break;
                case '>':
                    if (TryMovePart2(board, robotX, robotY, 1, 0))
                        robotX += 1;
                    break;
                case 'v':
                    if (TryMovePart2(board, robotX, robotY, 0, 1))
                        robotY += 1;
                    break;
                case '<':
                    if (TryMovePart2(board, robotX, robotY, -1, 0))
                        robotX -= 1;
                    break;
            }
        }

        long answer = 0;
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (board[j, i] == '[')
                    answer += j + 100 * i;
            }
        }

        return answer;
    }
}