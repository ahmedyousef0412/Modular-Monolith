using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Pagination;
using Identity.Application.Dtos;

namespace Identity.Application.Roles.Queries.GetRoles;

public record GetAllRolesQuery(string? SearchTerm) : PagedRequest, IQuery<PagedList<RoleSummary>>;

