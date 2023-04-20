using MediatR;
using ProjectManagementSystem.Domain.Users;
using ProjectManagementSystem.Domain.Users.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Users;

public class UpdateUserAsAdminCommandHandler : IRequestHandler<UpdateUserAsAdminCommand, UpdateUserAsAdminCommandResultState>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserAsAdminCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<UpdateUserAsAdminCommandResultState> Handle(UpdateUserAsAdminCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.Get(request.UserId, cancellationToken);

        if (user == null)
            return UpdateUserAsAdminCommandResultState.UserNotFound;

        if (request.Name != user.Name)
        {
            User? projectWithSameName = await _userRepository.GetByName(request.Name, cancellationToken);

            if (projectWithSameName != null)
                return UpdateUserAsAdminCommandResultState.UserWithSameNameAlreadyExists;
        }

        // if (request.Email != user.Email)
        // {
        //     User? projectWithSamePath = await _userRepository.GetByEmail(request.Email, cancellationToken);
        //
        //     if (projectWithSamePath != null)
        //         return UpdateUserAsAdminCommandResultState.UserWithSameEmailAlreadyExists;
        // }

        user.Update(request.Name);

        await _userRepository.Save(user, cancellationToken);

        return UpdateUserAsAdminCommandResultState.UserUpdated;
    }
}