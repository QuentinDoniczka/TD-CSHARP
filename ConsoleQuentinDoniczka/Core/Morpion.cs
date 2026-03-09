using ConsoleQuentinDoniczka.Core;
using ConsoleQuentinDoniczka.Players;
using ConsoleQuentinDoniczka.Services;

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
    private readonly GameStats? _stats;
    private readonly IGameSaveService? _saveService;

    public Morpion(IDisplay display, GameStats? stats = null, IGameSaveService? saveService = null)
    {
        _display = display;
        _grid = new Grid(GridSize);
        _playerX = new HumanPlayer(display, PlayerX);
        _playerO = new AiPlayer(display, _grid, GridSize, PlayerO);
        _stats = stats;
        _saveService = saveService;
    }

    public Morpion(IDisplay display, IPlayer playerX, IPlayer playerO, GameStats? stats = null)
    {
        _display = display;
        _grid = new Grid(GridSize);
        _playerX = playerX;
        _playerO = playerO;
        _stats = stats;
    }

    private void DisplayGrid()
    {
        _display.ClearScreen();
        if (_stats != null)
        {
            _display.ShowHistory(_stats);
        }
        _display.ShowGrid(_grid.GetCells());
    }

    public async Task<GameResult> Start()
    {
        IPlayer currentPlayer = await TryLoadGame() ?? _playerX;

        while (true)
        {
            DisplayGrid();
            UserAction action = await currentPlayer.GetAction();

            switch (action.Type)
            {
                case UserActionType.Save:
                    await HandleSave(currentPlayer.Symbol);
                    continue;

                case UserActionType.Quit:
                    await HandleSave(currentPlayer.Symbol);
                    return GameResult.Quit;

                case UserActionType.Invalid:
                    _display.ShowInvalidPosition();
                    continue;

                case UserActionType.PlayMove:
                    break;
            }

            bool moveSuccess = PlaceMove(action.Move!, currentPlayer.Symbol);

            if (!moveSuccess)
            {
                continue;
            }

            char winner = CheckWinner();
            if (winner != Grid.EmptyCell)
            {
                DisplayGrid();
                _display.ShowWinner(winner);
                return winner == PlayerX ? GameResult.HumanWin : GameResult.BotWin;
            }

            if (_grid.IsFull())
            {
                DisplayGrid();
                _display.ShowDraw();
                return GameResult.Draw;
            }

            currentPlayer = GetNextPlayer(currentPlayer);
        }
    }

    private async Task<IPlayer?> TryLoadGame()
    {
        if (_saveService == null) return null;

        var save = await _saveService.LoadAsync();
        if (save == null) return null;

        _grid.LoadState(save.GridState);
        _display.ShowGameLoaded();
        return save.CurrentSymbol == PlayerX ? _playerX : _playerO;
    }

    private async Task HandleSave(char currentSymbol)
    {
        if (_saveService == null) return;
        await _saveService.SaveAsync(_grid.GetState(), currentSymbol);
        _display.ShowGameSaved();
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
