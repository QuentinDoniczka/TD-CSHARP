namespace ConsoleQuentinDoniczka;

public class DisplayConsole : IDisplay
{
    private const int GridSize = 3;

    public void ShowGrid(char[,] grid)
    {
        Console.Clear();
        Console.WriteLine("      0        1        2");
        for (int row = 0; row < GridSize; row++)
        {
            Console.WriteLine("  +-------+-------+-------+");
            Console.Write(row + " |");
            for (int col = 0; col < GridSize; col++)
            {
                Console.Write($"   {grid[row, col]}   |");
            }
            Console.WriteLine();
            Console.Write("  |");
            for (int col = 0; col < GridSize; col++)
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

    public Position2D GetPlayerMove(char player)
    {
        Console.Write("Enter position (row col, like: 0 1): ");

        string? input = Console.ReadLine();
        var parts = input?.Split(' ');

        if (parts?.Length == 2 &&
            int.TryParse(parts[0], out int row) &&
            int.TryParse(parts[1], out int col))
        {
            return new Position2D(row, col);
        }

        return new Position2D(-1, -1);
    }

    public void ShowInvalidPosition()
    {
        Console.WriteLine($"Invalid position! Position must be between 0 and {GridSize - 1}.");
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
