using MediatR;
using ProjectManagementSystem.Domain.Users;
using ProjectManagementSystem.Domain.Users.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Users;

public sealed class CreateUserAsAdminCommandHandler : IRequestHandler<CreateUserAsAdminCommand, CreateUserAsAdminCommandResultState>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserAsAdminCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public async Task<CreateUserAsAdminCommandResultState> Handle(CreateUserAsAdminCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.Get(request.UserId, cancellationToken);

        if (user != null)
        {
            if (user.Id == request.UserId)
                return CreateUserAsAdminCommandResultState.UserCreated;
            return CreateUserAsAdminCommandResultState.UserWithSameIdButDifferentParamsAlreadyExists;
        }

        user = await _userRepository.GetByName(request.Name, cancellationToken);

        if (user != null)
        {
            if (user.Email == request.Name)
                return CreateUserAsAdminCommandResultState.UserCreated;
            return CreateUserAsAdminCommandResultState.UserWithSameNameAlreadyExists;
        }

        user = await _userRepository.GetByEmail(request.Email, cancellationToken);

        if (user != null)
        {
            if (user.Email == request.Email)
                return CreateUserAsAdminCommandResultState.UserCreated;
            return CreateUserAsAdminCommandResultState.UserWithSameEmailAlreadyExists;
        }

        string passwordHash = _passwordHasher.HashPassword(request.Password);

        await _userRepository.Save(new User(request.UserId, request.Name, request.Email, passwordHash, request.Role), cancellationToken);

        return CreateUserAsAdminCommandResultState.UserCreated;
    }
}