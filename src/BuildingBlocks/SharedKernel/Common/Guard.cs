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

    public static void AgainstNullOrEmpty<T>(T obj, string name) where T : class
    {
        if (obj == null)
        {
            throw new ArgumentNullException(name, $"'{name}' is required.");
        }
    }
}
