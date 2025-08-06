using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;

namespace RealEstateApiRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await applicationDbContext.User.ToListAsync();
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await applicationDbContext.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> CreateUser(User user)
        {
            var result = await applicationDbContext.User.AddAsync(user);
            await applicationDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User?> Login(string email, string password)
        {
            var result = await applicationDbContext.User
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return result;

        }

        public async Task<User?> UpdateUser(int userId, User user)
        {
            var result = await applicationDbContext.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (result != null)
            {
                result.Name = user.Name;
                result.Email = user.Email;
                result.Password = user.Password;

                await applicationDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<User?> DeleteUser(int userId)
        {
            var result = await applicationDbContext.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (result != null)
            {
                applicationDbContext.User.Remove(result);
                await applicationDbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}

