namespace SharedKernel.CQRS;

public sealed class CommandResult
{
    public bool IsSucess { get; private set; }

    public bool IsFailure => !IsSucess;

    public IEnumerable<string> Errors { get; private set; }

    public object Data { get; private set; }

    private CommandResult()
    {
    }

    public static CommandResult Success(object data = null)
    {
        return new CommandResult
        {
            IsSucess = true,
            Data = data
        };
    }

    public static CommandResult Failure(IEnumerable<string> errors)
    {
        return new CommandResult
        {
            IsSucess = false,
            Errors = errors
        };
    }
}
