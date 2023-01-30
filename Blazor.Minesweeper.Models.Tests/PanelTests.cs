using FluentAssertions;

namespace Blazor.Minesweeper.Models.Tests;

public class PanelTests
{
    [Fact]
    public void GivenUnrevealedPanel_WhenRevealFunctionCalled_ThenPropertyIsRevealedBecomesTrue()
    {
        var panel = new Panel(1,new Coordinate(2,3));
        panel.IsRevealed = false;

        panel.Reveal();

        panel.IsRevealed.Should().BeTrue();
    }

    [Fact]
    public void GivenFlaggedPanel_WhenRevealFunctionCalled_ThenPropertyIsFlaggedBecomesFalse()
    {
        var panel = new Panel(1, new Coordinate(2, 3));
        panel.IsFlagged = true;

        panel.Reveal();

        panel.IsFlagged.Should().BeFalse();
    }

    [Fact]
    public void GivenUnrevealedPanel_WhenFlagFunctionCalled_ThenPropertyIsFlaggedIsToggled()
    {
        var panel = new Panel(1, new Coordinate(2, 3));
        panel.IsRevealed = false;
        panel.IsFlagged = false;

        panel.Flag();

        panel.IsFlagged.Should().BeTrue();

        panel.Flag();

        panel.IsFlagged.Should().BeFalse();
    }

    [Fact]
    public void GivenrevealedPanel_WhenFlagFunctionCalled_ThenPropertyIsFlaggedDoesNotChange()
    {
        var panel = new Panel(1, new Coordinate(2, 3));
        panel.IsRevealed = true;
        panel.IsFlagged = false;

        panel.Flag();

        panel.IsFlagged.Should().BeFalse();

        panel.IsFlagged = true;
        panel.Flag();

        panel.IsFlagged.Should().BeTrue();
    }
}
