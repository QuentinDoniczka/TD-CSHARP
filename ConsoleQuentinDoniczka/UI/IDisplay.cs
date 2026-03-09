using ConsoleQuentinDoniczka.Core;
using ConsoleQuentinDoniczka.Services;

namespace ConsoleQuentinDoniczka;

public interface IDisplay
{
    void ClearScreen();
    void ShowHistory(GameStats stats);
    void ShowGrid(char[,] grid);
    void ShowPlayerTurn(char player);
    UserAction GetPlayerAction(char player);
    void ShowInvalidPosition();
    void ShowCellOccupied();
    void ShowWinner(char winner);
    void ShowDraw();
    void ShowPlayAgainPrompt();
    void ShowGameSaved();
    void ShowGameLoaded();
    void ShowNoSaveFound();
}
