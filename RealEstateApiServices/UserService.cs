using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;

namespace RealEstateApiServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        private readonly ITokenService tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.tokenService = tokenService;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await userRepository.GetAllUsers();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await userRepository.GetUserById(userId);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentException("Name cannot be null or empty");

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("Email cannot be null or empty");

            if (string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("Password cannot be null or empty");
            var createdUser = await userRepository.CreateUser(user);
            var tokenResponse = await tokenService.GenerateToken(createdUser);
            createdUser.Token = tokenResponse.Token!;

            return createdUser;

        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            var user = await userRepository.Login(email, password);
            if (user == null)
            {
                return null;
            }
            user.Token = GenerateAuthToken(user);
            return user;
        }

        public async Task<User?> UpdateUserAsync(int userId, User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (userId <= 0)
                throw new ArgumentException("Invalid user ID", nameof(userId));

            var existingUser = await userRepository.GetUserById(userId);
            if (existingUser == null)
                return null;

            return await userRepository.UpdateUser(userId, user);
        }

        public async Task<User?> DeleteUserAsync(int userId)
        {
            return await userRepository.DeleteUser(userId);
        }
        private string GenerateAuthToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("UserId",user.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValisIssues"],
            audience: configuration["JWT:ValisAudience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}