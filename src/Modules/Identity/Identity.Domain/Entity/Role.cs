using SharedKernel.Common;
using SharedKernel.Domain;
using SharedKernel.Entities;
using SharedKernel.Exceptions;

namespace Identity.Domain.Entity;

public class Role : AggregateRoot
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

    public Result UpdatePermissions(List<string> newCodes)
    {
        var currentPermissionsCode = _permissions.Select(p => p.PermissionCode).ToHashSet();
        //A , B , C , D

        var newCodesSet = newCodes.ToHashSet();
                                             // A , B  E         A , B , C , D
        var newCodesToAdd = newCodesSet.Except(currentPermissionsCode);

        foreach ( var code in newCodesToAdd )
        {
            _permissions.Add(new RolePermission(code));
        }

        var codeToRemove =
                                _permissions
                                 .Where(p => !newCodesSet.Contains(p.PermissionCode))
                                  .ToList();

        foreach (var perm in codeToRemove)
        {
            _permissions.Remove(perm);
        }

        return Result.Success();    
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
