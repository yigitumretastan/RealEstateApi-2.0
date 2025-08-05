using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;

namespace RealEstateApiRepositories.Contacts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(int userId);
        Task<User> CreateUser(User user);
        Task<User?> UpdateUser(int userId, User user);
        Task<User?> DeleteUser(int userId);
    }
}
