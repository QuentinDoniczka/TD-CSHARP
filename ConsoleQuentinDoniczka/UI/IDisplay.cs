using ConsoleQuentinDoniczka.Core;

namespace ConsoleQuentinDoniczka;

public interface IDisplay
{
    void ShowGrid(char[,] grid);
    void ShowPlayerTurn(char player);
    Move GetPlayerMove(char player);
    void ShowInvalidPosition();
    void ShowCellOccupied();
    void ShowWinner(char winner);
    void ShowDraw();
}
