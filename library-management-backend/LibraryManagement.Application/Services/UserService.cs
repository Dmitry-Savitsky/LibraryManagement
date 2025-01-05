using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Helpers;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<AuthResult> RegisterUserAsync(RegisterDto registerDto)
        {
            var existingUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return AuthResult.Failure("User with this email already exists.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                Email = registerDto.Email,
                Password = passwordHash,
                Role = "User",
                Name = registerDto.Name
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var token = JwtHelper.GenerateJwtToken(user, _configuration);

            return AuthResult.Success(token);
        }

        public async Task<AuthResult> AuthenticateUserAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                return AuthResult.Failure("Invalid email or password.");

            var token = JwtHelper.GenerateJwtToken(user, _configuration);

            return AuthResult.Success(token);
        }
    }

    public class AuthResult
    {
        public bool IsSuccess { get; private set; }
        public string Token { get; private set; }
        public string ErrorMessage { get; private set; }

        public static AuthResult Success(string token) => new AuthResult { IsSuccess = true, Token = token };
        public static AuthResult Failure(string errorMessage) => new AuthResult { IsSuccess = false, ErrorMessage = errorMessage };
    }
}
