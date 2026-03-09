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

    public Task<UserAction> GetAction()
    {
        _display.ShowPlayerTurn(Symbol);
        return Task.FromResult(_display.GetPlayerAction(Symbol));
    }
}
