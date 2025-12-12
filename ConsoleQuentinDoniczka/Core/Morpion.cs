using ConsoleQuentinDoniczka.Core;
using ConsoleQuentinDoniczka.Players;

namespace ConsoleQuentinDoniczka;

public class Morpion
{
    public const int GridSize = 3;

    private const char PlayerX = 'X';
    private const char PlayerO = 'O';

    private readonly IDisplay _display;
    private readonly Grid _grid;
    private readonly IPlayer _playerX;
    private readonly IPlayer _playerO;

    public Morpion(IDisplay display)
    {
        _display = display;
        _grid = new Grid(GridSize);
        _playerX = new HumanPlayer(display, PlayerX);
        _playerO = new AiPlayer(display, _grid, GridSize, PlayerO);
    }

    public Morpion(IDisplay display, IPlayer playerX, IPlayer playerO)
    {
        _display = display;
        _grid = new Grid(GridSize);
        _playerX = playerX;
        _playerO = playerO;
    }

    private void DisplayGrid()
    {
        _display.ShowGrid(_grid.GetCells());
    }
    

    public void Start()
    {
        bool isGameRunning = true;
        IPlayer currentPlayer = _playerX;
        while (isGameRunning)
        {
            DisplayGrid();
            Move position = currentPlayer.GetMove();
            bool moveSuccess = PlaceMove(position, currentPlayer.Symbol);

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
                currentPlayer = GetNextPlayer(currentPlayer);
            }
        }
    }

    private void EndGame(Action displayResult)
    {
        DisplayGrid();
        displayResult();
    }

    private bool PlaceMove(Move position, char player)
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

    private IPlayer GetNextPlayer(IPlayer currentPlayer)
    {
        return currentPlayer == _playerX ? _playerO : _playerX;
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


}