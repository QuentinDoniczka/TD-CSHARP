using ConsoleQuentinDoniczka;
using ConsoleQuentinDoniczka.Core;
using ConsoleQuentinDoniczka.Players;
using FluentAssertions;

namespace TestProject1;

public class TestMorpion
{
    [Fact]
    public void Start_WhenPlayerXWinsHorizontally_ShouldShowWinner()
    {
        var fakeDisplay = new FakeDisplay();
        var fakePlayerX = new FakePlayer('X', new[]
        {
            new Move(0, 0),
            new Move(1, 0),
            new Move(2, 0)
        });
        var fakePlayerO = new FakePlayer('O', new[]
        {
            new Move(0, 1),
            new Move(1, 1)
        });
        var morpion = new Morpion(fakeDisplay, fakePlayerX, fakePlayerO);

        morpion.Start();

        fakeDisplay.WinnerShown.Should().Be('X');
        fakeDisplay.ShowWinnerCallCount.Should().Be(1);
        fakeDisplay.ShowDrawCallCount.Should().Be(0);
    }

    [Fact]
    public void Start_WhenGameIsDraw_ShouldShowDraw()
    {
        var fakeDisplay = new FakeDisplay();
        var fakePlayerX = new FakePlayer('X', new[]
        {
            new Move(0, 0),
            new Move(1, 1),
            new Move(2, 0),
            new Move(0, 1),
            new Move(1, 2)
        });
        var fakePlayerO = new FakePlayer('O', new[]
        {
            new Move(0, 2),
            new Move(1, 0),
            new Move(2, 1),
            new Move(2, 2)
        });
        var morpion = new Morpion(fakeDisplay, fakePlayerX, fakePlayerO);

        morpion.Start();

        fakeDisplay.ShowDrawCallCount.Should().Be(1);
        fakeDisplay.ShowWinnerCallCount.Should().Be(0);
    }
}

public class FakeDisplay : IDisplay
{
    public int ShowGridCallCount { get; private set; }
    public int ShowPlayerTurnCallCount { get; private set; }
    public int ShowInvalidPositionCallCount { get; private set; }
    public int ShowCellOccupiedCallCount { get; private set; }
    public int ShowWinnerCallCount { get; private set; }
    public int ShowDrawCallCount { get; private set; }
    public char? WinnerShown { get; private set; }
    public char? LastPlayerTurnShown { get; private set; }

    public void ShowGrid(char[,] grid)
    {
        ShowGridCallCount++;
    }

    public void ShowPlayerTurn(char player)
    {
        ShowPlayerTurnCallCount++;
        LastPlayerTurnShown = player;
    }

    public Move GetPlayerMove(char player)
    {
        throw new NotImplementedException();
    }

    public void ShowInvalidPosition()
    {
        ShowInvalidPositionCallCount++;
    }

    public void ShowCellOccupied()
    {
        ShowCellOccupiedCallCount++;
    }

    public void ShowWinner(char winner)
    {
        ShowWinnerCallCount++;
        WinnerShown = winner;
    }

    public void ShowDraw()
    {
        ShowDrawCallCount++;
    }
}

public class FakePlayer : IPlayer
{
    private readonly Queue<Move> _moves;

    public char Symbol { get; }

    public FakePlayer(char symbol, Move[] moves)
    {
        Symbol = symbol;
        _moves = new Queue<Move>(moves);
    }

    public Move GetMove()
    {
        if (_moves.Count > 0)
        {
            return _moves.Dequeue();
        }

        return new Move(-1, -1);
    }
}
