using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories;
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

        public async Task<User> CreateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentOutOfRangeException(nameof(user));
            return await userRepository.CreateUser(user);
        }

        public async Task<User?> DeleteUserAsync(int userId)
        {
            var existingUser = await userRepository.GetUserById(userId);
            if (existingUser == null)
                return null;

            return await userRepository.DeleteUser(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await userRepository.GetAllUsers();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await userRepository.GetUserById(userId);
        }

        public async Task<User?> UpdateUserAsync(int userId, User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (userId != user.Id)
                throw new ArgumentException("User ID mismatch");

            var existingUser = await userRepository.GetUserById(userId);
            if (existingUser == null)
                return null;
            return await userRepository.UpdateUser(userId, user);
        }
    }
}