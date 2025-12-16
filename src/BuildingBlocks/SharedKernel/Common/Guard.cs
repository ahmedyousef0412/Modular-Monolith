namespace SharedKernel.Common;

public static class Guard
{

    public static void AgainstNullOrEmpty(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"'{name}' is required.", name);
        }
    }
}
