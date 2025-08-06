using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;
using RealEstateApiCore.DTOs;

namespace RealEstateApiServices.Contacts
{
    public interface IUserService
    {
       Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(RegisterDto registerDto);
        Task<User?> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
        Task<User?> DeleteUserAsync(int userId);
        Task<User?> LoginAsync(LoginDto loginDto);
    }
}