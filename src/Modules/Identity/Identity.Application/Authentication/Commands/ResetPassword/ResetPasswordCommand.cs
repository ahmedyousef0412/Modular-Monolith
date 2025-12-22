using SharedKernel.CQRS;

namespace Identity.Application.Authentication.Commands.ResetPassword;

public record ResetPasswordCommand(
    string Email,
    string Token,
    string NewPassword,
    string ConfirmNewPassword
) :ICommand<CommandResult>;

