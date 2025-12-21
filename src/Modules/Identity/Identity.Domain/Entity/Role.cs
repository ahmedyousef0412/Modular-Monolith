using SharedKernel.Common;
using SharedKernel.Entities;
using SharedKernel.Exceptions;

namespace Identity.Domain.Entity;

public class Role : BaseEntity
{

    public string Name { get; private set; }
    public string Description { get; private set; }


    private readonly List<RolePermission> _permissions = [];
    public IReadOnlyCollection<RolePermission> Permissions => _permissions.AsReadOnly();

    private readonly List<UserRole> _userRoles = [];
    private Role(string name, string description = "")
    {
        

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }


    public static Role Create(string name, string description = "")
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));
        return new Role(name, description);
    }
    public void Update(string name, string description)
    {

        Guard.AgainstNullOrEmpty(name, nameof(name));

        Name = name;
        Description = description;
      
    }


    public void AddPermission(string permissionCode)
    {
        Guard.AgainstNullOrEmpty(permissionCode, nameof(permissionCode));

        if (_permissions.Any(p => p.PermissionCode.Equals(permissionCode, StringComparison.OrdinalIgnoreCase)))
        {
            throw new DomainException($"Permission '{permissionCode}' already exists in role '{Name}'.");
        }

        var permission = new RolePermission(permissionCode);
        _permissions.Add(permission);
    }


    public void RemovePermission(string permissionCode)
    {
        var perm = _permissions.FirstOrDefault(p => p.PermissionCode == permissionCode);
        if (perm != null)
        {
            _permissions.Remove(perm); 
        }
    }
}
