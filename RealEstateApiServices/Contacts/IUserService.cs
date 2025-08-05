using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;

namespace RealEstateApiServices.Contacts
{
    public interface IUserService
    {
       Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(int userId, User user);
        Task<User?> DeleteUserAsync(int userId);
    }
}