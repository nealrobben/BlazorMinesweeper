using Blazor.Minesweeper.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorMinesweeper.Client.Components
{
    public partial class MineField
    {
        [Parameter]
        public GameBoard board { get; set; }

        [Parameter]
        public int MaxWidth { get; set; }

        [Parameter]
        public int MaxHeight { get; set; }
    }
}
