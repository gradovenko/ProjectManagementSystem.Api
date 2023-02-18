namespace ProjectManagementSystem.Domain.User.Accounts;

public sealed class UserUpdateService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserUpdateService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task UpdateName(Guid id, string name, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(id, cancellationToken);
            
        var existingUser = await _userRepository.GetByName(name, cancellationToken);

        if (existingUser != null)
            throw new NameAlreadyExistsException();
            
        if (!_passwordHasher.VerifyHashedPassword(user.PasswordHash, password))
            throw new InvalidPasswordException();

        user.UpdateName(name);

        await _userRepository.Save(user);
    }
        
    public async Task UpdateEmail(Guid id, string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(id, cancellationToken);
            
        var existingUser = await _userRepository.GetByEmail(email, cancellationToken);

        if (existingUser != null)
            throw new EmailAlreadyExistsException();

        if (!_passwordHasher.VerifyHashedPassword(user.PasswordHash, password))
            throw new InvalidPasswordException();

        user.UpdateEmail(email);

        await _userRepository.Save(user);
    }
}