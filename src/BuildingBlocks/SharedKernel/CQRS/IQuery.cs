using MediatR;

namespace SharedKernel.CQRS;


// Generic query interface with a return type. Ex(X,Y) => X is the query, Y is the return type may be Y (IEnumerable<Dto>, Dto, int, string, etc)
public interface IQuery<TQueryResult> : IRequest<TQueryResult> { }

