using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;

namespace RealEstateApiRepositories.Contacts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(int userId);
        Task<User?> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<User?> Login(string email, string password);
        Task<User?> UpdateUser(int userId, User user);
        Task<User?> DeleteUser(int userId);
        Task<int> GetTotalCount();
        Task<IEnumerable<User>> GetPagedUser(int pageNumber,int pageSize);
    }
}
