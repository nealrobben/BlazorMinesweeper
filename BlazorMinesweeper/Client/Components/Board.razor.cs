using BlazorMinesweeper.Client.Models;

namespace BlazorMinesweeper.Client.Components
{
    public partial class Board
    {
        private GameBoard board = new GameBoard();

        public int MaxWidth
        {
            get
            {
                return board.Width + 1;
            }
        }

        public int MaxHeight
        {
            get
            {
                return board.Height + 1;
            }
        }

        protected override void OnInitialized()
        {
            board.Initialize(16, 16, 40);
        }
    }
}
