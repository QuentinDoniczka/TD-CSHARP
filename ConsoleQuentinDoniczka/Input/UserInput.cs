using ConsoleQuentinDoniczka.Core;

namespace ConsoleQuentinDoniczka.Input;

public class UserInput : IUserInput
{
    public UserAction ReadAction()
    {
        string? input = Console.ReadLine()?.Trim().ToLower();

        if (input == "save") return UserAction.Save;
        if (input == "quit") return UserAction.Quit;

        var parts = input?.Split(' ');

        if (parts?.Length == 2 &&
            int.TryParse(parts[0], out int row) &&
            int.TryParse(parts[1], out int col))
        {
            return UserAction.FromMove(new Move(col, row));
        }

        return UserAction.Invalid;
    }
}
