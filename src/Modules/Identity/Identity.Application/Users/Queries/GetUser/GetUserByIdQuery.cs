using BuildingBlocks.Application.CQRS;
using Identity.Application.Authentication.Dtos;

namespace Identity.Application.Users.Queries.GetUser;

public record GetUserByIdQuery(Guid Id)
    : IQuery<UserResponse>; 

