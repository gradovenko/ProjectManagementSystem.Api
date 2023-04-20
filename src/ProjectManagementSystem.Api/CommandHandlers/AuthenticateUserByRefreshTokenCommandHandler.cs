using MediatR;
using ProjectManagementSystem.Domain;
using ProjectManagementSystem.Domain.Authentication;
using ProjectManagementSystem.Domain.Authentication.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers;

public sealed class AuthenticateUserByRefreshTokenCommandHandler : IRequestHandler<AuthenticateUserByRefreshTokenCommand, CommandResult<Token, AuthenticateUserByRefreshTokenCommandResultState>>
{
    private readonly IUserGetter _userGetter;
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly IAccessTokenCreator _accessTokenCreator;

    public AuthenticateUserByRefreshTokenCommandHandler(IUserGetter userGetter,
        IRefreshTokenStore refreshTokenStore, IAccessTokenCreator accessTokenCreator)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _refreshTokenStore = refreshTokenStore ?? throw new ArgumentNullException(nameof(refreshTokenStore));
        _accessTokenCreator = accessTokenCreator ?? throw new ArgumentNullException(nameof(accessTokenCreator));
    }

    public async Task<CommandResult<Token, AuthenticateUserByRefreshTokenCommandResultState>> Handle(AuthenticateUserByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var newRefreshToken = await _refreshTokenStore.Reissue(request.RefreshToken, cancellationToken);

        if (newRefreshToken == null)
            return new CommandResult<Token, AuthenticateUserByRefreshTokenCommandResultState>
            {
                Value = null,
                State = AuthenticateUserByRefreshTokenCommandResultState.OldRefreshTokenNotFound
            };

        User user = await _userGetter.Get(newRefreshToken.Value.UserId, cancellationToken);

        AccessToken accessToken = await _accessTokenCreator.Create(user, cancellationToken);
        
        return new CommandResult<Token, AuthenticateUserByRefreshTokenCommandResultState>
        {
            Value = new Token(accessToken.Value, accessToken.ExpiresIn, newRefreshToken.Value.Value),
            State = AuthenticateUserByRefreshTokenCommandResultState.Authenticated
        };
    }
}