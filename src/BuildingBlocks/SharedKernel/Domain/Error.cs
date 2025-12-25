namespace SharedKernel.Domain;

public sealed record Error(string Code, string Description)
{
   
    public static readonly Error None = new(string.Empty, string.Empty);

   
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

   
    public const string NotFoundCode = "Error.NotFound";
    public const string ValidationCode = "Error.Validation";
    public const string ConflictCode = "Error.Conflict"; 


    public static Error NotFound(string resourceName, object resourceId) =>
        new(NotFoundCode, $"{resourceName} with id '{resourceId}' was not found.");

    public static Error Validation(string description) =>
        new(ValidationCode, description);

    public static Error Conflict(string description) =>
        new(ConflictCode, description);

    public override string ToString() => Code;
}