using MediatR;
using ProjectManagementSystem.Domain;
using ProjectManagementSystem.Domain.Authentication;
using ProjectManagementSystem.Domain.Authentication.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers;

public sealed class AuthenticateUserByPasswordCommandHandler : IRequestHandler<AuthenticateUserByPasswordCommand, CommandResult<Token, AuthenticateUserByPasswordCommandResultState>>
{
    private readonly IUserGetter _userGetter;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly IAccessTokenCreator _accessTokenCreator;

    public AuthenticateUserByPasswordCommandHandler(IUserGetter userGetter, IPasswordHasher passwordHasher,
        IRefreshTokenStore refreshTokenStore, IAccessTokenCreator accessTokenCreator)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _refreshTokenStore = refreshTokenStore ?? throw new ArgumentNullException(nameof(refreshTokenStore));
        _accessTokenCreator = accessTokenCreator ?? throw new ArgumentNullException(nameof(accessTokenCreator));
    }

    public async Task<CommandResult<Token, AuthenticateUserByPasswordCommandResultState>> Handle(AuthenticateUserByPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.GetByLogin(request.Login, cancellationToken);

        if (user == null)
            return new CommandResult<Token, AuthenticateUserByPasswordCommandResultState>
            {
                Value = null,
                State = AuthenticateUserByPasswordCommandResultState.UserNotFound
            };

        if (!_passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password))
            return new CommandResult<Token, AuthenticateUserByPasswordCommandResultState>
            {
                Value = null,
                State = AuthenticateUserByPasswordCommandResultState.PasswordNotValid
            };

        string refreshToken = await _refreshTokenStore.Add(user.Id, cancellationToken);
        
        AccessToken accessToken = await _accessTokenCreator.Create(user, cancellationToken);

        return new CommandResult<Token, AuthenticateUserByPasswordCommandResultState>
        {
            Value = new Token(accessToken.Value, accessToken.ExpiresIn, refreshToken),
            State = AuthenticateUserByPasswordCommandResultState.Authenticated
        };
    }
}