namespace Blazor.Minesweeper.Models.Tests;

public class CoordinateTests
{
    [Fact]
    public void GivenXValueIsZero_WhenConstructingCoordinate_ThenThrowException()
    {
        var x = 0;
        var y = 1;

        Assert.Throws<ArgumentException>(() => new Coordinate(x,y));
    }

    [Fact]
    public void GivenYValueIsZero_WhenConstructingCoordinate_ThenThrowException()
    {
        var x = 1;
        var y = 0;

        Assert.Throws<ArgumentException>(() => new Coordinate(x, y));
    }
}
