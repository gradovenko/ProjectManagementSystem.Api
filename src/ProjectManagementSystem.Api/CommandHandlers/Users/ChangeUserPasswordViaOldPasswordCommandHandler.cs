using MediatR;
using ProjectManagementSystem.Domain.Users;
using ProjectManagementSystem.Domain.Users.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Users;

public sealed class ChangeUserPasswordViaOldPasswordCommandHandler : IRequestHandler<ChangeUserPasswordViaOldPasswordCommand, ChangeUserPasswordViaOldPasswordCommandResultState>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenStore _refreshTokenStore;

    public ChangeUserPasswordViaOldPasswordCommandHandler(IUserRepository userRepository,
        IPasswordHasher passwordHasher, IRefreshTokenStore refreshTokenStore)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _refreshTokenStore = refreshTokenStore ?? throw new ArgumentNullException(nameof(refreshTokenStore));
    }

    public async Task<ChangeUserPasswordViaOldPasswordCommandResultState> Handle(ChangeUserPasswordViaOldPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.Get(request.UserId, cancellationToken);

        if (user == null)
            return ChangeUserPasswordViaOldPasswordCommandResultState.UserNotFound;

        if (!_passwordHasher.VerifyHashedPassword(user.PasswordHash, request.OldPassword))
            return ChangeUserPasswordViaOldPasswordCommandResultState.UserPasswordWrong;

        user.ChangePasswordHash(_passwordHasher.HashPassword(request.NewPassword));

        await _refreshTokenStore.ExpireAllTokens(user.Id, cancellationToken);

        return ChangeUserPasswordViaOldPasswordCommandResultState.UserPasswordChanged;
    }
}