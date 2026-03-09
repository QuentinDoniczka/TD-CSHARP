namespace ConsoleQuentinDoniczka.Core;

public record Move(int Col, int Row)
{
    public static readonly Move Save = new(-2, -2);
    public static readonly Move Load = new(-3, -3);
}
