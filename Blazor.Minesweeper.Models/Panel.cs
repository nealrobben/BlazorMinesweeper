using System.Diagnostics;

namespace Blazor.Minesweeper.Models;

[DebuggerDisplay("{X},{Y}")]
public class Panel
{
    public int ID { get; set; }

    public Coordinate Location { get; private set; }

    public bool IsMine { get; set; }

    public int NumberOfAdjacentMines { get; set; }

    public bool IsRevealed { get; set; }

    public bool IsFlagged { get; set; }

    public Panel(int id, int x, int y)
    {
        ID = id;
        Location = new Coordinate(x, y);
    }

    public void Flag()
    {
        if (!IsRevealed)
        {
            IsFlagged = !IsFlagged;
        }
    }

    public void Reveal()
    {
        IsRevealed = true;
        IsFlagged = false;
    }
}
