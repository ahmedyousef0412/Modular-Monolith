using SharedKernel.CQRS;


namespace Sales.Application.Commands;

public record MarkOrderAsPaidCommand(Guid OrderId) : ICommand<bool>;

