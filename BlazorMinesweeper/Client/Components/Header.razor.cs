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

        [Parameter]
        public int NumberOfMinesRemaining { get; set; }
        
        [Parameter]
        public GameStatus GameStatus { get; set; }

        [Parameter]
        public int SecondsElapsed { get; set; }

        [Parameter]
        public EventCallback FaceIconClicked { get; set; }

        public static int GetPlace(int value, int place)
        {
            if (value == 0)
                return 0;

            return ((value % (place * 10)) - (value % place)) / place;
        }
    }
}
