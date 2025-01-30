using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Helpers;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Exceptions;
using LibraryManagement.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AuthResult> RegisterUserAsync(RegisterDto registerDto)
        {
            var existingUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(registerDto.Email);
            if (existingUser != null)
                throw new AlreadyExistsException("User with this email already exists.");

            var user = _mapper.Map<User>(registerDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            user.Role = "User";

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var token = JwtHelper.GenerateJwtToken(user, _configuration);

            return AuthResult.Success(token);
        }

        public async Task<AuthResult> AuthenticateUserAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                throw new UnauthorizedException("Invalid email or password.");

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
