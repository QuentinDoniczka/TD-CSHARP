namespace ConsoleQuentinDoniczka;

public class Utils
{
    public record Result(string? Error, bool IsSuccess)
    {
        public static Result Success()
            => new Result(null, true);

        public static Result Failure(string error)
            => new Result(error, false);

        public bool IsFailure => !IsSuccess;
    }
}