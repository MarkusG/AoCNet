namespace AoC;

public class Board<T> where T : struct
{
    private readonly T[,] _board;

    public Board(string[] lines, Func<char, T> selector)
    {
        Width = lines.First().Length;
        Height = lines.Length;

        _board = new T[Width, Height];
        for (var i = 0; i < Height; i++)
        for (var j = 0; j < Width; j++)
            _board[j, i] = selector(lines[i][j]);
    }

    public int Width { get; }

    public int Height { get; }

    public T? this[int x, int y]
    {
        get
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                return _board[x, y];
            return null;
        }
        set
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                _board[x, y] = value!.Value;
        }
    }

    public IEnumerable<(int X, int Y, T Value)> Enumerate()
    {
        for (var i = 0; i < Height; i++)
        for (var j = 0; j < Width; j++)
            yield return (j, i, _board[j, i]);
    }

    public void Print()
    {
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
                Console.Write(_board[j, i]);
            Console.WriteLine();
        }
    }
}