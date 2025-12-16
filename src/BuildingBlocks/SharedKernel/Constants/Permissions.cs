namespace SharedKernel.Constants;


/// <summary>
/// Define my permissions as constants so I can use them in attributes across modules.
/// </summary>
public static class Permissions
{
    public static class Inventory
    {
        public const string Create = "Permissions.Inventory.Create";
        public const string Edit = "Permissions.Inventory.Edit";
        public const string Delete = "Permissions.Inventory.Delete";
    }

    public static class Sales
    {
        public const string Create = "Permissions.Sales.Create";
        public const string Edit = "Permissions.Sales.Edit";
        public const string Delete = "Permissions.Sales.Delete";
    }

    public static class Warehouse
    {
        public const string Create = "Permissions.Warehouse.Create";
        public const string Edit = "Permissions.Warehouse.Edit";
        public const string Delete = "Permissions.Warehouse.Delete";
    }
}
