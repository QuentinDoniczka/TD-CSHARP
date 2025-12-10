namespace ConsoleQuentinDoniczka;

public static class Display
{
    public static void ShowGrid(char[,] grid)
    {
        Console.Clear();
        Console.WriteLine("      0        1        2");
        for (int row = 0; row < 3; row++)
        {
            Console.WriteLine("  +-------+-------+-------+");
            Console.Write(row + " |");
            for (int col = 0; col < 3; col++)
            {
                Console.Write($"   {grid[row, col]}   |");
            }
            Console.WriteLine();
            Console.Write("  |");
            for (int col = 0; col < 3; col++)
            {
                Console.Write("       |");
            }
            Console.WriteLine();
        }
        Console.WriteLine("  +-------+-------+-------+");
        Console.WriteLine();
    }
}
