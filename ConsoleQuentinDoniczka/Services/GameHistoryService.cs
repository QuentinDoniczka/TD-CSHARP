using ConsoleQuentinDoniczka.Core;
using ConsoleQuentinDoniczka.Data;
using ConsoleQuentinDoniczka.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleQuentinDoniczka.Services;

public class GameHistoryService : IGameHistoryService
{
    private readonly GameDbContext _dbContext;

    public GameHistoryService(GameDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GameStats> GetStatsAsync()
    {
        var counts = await _dbContext.GameRecords
            .GroupBy(r => r.Result)
            .Select(g => new { Result = g.Key, Count = g.Count() })
            .ToListAsync()
            .ConfigureAwait(false);

        var humanWins = counts.FirstOrDefault(c => c.Result == GameResult.HumanWin)?.Count ?? 0;
        var botWins = counts.FirstOrDefault(c => c.Result == GameResult.BotWin)?.Count ?? 0;
        var draws = counts.FirstOrDefault(c => c.Result == GameResult.Draw)?.Count ?? 0;

        return new GameStats(humanWins + botWins + draws, humanWins, botWins, draws);
    }

    public async Task RecordGameAsync(GameResult result)
    {
        _dbContext.GameRecords.Add(new GameRecord { Result = result });
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}
