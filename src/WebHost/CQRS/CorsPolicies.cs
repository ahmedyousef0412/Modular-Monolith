namespace WebHost.CQRS;

public static class CorsPolicies
{
    public const string Spa = "SpaPolicy";


    public static class Sections
    {
        public const string Cors = "CORS";
        public const string SpaOrigins = "CORS:SpaOrigins";
    }
}
