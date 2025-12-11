using ConsoleQuentinDoniczka.Core;

namespace ConsoleQuentinDoniczka.Players;

public class AiPlayer : IPlayer
{
    private readonly Random _random;
    private readonly Grid _grid;
    private readonly int _gridSize;
    private readonly IDisplay _display;

    public char Symbol { get; }

    public AiPlayer(IDisplay display, Grid grid, int gridSize, char symbol)
    {
        _display = display;
        _grid = grid;
        _gridSize = gridSize;
        Symbol = symbol;
        _random = new Random();
    }

    public Move GetMove()
    {
        _display.ShowPlayerTurn(Symbol);
        Thread.Sleep(1000);
        var emptyCells = GetEmptyCells();
        int randomIndex = _random.Next(emptyCells.Count);
        return emptyCells[randomIndex];
    }

    private List<Move> GetEmptyCells()
    {
        var emptyCells = new List<Move>();

        for (int row = 0; row < _gridSize; row++)
        {
            for (int col = 0; col < _gridSize; col++)
            {
                var move = new Move(col, row);
                if (_grid.IsEmptyCell(move))
                {
                    emptyCells.Add(move);
                }
            }
        }

        return emptyCells;
    }
}
