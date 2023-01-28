using BlazorMinesweeper.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorMinesweeper.Client.Pages;

public partial class Index
{
    public IJSRuntime _jsRuntime { get; set; } = default!;
    public NavigationManager _navManager { get; set; } = default!;

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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        while (board.Status == GameStatus.InProgress && _navManager.Uri.Contains("minesweeper"))
        {
            await Task.Delay(500);
            var elapsedTime = (int)board.Stopwatch.Elapsed.TotalSeconds;
            var hundreds = GetPlace(elapsedTime, 100);
            var tens = GetPlace(elapsedTime, 10);
            var ones = GetPlace(elapsedTime, 1);

            await _jsRuntime.InvokeAsync<string>("setTime", hundreds, tens, ones);
        }

        //await _jsRuntime.InvokeVoidAsync("Prism.highlightAll");
    }

    public static int GetPlace(int value, int place)
    {
        if (value == 0) 
            return 0;

        return ((value % (place * 10)) - (value % place)) / place;
    }
}
