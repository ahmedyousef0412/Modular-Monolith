namespace SharedKernel.Domain;

public class Result<TValue> : Result 
{
    private readonly TValue? _value;

    public Result(TValue? value ,bool isSuccess ,Error error):base(isSuccess,error)
    {
        _value = value;
    }

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Failure results cannot have value ");

    public static Result<TValue> Success(TValue value) =>
       new(value, true, Error.None);

    public static new Result<TValue> Failure(Error error) =>
        new(default, false, error);
}