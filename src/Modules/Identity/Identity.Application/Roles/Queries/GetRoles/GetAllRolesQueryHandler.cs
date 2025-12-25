using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Pagination;
using Identity.Application.Abstractions;
using Identity.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain;

namespace Identity.Application.Roles.Queries.GetRoles;

public class GetAllRolesQueryHandler(IIdentityDbContext context) : IQueryHandler<GetAllRolesQuery, PagedList<RoleSummary>>
{
    public async Task<Result<PagedList<RoleSummary>>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
    {
        var rolesQuery = context.Roles.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            rolesQuery = rolesQuery.Where(r => r.Name.Contains(query.SearchTerm));
        }


        var roles = rolesQuery.Select(r => new RoleSummary(r.Id, r.Name, r.Description));

        var pagedList = await PagedList<RoleSummary>.CreateAsync(roles,query.Page, query.PageSize, cancellationToken);

        return Result.Success(pagedList);
    }
}