using ConsoleQuentinDoniczka.Core;

namespace ConsoleQuentinDoniczka;

public class Grid
{
    public const char EmptyCell = ' ';

    private const string ErrorInvalidPosition = "InvalidPosition";
    private const string ErrorCellOccupied = "CellOccupied";

    private readonly int _size;
    private readonly char[,] _cells;

    public Grid(int size)
    {
        _size = size;
        _cells = new char[size, size];
        Initialize();
    }
    
    public char this[int row, int col] => _cells[row, col];

    public char[,] GetCells() => _cells;

    private void Initialize()
    {
        for (int row = 0; row < _size; row++)
        {
            for (int col = 0; col < _size; col++)
            {
                _cells[row, col] = EmptyCell;
            }
        }
    }

    public Utils.Result TryPlaceSymbol(Move position, char symbol)
    {
        if (!IsValidPosition(position))
        {
            return Utils.Result.Failure(ErrorInvalidPosition);
        }

        if (!IsEmptyCell(position))
        {
            return Utils.Result.Failure(ErrorCellOccupied);
        }

        _cells[position.Row, position.Col] = symbol;
        return Utils.Result.Success();
    }

    public bool IsValidPosition(Move position)
    {
        return position.Row >= 0 && position.Row < _size &&
               position.Col >= 0 && position.Col < _size;
    }

    public bool IsEmptyCell(Move position)
    {
        return _cells[position.Row, position.Col] == EmptyCell;
    }

    public bool IsFull()
    {
        for (int row = 0; row < _size; row++)
        {
            for (int col = 0; col < _size; col++)
            {
                if (_cells[row, col] == EmptyCell)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
