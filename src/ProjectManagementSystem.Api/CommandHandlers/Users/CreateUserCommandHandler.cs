using MediatR;
using ProjectManagementSystem.Domain.Users;
using ProjectManagementSystem.Domain.Users.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Users;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResultState>
{
    private readonly UserCreator _userCreator;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(UserCreator userCreator, IUserRepository userRepository)
    {
        _userCreator = userCreator ?? throw new ArgumentNullException(nameof(userCreator));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<CreateUserCommandResultState> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.Get(request.UserId, cancellationToken);

        if (user != null)
        {
            if (user.Id == request.UserId)
                return CreateUserCommandResultState.UserCreated;
            return CreateUserCommandResultState.UserWithSameIdButOtherParamsAlreadyExists;
        }

        user = await _userRepository.GetByName(request.Name, cancellationToken);

        if (user != null)
        {
            if (user.Email == request.Name)
                return CreateUserCommandResultState.UserCreated;
            return CreateUserCommandResultState.UserWithSameNameAlreadyExists;
        }
        
        user = await _userRepository.GetByEmail(request.Email, cancellationToken);

        if (user != null)
        {
            if (user.Email == request.Email)
                return CreateUserCommandResultState.UserCreated;
            return CreateUserCommandResultState.UserWithSameEmailAlreadyExists;
        }

        user = _userCreator.CreateUser(request.UserId, request.Name, request.Email, request.Password, request.Role);

        await _userRepository.Save(user, cancellationToken);

        return CreateUserCommandResultState.UserCreated;
    }
}