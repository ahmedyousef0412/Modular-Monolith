using SharedKernel.Common;

namespace Identity.Domain.Entity;


//Natural key: RoleId + PermissionCode
public class RolePermission
{

    // It is a child entity / value-like object
    // Identified by RoleId + PermissionCode, no separate Id required

    public Guid RoleId { get; private set; }
    public string PermissionCode { get; private set; }


    internal RolePermission(string permissionCode)
    {
        Guard.AgainstNullOrEmpty(permissionCode, nameof(permissionCode));
        //RoleId = roleId;
        PermissionCode = permissionCode.Trim();
    }
}
