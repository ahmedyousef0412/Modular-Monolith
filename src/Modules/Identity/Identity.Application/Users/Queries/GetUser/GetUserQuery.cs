using Identity.Application.Authentication.Dtos;
using SharedKernel.CQRS;

namespace Identity.Application.Users.Queries.GetUser;

public record GetUserQuery(Guid Id)
    : IQuery<UserResponse>; 

