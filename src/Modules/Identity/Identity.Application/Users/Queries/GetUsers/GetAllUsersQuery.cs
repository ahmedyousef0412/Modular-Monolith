using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Pagination;
using Identity.Application.Authentication.Dtos;

namespace Identity.Application.Users.Queries.GetUsers;

public record GetAllUsersQuery(string? SearchTerm) : PagedRequest, IQuery<PagedList<UserResponse>>;

//PageRequest contain (PageNumber ,PageSize)