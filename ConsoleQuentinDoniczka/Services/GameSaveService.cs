using ConsoleQuentinDoniczka.Data;
using ConsoleQuentinDoniczka.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleQuentinDoniczka.Services;

public class GameSaveService : IGameSaveService
{
    private readonly GameDbContext _dbContext;

    public GameSaveService(GameDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(string gridState, char currentSymbol)
    {
        var existing = await _dbContext.GameSaves.FindAsync(1).ConfigureAwait(false);

        if (existing != null)
        {
            existing.GridState = gridState;
            existing.CurrentSymbol = currentSymbol;
            existing.SavedAt = DateTime.UtcNow;
        }
        else
        {
            _dbContext.GameSaves.Add(new GameSave
            {
                Id = 1,
                GridState = gridState,
                CurrentSymbol = currentSymbol,
                SavedAt = DateTime.UtcNow
            });
        }

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<GameSave?> LoadAsync()
    {
        return await _dbContext.GameSaves.FindAsync(1).ConfigureAwait(false);
    }
}
