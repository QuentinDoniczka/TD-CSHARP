using ConsoleQuentinDoniczka.Input;
using FluentAssertions;

namespace TestProject1;

public class TestUserInput
{
    [Fact]
    public void ReadMove_WithValidInput_ShouldReturnCorrectMove()
    {
        var simulatedInput = new StringReader("2 1");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var move = userInput.ReadMove();

        move.Col.Should().Be(1);
        move.Row.Should().Be(2);
    }

    [Fact]
    public void ReadMove_WithValidInputZeroCoordinates_ShouldReturnZeroMove()
    {
        var simulatedInput = new StringReader("0 0");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var move = userInput.ReadMove();

        move.Col.Should().Be(0);
        move.Row.Should().Be(0);
    }

    [Fact]
    public void ReadMove_WithSingleNumber_ShouldReturnInvalidMove()
    {
        var simulatedInput = new StringReader("5");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var move = userInput.ReadMove();

        move.Col.Should().Be(-1);
        move.Row.Should().Be(-1);
    }

    [Fact]
    public void ReadMove_WithNonNumericInput_ShouldReturnInvalidMove()
    {
        var simulatedInput = new StringReader("abc def");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var move = userInput.ReadMove();

        move.Col.Should().Be(-1);
        move.Row.Should().Be(-1);
    }

    [Fact]
    public void ReadMove_WithPartiallyNumericInput_ShouldReturnInvalidMove()
    {
        var simulatedInput = new StringReader("1 abc");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var move = userInput.ReadMove();

        move.Col.Should().Be(-1);
        move.Row.Should().Be(-1);
    }

    [Fact]
    public void ReadMove_WithEmptyInput_ShouldReturnInvalidMove()
    {
        var simulatedInput = new StringReader("");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var move = userInput.ReadMove();

        move.Col.Should().Be(-1);
        move.Row.Should().Be(-1);
    }

    [Fact]
    public void ReadMove_WithTooManyNumbers_ShouldReturnInvalidMove()
    {
        var simulatedInput = new StringReader("1 2 3");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var move = userInput.ReadMove();

        move.Col.Should().Be(-1);
        move.Row.Should().Be(-1);
    }

    [Fact]
    public void ReadMove_WithNegativeNumbers_ShouldReturnNegativeCoordinates()
    {
        var simulatedInput = new StringReader("-1 -2");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var move = userInput.ReadMove();

        move.Col.Should().Be(-2);
        move.Row.Should().Be(-1);
    }

    [Fact]
    public void ReadMove_WithExtraSpaces_ShouldReturnInvalidMove()
    {
        var simulatedInput = new StringReader("  1   2  ");
        Console.SetIn(simulatedInput);
        var userInput = new UserInput();

        var move = userInput.ReadMove();

        move.Col.Should().Be(-1);
        move.Row.Should().Be(-1);
    }
}
