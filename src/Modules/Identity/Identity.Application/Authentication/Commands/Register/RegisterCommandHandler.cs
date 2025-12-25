using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using Identity.Domain.Constants;
using Identity.Domain.Entity;
using Identity.Domain.Exceptions;
using Identity.Domain.ValueObjects;
using SharedKernel.Domain;

namespace Identity.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand,Guid>
{

    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(IUserRepository userRepository,
        IIdentityUnitOfWork identityUnitOfWork,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _identityUnitOfWork = identityUnitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email);
        var existingUser = await _userRepository.IsEmailUniqueAsync(email,cancellationToken);

        if (existingUser)
            throw new UserAlreadyExistsException(command.Email);


        var passwordHash = _passwordHasher.Hash(command.Password);


        var user = User.Create
        (
            command.FirstName,
            command.LastName,
            email,
            passwordHash
        );

        var memberRole = await _roleRepository.GetByNameAsync(RoleConstants.Member, cancellationToken)
           ?? throw new InvalidOperationException("System is not properly seeded. Default role missing.");


        user.AssignRole(memberRole);

        await _userRepository.AddAsync(user,cancellationToken);

        await _identityUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}
