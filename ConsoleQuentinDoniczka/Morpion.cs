using System.ComponentModel.DataAnnotations;

namespace ConsoleQuentinDoniczka;

public class Morpion
{
    private char[,] _grid;
    private char _playerX = 'X';
    private char _playerO = 'O';

    public Morpion()
    {
        _grid = new char[3, 3];
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                _grid[row, col] = ' ';
            }
        }
    }

    private void DisplayGrid()
    {
        Display.ShowGrid(_grid);
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
            if (winner != ' ')
            {
                DisplayGrid();
                Console.WriteLine($"Player {winner} wins!");
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
        if (position.Row < 0 || position.Row > 2 || position.Col < 0 || position.Col > 2)
        {
            Console.WriteLine("Invalid position! Position must be between 0 and 2.");
            return false;
        }

        if (_grid[position.Row, position.Col] != ' ')
        {
            Console.WriteLine("Cell already occupied! Choose another position.");
            return false;
        }

        _grid[position.Row, position.Col] = player;
        return true;
    }

    private char SwitchPlayer(char currentPlayer)
    {
        return currentPlayer == _playerO ? _playerX : _playerO;
    }

    private char CheckWinner()
    {
        // Check rows
        for (int row = 0; row < 3; row++)
        {
            if (_grid[row, 0] != ' ' &&
                _grid[row, 0] == _grid[row, 1] &&
                _grid[row, 1] == _grid[row, 2])
            {
                return _grid[row, 0];
            }
        }

        // Check columns
        for (int col = 0; col < 3; col++)
        {
            if (_grid[0, col] != ' ' &&
                _grid[0, col] == _grid[1, col] &&
                _grid[1, col] == _grid[2, col])
            {
                return _grid[0, col];
            }
        }

        // Check diagonal top left to bottom right
        if (_grid[0, 0] != ' ' &&
            _grid[0, 0] == _grid[1, 1] &&
            _grid[1, 1] == _grid[2, 2])
        {
            return _grid[0, 0];
        }

        // Check diagonal top right to bottom left
        if (_grid[0, 2] != ' ' &&
            _grid[0, 2] == _grid[1, 1] &&
            _grid[1, 1] == _grid[2, 0])
        {
            return _grid[0, 2];
        }

        return ' ';
    }

    private Position2D GetPlayerMove(char player)
    {
        Console.WriteLine($"Player {player}, it's your turn!");
        Console.Write("Enter position (row col, e.g., 0 1): ");

        string? input = Console.ReadLine();

        var parts = input?.Split(' ');

        int row = 0;
        int col = 0;

        if (parts?.Length == 2)
        {
            int.TryParse(parts[0], out row);
            int.TryParse(parts[1], out col);
        }

        return new Position2D(row, col);
    }

}