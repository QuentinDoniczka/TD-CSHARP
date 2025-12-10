using System.ComponentModel.DataAnnotations;

namespace ConsoleQuentinDoniczka;

public class Morpion
{
    public const int GridSize = 3;

    private const char PlayerX = 'X';
    private const char PlayerO = 'O';

    private readonly IDisplay _display;
    private readonly Grid _grid;

    public Morpion(IDisplay display)
    {
        _display = display;
        _grid = new Grid(GridSize);
    }

    private void DisplayGrid()
    {
        _display.ShowGrid(_grid.GetCells());
    }
    

    public void Start()
    {
        bool isGameRunning = true;
        char playerTurn = PlayerX;
        while (isGameRunning)
        {
            DisplayGrid();
            Position2D position = GetPlayerMove(playerTurn);
            bool moveSuccess = PlaceMove(position, playerTurn);

            if (!moveSuccess)
            {
                continue;
            }

            char winner = CheckWinner();
            if (winner != Grid.EmptyCell)
            {
                EndGame(() => _display.ShowWinner(winner));
                isGameRunning = false;
            }
            else if (_grid.IsFull())
            {
                EndGame(() => _display.ShowDraw());
                isGameRunning = false;
            }
            else
            {
                playerTurn = GetNextPlayer(playerTurn);
            }
        }
    }

    private void EndGame(Action displayResult)
    {
        DisplayGrid();
        displayResult();
    }

    private bool PlaceMove(Position2D position, char player)
    {
        var result = _grid.TryPlaceSymbol(position, player);

        if (result.IsFailure)
        {
            if (result.Error == "InvalidPosition")
            {
                _display.ShowInvalidPosition();
            }
            else if (result.Error == "CellOccupied")
            {
                _display.ShowCellOccupied();
            }
        }

        return result.IsSuccess;
    }

    private char GetNextPlayer(char currentPlayer)
    {
        return currentPlayer == PlayerO ? PlayerX : PlayerO;
    }

    private char CheckThreeCells(char cell1, char cell2, char cell3)
    {
        if (cell1 != Grid.EmptyCell && cell1 == cell2 && cell2 == cell3)
        {
            return cell1;
        }
        return Grid.EmptyCell;
    }
    

    private char CheckWinner()
    {
        char winner = GetWinnerFromRows();
        if (winner != Grid.EmptyCell) return winner;

        winner = GetWinnerFromColumns();
        if (winner != Grid.EmptyCell) return winner;

        return GetWinnerFromDiagonals();
    }

    private char GetWinnerFromRows()
    {
        for (int row = 0; row < GridSize; row++)
        {
            char winner = CheckThreeCells(_grid[row, 0], _grid[row, 1], _grid[row, 2]);
            if (winner != Grid.EmptyCell) return winner;
        }
        return Grid.EmptyCell;
    }

    private char GetWinnerFromColumns()
    {
        for (int col = 0; col < GridSize; col++)
        {
            char winner = CheckThreeCells(_grid[0, col], _grid[1, col], _grid[2, col]);
            if (winner != Grid.EmptyCell) return winner;
        }
        return Grid.EmptyCell;
    }

    private char GetWinnerFromDiagonals()
    {
        char winner = CheckThreeCells(_grid[0, 0], _grid[1, 1], _grid[2, 2]);
        if (winner != Grid.EmptyCell) return winner;

        return CheckThreeCells(_grid[0, 2], _grid[1, 1], _grid[2, 0]);
    }

    private Position2D GetPlayerMove(char player)
    {
        _display.ShowPlayerTurn(player);
        return _display.GetPlayerMove(player);
    }

}