using MediatR;
using ProjectManagementSystem.Domain.Users;
using ProjectManagementSystem.Domain.Users.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Users;

public sealed class DeleteUserAsAdminCommandHandler : IRequestHandler<DeleteUserAsAdminCommand, DeleteUserAsAdminCommandResultState>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserAsAdminCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<DeleteUserAsAdminCommandResultState> Handle(DeleteUserAsAdminCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.Get(request.UserId, cancellationToken);

        if (user == null)
            return DeleteUserAsAdminCommandResultState.UserNotFound;

        user.Delete();

        await _userRepository.Save(user, cancellationToken);

        return DeleteUserAsAdminCommandResultState.UserDeleted;
    }
}