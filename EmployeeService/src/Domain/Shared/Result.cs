namespace EmployeeService.Domain.Shared;

public class Result
{
    public Error? Error { get; protected set; }
    public bool IsSuccess { get; protected set; }
    public static Result Success()
    {
        return new Result
        {
            IsSuccess = true
        };
    }

    public static Result Failure(Error error)
    {
        return new Result
        {
            IsSuccess = false,
            Error = error
        };
    }
}
public class Result<TResponse> : Result
{
    public TResponse? Value { get; private set; }
    public Result()
    {

    }
    public Result(TResponse value)
    {
        Value = value;
        IsSuccess = true;
    }
    public static Result<TResponse> Success(TResponse value)
    {
        return new Result<TResponse>
        {
            IsSuccess = true,
            Value = value
        };
    }
    public static new Result<TResponse> Failure(Error error)
    {
        return new Result<TResponse>
        {
            IsSuccess = false,
            Error = error
        };
    }

}
