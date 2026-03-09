using ConsoleQuentinDoniczka;
using ConsoleQuentinDoniczka.Core;
using FluentAssertions;

namespace TestProject1;

public class TestGrid
{
    [Fact]
    public void Constructor_ShouldInitializeGridWithEmptyCells()
    {
        var newGrid = new Grid(3);

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                newGrid[row, col].Should().Be(Grid.EmptyCell);
            }
        }
    }

    [Fact]
    public void TryPlaceSymbol_WithValidPosition_ShouldSucceed()
    {
        var emptyGrid = new Grid(3);
        var centerMove = new Move(1, 1);
        var playerSymbol = 'X';

        var placementResult = emptyGrid.TryPlaceSymbol(centerMove, playerSymbol);

        placementResult.IsSuccess.Should().BeTrue();
        emptyGrid[1, 1].Should().Be(playerSymbol);
    }

    [Fact]
    public void TryPlaceSymbol_WithInvalidPosition_ShouldFail()
    {
        var emptyGrid = new Grid(3);
        var outOfBoundsMove = new Move(5, 5);
        var playerSymbol = 'X';

        var placementResult = emptyGrid.TryPlaceSymbol(outOfBoundsMove, playerSymbol);

        placementResult.IsFailure.Should().BeTrue();
        placementResult.Error.Should().Be("InvalidPosition");
    }

    [Fact]
    public void TryPlaceSymbol_OnOccupiedCell_ShouldFail()
    {
        var gridWithOneSymbol = new Grid(3);
        var centerMove = new Move(1, 1);
        gridWithOneSymbol.TryPlaceSymbol(centerMove, 'X');

        var secondPlacementResult = gridWithOneSymbol.TryPlaceSymbol(centerMove, 'O');

        secondPlacementResult.IsFailure.Should().BeTrue();
        secondPlacementResult.Error.Should().Be("CellOccupied");
    }

    [Fact]
    public void IsValidPosition_WithValidPosition_ShouldReturnTrue()
    {
        var emptyGrid = new Grid(3);
        var bottomRightMove = new Move(2, 2);

        var isPositionValid = emptyGrid.IsValidPosition(bottomRightMove);

        isPositionValid.Should().BeTrue();
    }

    [Fact]
    public void IsValidPosition_WithInvalidPosition_ShouldReturnFalse()
    {
        var emptyGrid = new Grid(3);
        var outOfBoundsMove = new Move(-1, 3);

        var isPositionValid = emptyGrid.IsValidPosition(outOfBoundsMove);

        isPositionValid.Should().BeFalse();
    }

    [Fact]
    public void IsEmptyCell_OnEmptyCell_ShouldReturnTrue()
    {
        var emptyGrid = new Grid(3);
        var topLeftMove = new Move(0, 0);

        var isCellEmpty = emptyGrid.IsEmptyCell(topLeftMove);

        isCellEmpty.Should().BeTrue();
    }

    [Fact]
    public void IsEmptyCell_OnOccupiedCell_ShouldReturnFalse()
    {
        var gridWithOneSymbol = new Grid(3);
        var centerMove = new Move(1, 1);
        gridWithOneSymbol.TryPlaceSymbol(centerMove, 'X');

        var isCellEmpty = gridWithOneSymbol.IsEmptyCell(centerMove);

        isCellEmpty.Should().BeFalse();
    }

    [Fact]
    public void IsFull_OnEmptyGrid_ShouldReturnFalse()
    {
        var emptyGrid = new Grid(3);

        var isGridFull = emptyGrid.IsFull();

        isGridFull.Should().BeFalse();
    }

    [Fact]
    public void IsFull_OnFullGrid_ShouldReturnTrue()
    {
        var completelyFilledGrid = new Grid(3);
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                completelyFilledGrid.TryPlaceSymbol(new Move(col, row), row % 2 == 0 ? 'X' : 'O');
            }
        }

        var isGridFull = completelyFilledGrid.IsFull();

        isGridFull.Should().BeTrue();
    }

    [Fact]
    public void GetCells_ShouldReturnGridCells()
    {
        var gridWithOneSymbol = new Grid(3);
        var centerMove = new Move(1, 1);
        gridWithOneSymbol.TryPlaceSymbol(centerMove, 'X');

        var retrievedCells = gridWithOneSymbol.GetCells();

        retrievedCells.Should().NotBeNull();
        retrievedCells[1, 1].Should().Be('X');
    }
}