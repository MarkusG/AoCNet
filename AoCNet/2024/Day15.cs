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
                        board[++j, i] = '.';
                        break;
                }
                if (Input.Lines[i][j / 2] == '@')
                    (robotX, robotY) = (j, i);
            }
        }

        Print(board, width, height);

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
}