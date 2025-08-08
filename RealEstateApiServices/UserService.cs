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
using System.Text.RegularExpressions;

namespace RealEstateApiServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly ITokenService tokenService;
        private readonly IPasswordService passwordService;

        public UserService(IUserRepository userRepository, ITokenService tokenService, IConfiguration configuration, IPasswordService passwordService)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.tokenService = tokenService;
            this.passwordService = passwordService;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await userRepository.GetAllUsers();
        }

        public async Task<User?> GetUserByIdAsync(int userId) => await userRepository.GetUserById(userId);

        public async Task<User?> GetUserByEmailAsync(string email) => await userRepository.GetUserByEmail(email);

        public async Task<User> CreateUserAsync(User user)
        {
            var controluser = await userRepository.GetUserByEmail(user.Email);
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (controluser == null)
                throw new ArgumentException("This email is already registered", nameof(user));

            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentException("Name cannot be null or empty");

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("Email cannot be null or empty");

            if (string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("Password cannot be null or empty");
            if (!IsValidPassword(user.Password))
                throw new ArgumentException("Password must contain at least one uppercase letter, one lowercase letter, one number, and be at least 8 characters long.");
            if (!IsValidEmail(user.Email))
                throw new ArgumentException("Email address must include @ and .");
            user.Password = passwordService.HashPassword(user.Password);


            var createdUser = await userRepository.CreateUser(user);
            var tokenResponse = await tokenService.GenerateToken(createdUser);
            createdUser.Token = tokenResponse.Token;

            return createdUser;

        }
        
        public async Task<User?> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            var user = await userRepository.GetUserByEmail(email);
            //    var user = await userRepository.Login(email, password);
            if (user == null)
            {
                return null;
            }

            if (!passwordService.VerifyPassword(password, user.Password))
            {
                throw new ArgumentException("Invalid email or password.");
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
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
            audience: configuration["JWT:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool IsValidPassword(string password)
        {
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }
        public bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}