namespace ConsoleQuentinDoniczka;

public class Morpion
{
    private const char EmptyCell = ' ';
    private const char PlayerX = 'X';
    private const char PlayerO = 'O';

    private readonly IDisplay _display;
    private readonly char[,] _grid;

    public Morpion(IDisplay display)
    {
        _display = display;
        _grid = new char[GameConstants.GridSize, GameConstants.GridSize];
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int row = 0; row < GameConstants.GridSize; row++)
        {
            for (int col = 0; col < GameConstants.GridSize; col++)
            {
                _grid[row, col] = EmptyCell;
            }
        }
    }

    private void DisplayGrid()
    {
        _display.ShowGrid(_grid);
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
            if (winner != EmptyCell)
            {
                EndGame(() => _display.ShowWinner(winner));
                isGameRunning = false;
            }
            else if (IsGridFull())
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
        if (position.Row < 0 || position.Row >= GameConstants.GridSize || position.Col < 0 || position.Col >= GameConstants.GridSize)
        {
            _display.ShowInvalidPosition();
            return false;
        }

        if (_grid[position.Row, position.Col] != EmptyCell)
        {
            _display.ShowCellOccupied();
            return false;
        }

        _grid[position.Row, position.Col] = player;
        return true;
    }

    private char GetNextPlayer(char currentPlayer)
    {
        return currentPlayer == PlayerO ? PlayerX : PlayerO;
    }

    private bool IsGridFull()
    {
        for (int row = 0; row < GameConstants.GridSize; row++)
        {
            for (int col = 0; col < GameConstants.GridSize; col++)
            {
                if (_grid[row, col] == EmptyCell)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private char CheckThreeCells(char cell1, char cell2, char cell3)
    {
        if (cell1 != EmptyCell && cell1 == cell2 && cell2 == cell3)
        {
            return cell1;
        }
        return EmptyCell;
    }

    private char CheckWinner()
    {
        for (int row = 0; row < GameConstants.GridSize; row++)
        {
            char rowWin = CheckThreeCells(_grid[row, 0], _grid[row, 1], _grid[row, 2]);
            if (rowWin != EmptyCell) return rowWin;
        }

        for (int col = 0; col < GameConstants.GridSize; col++)
        {
            char columnWin = CheckThreeCells(_grid[0, col], _grid[1, col], _grid[2, col]);
            if (columnWin != EmptyCell) return columnWin;
        }

        char diagonalWin = CheckThreeCells(_grid[0, 0], _grid[1, 1], _grid[2, 2]);
        if (diagonalWin != EmptyCell) return diagonalWin;

        char antiDiagonalWin = CheckThreeCells(_grid[0, 2], _grid[1, 1], _grid[2, 0]);
        if (antiDiagonalWin != EmptyCell) return antiDiagonalWin;

        return EmptyCell;
    }

    private Position2D GetPlayerMove(char player)
    {
        _display.ShowPlayerTurn(player);
        return _display.GetPlayerMove(player);
    }

}