namespace SharedKernel.Domain;

public class Result
{

    public Result(bool isSuccess ,Error error)
    {
        if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; } = default!;


    public static Result Success() => new (true, Error.None);

    public static Result Failure(Error error) => new (false, error);

}