using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Application.Dtos;
using Identity.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain;

namespace Identity.Application.Roles.Queries.GetRole;

public class GetRoleByIdQueryHandler(IIdentityDbContext context) : IQueryHandler<GetRoleByIdQuery, RoleResponse>
{
    public async Task<Result<RoleResponse>> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
    {
        var role = await context
            .Roles
            .AsNoTracking()
            .Where(r => r.Id == query.RoleId)
            .Select(r => new RoleResponse
                (

                    r.Id,
                    r.Name,
                    r.Description,
                    r.Permissions.Select(p => p.PermissionCode).ToList()
                )
            )
            .FirstOrDefaultAsync(cancellationToken);

        if (role is null)
            return Result<RoleResponse>.Failure(Error.NotFound(nameof(Role), query.RoleId));


        return Result.Success(role);

    }
}
