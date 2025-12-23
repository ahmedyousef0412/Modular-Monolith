namespace SharedKernel.Domain;

public sealed record Error(string Code, string Description)
{

    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error NotFound(string name, object key) =>
      new("NotFound", $"{name} ({key}) was not found");

    public static Error Validation(string message) =>
        new("Validation", message);
}
