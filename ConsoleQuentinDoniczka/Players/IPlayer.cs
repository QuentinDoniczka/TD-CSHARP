using ConsoleQuentinDoniczka.Core;

namespace ConsoleQuentinDoniczka.Players;

public interface IPlayer
{
    char Symbol { get; }
    Task<Move> GetMove();
}
