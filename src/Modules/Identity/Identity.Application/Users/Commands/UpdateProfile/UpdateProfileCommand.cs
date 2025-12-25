using BuildingBlocks.Application.CQRS;

namespace Identity.Application.Users.Commands.UpdateProfile;

public record UpdateProfileCommand(
    string FirstName,
    string LastName
) : ICommand;
