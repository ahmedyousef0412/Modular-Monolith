using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Pagination;
using Identity.Application.Abstractions;
using Identity.Application.Authentication.Dtos;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain;

namespace Identity.Application.Users.Queries.GetUsers;

public class GetAllUsersQueryHandler(IIdentityDbContext dbContext) : IQueryHandler<GetAllUsersQuery, PagedList<UserResponse>>
{
    public async Task<Result<PagedList<UserResponse>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var usersQuery = dbContext.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            usersQuery = usersQuery.Where(u =>
               u.FirstName.Contains(query.SearchTerm)
            || u.LastName.Contains(query.SearchTerm)
            || u.Email.Value.Contains(query.SearchTerm));
        }

        usersQuery = usersQuery.OrderBy(u =>u.FirstName);


        var projection = usersQuery
                                            .Select
                                            (
                                            u =>  new UserResponse
                                                (
                                                  u.Id,
                                                  u.FirstName,
                                                  u.LastName, u.Email.Value
                                                )
                                            );

        var pagedList = await PagedList<UserResponse>
            .CreateAsync
            (
                projection,
                query.Page,
                query.PageSize,
                cancellationToken
            );

        return Result.Success(pagedList); 

    }
}
