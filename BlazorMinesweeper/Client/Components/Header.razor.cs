using BlazorMinesweeper.Client.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorMinesweeper.Client.Components
{
    public partial class Header
    {
        [Parameter]
        public GameBoard board { get; set; }

        [Parameter]
        public int MaxWidth { get; set; }

        public static int GetPlace(int value, int place)
        {
            if (value == 0)
                return 0;

            return ((value % (place * 10)) - (value % place)) / place;
        }
    }
}
