using System.Reflection;

namespace SharedKernel.Constants;


/// <summary>
/// Define my permissions as constants so I can use them in attributes across modules.
/// </summary>
public static class PermissionsHelper
{

    //NestedType
    public static class Inventory
    {
        //Fields
        public const string Create = "Permissions.Inventory.Create"; //FieldInfo
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

    public static class Users
    {
        public const string View = "Permissions.Users.View";
        public const string Create = "Permissions.Users.Create";
        public const string Edit = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";
    }



    public static List<string> GetAllPermissions()
    {
        //Static fields don’t need an instance, so we pass null

        return typeof(PermissionsHelper)
            .GetNestedTypes()
            .SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            .Select(f => f.GetValue(null).ToString())
            .ToList();
    }
}
