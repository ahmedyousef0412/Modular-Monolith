namespace Identity.Application.Authentication.Dtos;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);