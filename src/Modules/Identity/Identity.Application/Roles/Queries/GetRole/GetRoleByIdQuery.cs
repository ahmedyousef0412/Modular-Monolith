using BuildingBlocks.Application.CQRS;
using Identity.Application.Dtos;

namespace Identity.Application.Roles.Queries.GetRole;

public record GetRoleByIdQuery(Guid RoleId) : IQuery<RoleResponse>;

