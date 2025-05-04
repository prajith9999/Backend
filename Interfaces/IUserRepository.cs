using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces; // Assuming User is in LandWind.Models

namespace LandWind.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
    }
}
