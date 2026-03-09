using ConsoleQuentinDoniczka.Core;

namespace ConsoleQuentinDoniczka.Services;

public record GameStats(int TotalGames, int HumanWins, int BotWins, int Draws);

public interface IGameHistoryService
{
    Task<GameStats> GetStatsAsync();
    Task RecordGameAsync(GameResult result);
}
