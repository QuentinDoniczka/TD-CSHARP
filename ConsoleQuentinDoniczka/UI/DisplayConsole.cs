using ConsoleQuentinDoniczka.Core;
using ConsoleQuentinDoniczka.Input;

namespace ConsoleQuentinDoniczka.UI;

public class DisplayConsole : IDisplay
{
    private readonly IUserInput _userInput;

    public DisplayConsole(IUserInput userInput)
    {
        _userInput = userInput;
    }
    public void ShowGrid(char[,] grid)
    {
        int gridSize = grid.GetLength(0);

        Console.Clear();
        Console.WriteLine("      0        1        2");
        for (int row = 0; row < gridSize; row++)
        {
            Console.WriteLine("  +-------+-------+-------+");
            Console.Write(row + " |");
            for (int col = 0; col < gridSize; col++)
            {
                Console.Write($"   {grid[row, col]}   |");
            }
            Console.WriteLine();
            Console.Write("  |");
            for (int col = 0; col < gridSize; col++)
            {
                Console.Write("       |");
            }
            Console.WriteLine();
        }
        Console.WriteLine("  +-------+-------+-------+");
        Console.WriteLine();
    }

    public void ShowPlayerTurn(char player)
    {
        Console.WriteLine($"Player {player}, it's your turn!");
    }

    public Move GetPlayerMove(char player)
    {
        Console.Write("Enter position (row col, like: 0 1): ");
        return _userInput.ReadMove();
    }

    public void ShowInvalidPosition()
    {
        Console.WriteLine("Invalid position! Please try again.");
    }

    public void ShowCellOccupied()
    {
        Console.WriteLine("Cell already occupied! Choose another position.");
    }

    public void ShowWinner(char winner)
    {
        Console.WriteLine($"Player {winner} wins!");
    }

    public void ShowDraw()
    {
        Console.WriteLine("It's a draw! No one wins.");
    }
}
