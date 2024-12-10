using AdventOfCodeSupport;

namespace AoC;

public static class InputBlockExtensions
{
    public static Board<char> ToBoard(this InputBlock input)
    {
        return new Board<char>(input.Lines, c => c);
    }
    
    public static Board<T> ToBoard<T>(this InputBlock input, Func<char, T> selector) where T : struct
    {
        return new Board<T>(input.Lines, selector);
    }
}