using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Persistance;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        //1. validate the user exists
        if (_userRepository.GetByEmail(email) is not User user)
        {
            throw new Exception("User does not exist");
        }
        //2. validate the password is correct
        if (user.Password != password)
        {
            throw new Exception("Password is incorrect");
        }
        //3. generate the JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

    public AuthenticationResult Register(
        string firstName,
        string lastName,
        string email,
        string password)
    {
        //check if user exists
        if (_userRepository.GetByEmail(email) != null)
        {
            throw new Exception("User already exists");
        }
        //create user if not exists
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }
}
