using Blazor.Minesweeper.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorMinesweeper.Client.Components
{
    public partial class MineField
    {
        [Parameter]
        public GameBoard board { get; set; }

        [Parameter]
        public List<Panel> Panels { get; set; }

        [Parameter]
        public int MaxWidth { get; set; }

        [Parameter]
        public int MaxHeight { get; set; }

        [Parameter]
        public EventCallback<Coordinate> OnLeftClickPanel { get; set; }

        [Parameter]
        public EventCallback<Coordinate> OnRightClickPanel { get; set; }

        private async Task LeftClickPanel(Coordinate location)
        {
            await OnLeftClickPanel.InvokeAsync(location);
        }

        private async Task RightClickPanel(Coordinate location)
        {
            await OnRightClickPanel.InvokeAsync(location);
        }
    }
}
