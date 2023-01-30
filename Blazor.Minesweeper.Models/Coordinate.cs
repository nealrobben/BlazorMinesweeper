namespace Blazor.Minesweeper.Models;

public struct Coordinate
{
    public int X;
    public int Y;

    public Coordinate(int x, int y)
    {
        if (x == 0 || y == 0)
            throw new ArgumentException("x/y cannot be zero");

        X = x;
        Y = y;
    }
}
