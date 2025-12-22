using SharedKernel.CQRS;

namespace Identity.Application.Authentication.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : ICommand<CommandResult>;

