using ConsoleQuentinDoniczka.Data.Entities;

namespace ConsoleQuentinDoniczka.Services;

public interface IGameSaveService
{
    Task SaveAsync(string gridState, char currentSymbol);
    Task<GameSave?> LoadAsync();
}
