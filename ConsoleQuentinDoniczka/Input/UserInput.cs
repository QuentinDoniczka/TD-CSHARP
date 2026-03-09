using ConsoleQuentinDoniczka.Core;

namespace ConsoleQuentinDoniczka.Input;

public class UserInput : IUserInput
{
    public Move ReadMove()
    {
        string? input = Console.ReadLine()?.Trim().ToLower();

        if (input == "save") return Move.Save;
        if (input == "load") return Move.Load;

        var parts = input?.Split(' ');

        if (parts?.Length == 2 &&
            int.TryParse(parts[0], out int row) &&
            int.TryParse(parts[1], out int col))
        {
            return new Move(col, row);
        }

        return new Move(-1, -1);
    }
}
