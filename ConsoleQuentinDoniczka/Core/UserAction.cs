namespace ConsoleQuentinDoniczka.Core;

public enum UserActionType { PlayMove, Save, Quit, Invalid }

public record UserAction(UserActionType Type, Move? Move = null)
{
    public static UserAction FromMove(Move move) => new(UserActionType.PlayMove, move);
    public static readonly UserAction Save = new(UserActionType.Save);
    public static readonly UserAction Quit = new(UserActionType.Quit);
    public static readonly UserAction Invalid = new(UserActionType.Invalid);
}
