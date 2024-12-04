using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AoC._2024;

public partial class Day04 : AdventBase
{
    protected override object InternalPart1()
    {
        var lines = Input.Lines;
        var width = lines.First().Length;
        var height = lines.Length;

        var occurrences = 0;

        var board = new char[width, height];
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
                board[i, j] = lines[j][i];
        }

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (board[i, j] == 'X')
                {
                    // horizontal
                    try
                    {
                        if (board[i + 1, j] == 'M' && board[i + 2, j] == 'A' && board[i + 3, j] == 'S')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }

                    // horizontal backward
                    try
                    {
                        if (board[i - 1, j] == 'M' && board[i - 2, j] == 'A' && board[i - 3, j] == 'S')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }

                    // vertical
                    try
                    {
                        if (board[i, j + 1] == 'M' && board[i, j + 2] == 'A' && board[i, j + 3] == 'S')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }

                    // vertical backward
                    try
                    {
                        if (board[i, j - 1] == 'M' && board[i, j - 2] == 'A' && board[i, j - 3] == 'S')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }

                    // diagonal sloping down
                    try
                    {
                        if (board[i + 1, j + 1] == 'M' && board[i + 2, j + 2] == 'A' && board[i + 3, j + 3] == 'S')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }

                    // diagonal sloping down backward
                    try
                    {
                        if (board[i - 1, j - 1] == 'M' && board[i - 2, j - 2] == 'A' && board[i - 3, j - 3] == 'S')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }

                    // diagonal sloping up
                    try
                    {
                        if (board[i - 1, j + 1] == 'M' && board[i - 2, j + 2] == 'A' && board[i - 3, j + 3] == 'S')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }

                    // diagonal sloping up backward
                    try
                    {
                        if (board[i + 1, j - 1] == 'M' && board[i + 2, j - 2] == 'A' && board[i + 3, j - 3] == 'S')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                }
            }
        }

        return occurrences;
    }

    protected override object InternalPart2()
    {
        var lines = Input.Lines;
        var width = lines.First().Length;
        var height = lines.Length;

        var occurrences = 0;

        var board = new char[width, height];
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
                board[i, j] = lines[j][i];
        }

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (board[i, j] == 'A')
                {
                    // M.S
                    // .A.
                    // M.S
                    try
                    {
                        if (board[i - 1, j - 1] == 'M' && board[i - 1, j + 1] == 'M' && board[i + 1, j + 1] == 'S' && board[i + 1, j - 1] == 'S')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                    
                    // S.M
                    // .A.
                    // S.M
                    try
                    {
                        if (board[i - 1, j - 1] == 'S' && board[i - 1, j + 1] == 'S' && board[i + 1, j + 1] == 'M' && board[i + 1, j - 1] == 'M')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                    
                    // M.M
                    // .A.
                    // S.S
                    try
                    {
                        if (board[i - 1, j - 1] == 'M' && board[i - 1, j + 1] == 'S' && board[i + 1, j + 1] == 'S' && board[i + 1, j - 1] == 'M')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                    
                    // S.S
                    // .A.
                    // M.M
                    try
                    {
                        if (board[i - 1, j - 1] == 'S' && board[i - 1, j + 1] == 'M' && board[i + 1, j + 1] == 'M' && board[i + 1, j - 1] == 'S')
                            occurrences++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                }
            }
        }

        return occurrences;
    }
}