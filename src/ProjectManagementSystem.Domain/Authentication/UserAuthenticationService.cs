using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Authentication
{
    public sealed class UserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly IJwtAccessTokenFactory _jwtAccessTokenFactory;
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthenticationService(IUserRepository userRepository, IRefreshTokenStore refreshTokenStore,
            IJwtAccessTokenFactory jwtAccessTokenFactory, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _refreshTokenStore = refreshTokenStore;
            _jwtAccessTokenFactory = jwtAccessTokenFactory;
            _passwordHasher = passwordHasher;
        }

        public async Task<Token> AuthenticationByPassword(string email, string password,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(email, cancellationToken);

            if (user == null)
                throw new InvalidCredentialsException();

            if (!_passwordHasher.VerifyHashedPassword(user.PasswordHash, password))
                throw new InvalidCredentialsException();

            var refreshToken = await _refreshTokenStore.Create(user.Id);

            var accessToken = await _jwtAccessTokenFactory.Create(user, cancellationToken);

            return new Token(accessToken.Value, accessToken.ExpiresIn, refreshToken);
        }

        public async Task<Token> AuthenticationByRefreshToken(Guid refreshToken, CancellationToken cancellationToken)
        {
            var newRefreshToken = await _refreshTokenStore.Reissue(refreshToken);

            if (newRefreshToken == null)
                throw new InvalidCredentialsException();

            var user = await _userRepository.Get(newRefreshToken.UserId, cancellationToken);

            var accessToken = await _jwtAccessTokenFactory.Create(user, cancellationToken);

            return new Token(accessToken.Value, accessToken.ExpiresIn, refreshToken);
        }
    }
}