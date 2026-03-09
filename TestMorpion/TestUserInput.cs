using ConsoleQuentinDoniczka.Core;
using ConsoleQuentinDoniczka.Input;
using FluentAssertions;

namespace TestProject1;

public class TestUserInput
{
    [Fact]
    public void ReadAction_WithValidInput_ShouldReturnPlayMove()
    {
        var simulatedInput = new StringReader("2 1");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.PlayMove);
        action.Move!.Col.Should().Be(1);
        action.Move!.Row.Should().Be(2);
    }

    [Fact]
    public void ReadAction_WithValidInputZeroCoordinates_ShouldReturnZeroMove()
    {
        var simulatedInput = new StringReader("0 0");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.PlayMove);
        action.Move!.Col.Should().Be(0);
        action.Move!.Row.Should().Be(0);
    }

    [Fact]
    public void ReadAction_WithSave_ShouldReturnSaveAction()
    {
        var simulatedInput = new StringReader("save");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.Save);
        action.Move.Should().BeNull();
    }

    [Fact]
    public void ReadAction_WithQuit_ShouldReturnQuitAction()
    {
        var simulatedInput = new StringReader("quit");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.Quit);
        action.Move.Should().BeNull();
    }

    [Fact]
    public void ReadAction_WithSingleNumber_ShouldReturnInvalid()
    {
        var simulatedInput = new StringReader("5");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.Invalid);
    }

    [Fact]
    public void ReadAction_WithNonNumericInput_ShouldReturnInvalid()
    {
        var simulatedInput = new StringReader("abc def");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.Invalid);
    }

    [Fact]
    public void ReadAction_WithPartiallyNumericInput_ShouldReturnInvalid()
    {
        var simulatedInput = new StringReader("1 abc");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.Invalid);
    }

    [Fact]
    public void ReadAction_WithEmptyInput_ShouldReturnInvalid()
    {
        var simulatedInput = new StringReader("");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.Invalid);
    }

    [Fact]
    public void ReadAction_WithTooManyNumbers_ShouldReturnInvalid()
    {
        var simulatedInput = new StringReader("1 2 3");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.Invalid);
    }

    [Fact]
    public void ReadAction_WithNegativeNumbers_ShouldReturnNegativeCoordinates()
    {
        var simulatedInput = new StringReader("-1 -2");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.PlayMove);
        action.Move!.Col.Should().Be(-2);
        action.Move!.Row.Should().Be(-1);
    }

    [Fact]
    public void ReadAction_WithExtraSpaces_ShouldReturnInvalid()
    {
        var simulatedInput = new StringReader("  1   2  ");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var action = userInput.ReadAction();

        action.Type.Should().Be(UserActionType.Invalid);
    }
}
