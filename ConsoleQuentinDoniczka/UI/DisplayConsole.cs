using ConsoleQuentinDoniczka.Core;
using ConsoleQuentinDoniczka.Input;
using ConsoleQuentinDoniczka.Services;

namespace ConsoleQuentinDoniczka.UI;

public class DisplayConsole : IDisplay
{
    private readonly IUserInput _userInput;

    public DisplayConsole(IUserInput userInput)
    {
        _userInput = userInput;
    }

    public void ClearScreen()
    {
        Console.Clear();
    }

    public void ShowHistory(GameStats stats)
    {
        Console.WriteLine("=== Historique ===");
        Console.WriteLine($"Parties: {stats.TotalGames} | Victoires Humain (X): {stats.HumanWins} | Victoires Bot (O): {stats.BotWins} | Nuls: {stats.Draws}");
        Console.WriteLine();
    }

    public void ShowGrid(char[,] grid)
    {
        int gridSize = grid.GetLength(0);

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
        Console.Write("Enter position (row col, like: 0 1) you can also use save or load: ");
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

    public void ShowPlayAgainPrompt()
    {
        Console.Write("Rejouer ? (o/n) : ");
    }

    public void ShowGameSaved()
    {
        Console.WriteLine("Partie sauvegardee !");
    }

    public void ShowGameLoaded()
    {
        Console.WriteLine("Partie chargee !");
    }

    public void ShowNoSaveFound()
    {
        Console.WriteLine("Aucune sauvegarde trouvee.");
    }
}
