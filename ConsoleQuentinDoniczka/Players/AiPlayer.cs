using ConsoleQuentinDoniczka.Core;

namespace ConsoleQuentinDoniczka.Players;

public class AiPlayer : IPlayer
{
    private readonly Random _random;
    private readonly Grid _grid;
    private readonly IDisplay _display;

    public char Symbol { get; }

    public AiPlayer(IDisplay display, Grid grid, int gridSize, char symbol)
    {
        _display = display;
        _grid = grid;
        Symbol = symbol;
        _random = new Random();
    }

    public Move GetMove()
    {
        _display.ShowPlayerTurn(Symbol);
        Task.Delay(1000).Wait();
        var emptyCells = _grid.GetEmptyCells();
        int randomIndex = _random.Next(emptyCells.Count);
        return emptyCells[randomIndex];
    }
}
