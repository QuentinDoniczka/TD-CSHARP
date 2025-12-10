namespace ConsoleQuentinDoniczka;

public class Morpion
{
    private const int GridSize = 3;
    private const char EmptyCell = ' ';

    private readonly IDisplay _display;
    private readonly char[,] _grid;
    private readonly char _playerX = 'X';
    private readonly char _playerO = 'O';

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
        char playerTurn = _playerX;
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
                DisplayGrid();
                _display.ShowWinner(winner);
                isGameRunning = false;
            }
            else if (IsGridFull())
            {
                DisplayGrid();
                _display.ShowDraw();
                isGameRunning = false;
            }
            else
            {
                playerTurn = SwitchPlayer(playerTurn);
            }
        }
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

    private char SwitchPlayer(char currentPlayer)
    {
        return currentPlayer == _playerO ? _playerX : _playerO;
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

    private char CheckWinner()
    {
        // Check rows
        for (int row = 0; row < GridSize; row++)
        {
            if (_grid[row, 0] != EmptyCell &&
                _grid[row, 0] == _grid[row, 1] &&
                _grid[row, 1] == _grid[row, 2])
            {
                return _grid[row, 0];
            }
        }

        // Check columns
        for (int col = 0; col < GridSize; col++)
        {
            if (_grid[0, col] != EmptyCell &&
                _grid[0, col] == _grid[1, col] &&
                _grid[1, col] == _grid[2, col])
            {
                return _grid[0, col];
            }
        }

        // Check diagonal top left to bottom right
        if (_grid[0, 0] != EmptyCell &&
            _grid[0, 0] == _grid[1, 1] &&
            _grid[1, 1] == _grid[2, 2])
        {
            return _grid[0, 0];
        }

        // Check diagonal top right to bottom left
        if (_grid[0, 2] != EmptyCell &&
            _grid[0, 2] == _grid[1, 1] &&
            _grid[1, 1] == _grid[2, 0])
        {
            return _grid[0, 2];
        }

        return EmptyCell;
    }

    private Position2D GetPlayerMove(char player)
    {
        _display.ShowPlayerTurn(player);
        return _display.GetPlayerMove(player);
    }

}