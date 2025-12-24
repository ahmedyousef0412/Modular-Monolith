using BuildingBlocks.Application.CQRS;

namespace Identity.Application.Users.Commands.ChangePassword;

public record ChangePasswordCommand(

    string CurrentPassword,
    string NewPassword,
    string ConfirmNewPassword
) : ICommand<CommandResult>;

