namespace ConsoleQuentinDoniczka;

public class Morpion
{
    private const int GridSize = 3;
    private const char EmptyCell = ' ';
    private const char PlayerX = 'X';
    private const char PlayerO = 'O';

    private readonly IDisplay _display;
    private readonly char[,] _grid;

    public Morpion(IDisplay display)
    {
        _display = display;
        _grid = new char[GridSize, GridSize];
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int row = 0; row < GridSize; row++)
        {
            for (int col = 0; col < GridSize; col++)
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
        if (position.Row < 0 || position.Row >= GridSize || position.Col < 0 || position.Col >= GridSize)
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
        for (int row = 0; row < GridSize; row++)
        {
            for (int col = 0; col < GridSize; col++)
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
        // Check rows
        for (int row = 0; row < GridSize; row++)
        {
            char winner = CheckThreeCells(_grid[row, 0], _grid[row, 1], _grid[row, 2]);
            if (winner != EmptyCell) return winner;
        }

        // Check columns
        for (int col = 0; col < GridSize; col++)
        {
            char winner = CheckThreeCells(_grid[0, col], _grid[1, col], _grid[2, col]);
            if (winner != EmptyCell) return winner;
        }

        // Check diagonals
        char diagonal1 = CheckThreeCells(_grid[0, 0], _grid[1, 1], _grid[2, 2]);
        if (diagonal1 != EmptyCell) return diagonal1;

        char diagonal2 = CheckThreeCells(_grid[0, 2], _grid[1, 1], _grid[2, 0]);
        if (diagonal2 != EmptyCell) return diagonal2;

        return EmptyCell;
    }

    private Position2D GetPlayerMove(char player)
    {
        _display.ShowPlayerTurn(player);
        return _display.GetPlayerMove(player);
    }

}