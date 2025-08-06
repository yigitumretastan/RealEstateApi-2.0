using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;
using RealEstateApiCore.DTOs;

namespace RealEstateApiServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await userRepository.GetAllUsers();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await userRepository.GetUserById(userId);
        }

        public async Task<User> CreateUserAsync(RegisterDto registerDto)
        {
            if (registerDto == null)
                throw new ArgumentNullException(nameof(registerDto));

            var newUser = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password
            };

            return await userRepository.CreateUser(newUser);
        }

        public async Task<User?> LoginAsync(LoginDto loginDto)
        {
            if (loginDto == null)
                throw new ArgumentNullException(nameof(loginDto));

            if (string.IsNullOrEmpty(loginDto.Email))
                throw new ArgumentException("Email cannot be null or empty", nameof(loginDto.Email));

            if (string.IsNullOrEmpty(loginDto.Password))
                throw new ArgumentException("Password cannot be null or empty", nameof(loginDto.Password));

            return await userRepository.Login(loginDto.Email, loginDto.Password);
        }

        public async Task<User?> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            if (updateUserDto == null)
                throw new ArgumentNullException(nameof(updateUserDto));

            if (userId <= 0)
                throw new ArgumentException("Invalid user ID", nameof(userId));

            var existingUser = await userRepository.GetUserById(userId);
            if (existingUser == null)
                throw new Exception("User not found");

            existingUser.Name = updateUserDto.Name;
            existingUser.Email = updateUserDto.Email;
            existingUser.Password = updateUserDto.Password; 

            return await userRepository.UpdateUser(userId, existingUser);
        }

        public async Task<User?> DeleteUserAsync(int userId)
        {
            return await userRepository.DeleteUser(userId);
        }
    }
}
