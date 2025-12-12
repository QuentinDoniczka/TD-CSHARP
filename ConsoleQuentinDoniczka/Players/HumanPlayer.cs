using ConsoleQuentinDoniczka.Core;

namespace ConsoleQuentinDoniczka.Players;

public class HumanPlayer : IPlayer
{
    private readonly IDisplay _display;

    public char Symbol { get; }

    public HumanPlayer(IDisplay display, char symbol)
    {
        _display = display;
        Symbol = symbol;
    }

    public Move GetMove()
    {
        _display.ShowPlayerTurn(Symbol);
        return _display.GetPlayerMove(Symbol);
    }
}
