using System.Diagnostics;

namespace Blazor.Minesweeper.Models;

public class GameBoard
{
    public int Width { get; set; } = 16;
    public int Height { get; set; } = 16;
    public int MineCount { get; set; } = 40;
    public GameStatus Status { get; set; }
    public Stopwatch Stopwatch { get; set; } = new Stopwatch();
    public List<Panel> Panels { get; set; }

    public int NumberOfMinesRemaining
    {
        get
        {
            return MineCount - Panels.Where(x => x.IsFlagged).Count();
        }
    }

    public void Initialize(int width, int height, int mines)
    {
        Width = width;
        Height = height;
        MineCount = mines;
        Panels = new List<Panel>();

        int id = 1;

        for (int i = 1; i <= height; i++)
        {
            for (int j = 1; j <= width; j++)
            {
                Panels.Add(new Panel(id, new Coordinate(j,i)));
                id++;
            }
        }

        Status = GameStatus.AwaitingFirstMove;
    }

    public void Reset()
    {
        Initialize(Width, Height, MineCount);
        Stopwatch = Stopwatch.StartNew();
    }

    public List<Panel> GetNeighbors(Coordinate location)
    {
        var nearbyPanels = Panels.Where(panel => panel.Location.X >= location.X - 1
        && panel.Location.X <= location.X + 1
        && panel.Location.Y >= location.Y - 1
        && panel.Location.Y <= location.Y + 1);

        var currentPanel = Panels.Where(panel => panel.Location.X == location.X && panel.Location.Y == location.Y);

        return nearbyPanels.Except(currentPanel).ToList();
    }

    public void FirstMove(Coordinate location)
    {
        Random rand = new Random();

        //For any board, take the user's first revealed panel 
        // and any neighbors of that panel, and mark them 
        // as unavailable for mine placement.
        var neighbors = GetNeighbors(location); //Get all neighbors

        //Add the clicked panel to the "unavailable for mines" group.
        neighbors.Add(Panels.First(z => z.Location.X == location.X && z.Location.Y == location.Y));

        //Select all panels from set which are available for mine placement.
        //Order them randomly.
        var mineList = Panels.Except(neighbors)
                             .OrderBy(user => rand.Next());

        //Select the first Z random panels.
        var mineSlots = mineList.Take(MineCount)
                                .ToList()
                                .Select(z => new { z.Location.X, z.Location.Y });

        //Place the mines in the randomly selected panels.
        foreach (var mineCoord in mineSlots)
        {
            Panels.Single(panel => panel.Location.X == mineCoord.X
                                   && panel.Location.Y == mineCoord.Y)
                  .IsMine = true;
        }

        //For every panel which is not a mine, 
        // including the unavailable ones from earlier,
        // determine and save the adjacent mines.
        foreach (var openPanel in Panels.Where(panel => !panel.IsMine))
        {
            var nearbyPanels = GetNeighbors(openPanel.Location);
            openPanel.NumberOfAdjacentMines = nearbyPanels.Count(z => z.IsMine);
        }

        //Mark the game as started.
        Status = GameStatus.InProgress;
        Stopwatch.Start();
    }

    public void MakeMove(Coordinate location)
    {
        if (Status == GameStatus.AwaitingFirstMove)
        {
            FirstMove(location);
        }

        RevealPanel(location);
    }

    public void RevealPanel(Coordinate location)
    {
        //Step 1: Find and reveal the clicked panel
        var selectedPanel = Panels.First(panel => panel.Location.X == location.X
                                                  && panel.Location.Y == location.Y);
        selectedPanel.Reveal();

        //Step 2: If the panel is a mine, show all mines. Game over!
        if (selectedPanel.IsMine)
        {
            Status = GameStatus.Failed;
            RevealAllMines();
            return;
        }

        //Step 3: If the panel is a zero, cascade reveal neighbors.
        if (selectedPanel.NumberOfAdjacentMines == 0)
        {
            RevealZeros(location);
        }

        //Step 4: If this move caused the game to be complete, mark it as such
        PerformCompletionCheck();
    }

    private void RevealAllMines()
    {
        Panels.Where(x => x.IsMine)
              .ToList()
              .ForEach(x => x.IsRevealed = true);
    }

    public void RevealZeros(Coordinate location)
    {
        //Get all neighbor panels
        var neighborPanels = GetNeighbors(location)
                               .Where(panel => !panel.IsRevealed);

        foreach (var neighbor in neighborPanels)
        {
            //For each neighbor panel, reveal that panel.
            neighbor.IsRevealed = true;

            //If the neighbor is also a 0, reveal all of its neighbors too.
            if (neighbor.NumberOfAdjacentMines == 0)
            {
                RevealZeros(neighbor.Location);
            }
        }
    }

    private void PerformCompletionCheck()
    {
        var hiddenPanels = Panels.Where(x => !x.IsRevealed)
                                 .Select(x => x.ID);

        var minePanels = Panels.Where(x => x.IsMine)
                               .Select(x => x.ID);

        if (!hiddenPanels.Except(minePanels).Any())
        {
            Status = GameStatus.Completed;
            Stopwatch.Stop();
        }
    }

    public void FlagPanel(Coordinate location)
    {
        if (NumberOfMinesRemaining > 0)
        {
            var panel = Panels.Where(z => z.Location.X == location.X && z.Location.Y == location.Y).First();

            panel.Flag();
        }
    }
}
