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
            PlaceMove(position, playerTurn);
            playerTurn = SwitchPlayer(playerTurn);
        }
    }

    private void PlaceMove(Position2D position, char player)
    {
        _grid[position.Row, position.Col] = player;
    }

    private char SwitchPlayer(char currentPlayer)
    {
        return currentPlayer == _playerO ? _playerX : _playerO;
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