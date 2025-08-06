using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;

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

            return await userRepository.CreateUser(user);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            return await userRepository.Login(email, password);
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
    }
}