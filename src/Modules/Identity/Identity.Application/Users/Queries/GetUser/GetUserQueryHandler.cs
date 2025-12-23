using Identity.Application.Abstractions;
using Identity.Application.Authentication.Dtos;
using Identity.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.CQRS;
using SharedKernel.Domain;

namespace Identity.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(IIdentityDbContext dbContext) : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var userDto = await dbContext
            .Users
            .AsNoTracking()
            .Where(u => u.Id == query.Id)
            .Select(u => new UserResponse(u.Id, u.FirstName, u.LastName, u.Email.Value))
            .FirstOrDefaultAsync(cancellationToken);

        if (userDto is null)
            return Result<UserResponse>.Failure(Error.NotFound(nameof(User), query.Id));

        return Result<UserResponse>.Success(userDto);
    }
}
