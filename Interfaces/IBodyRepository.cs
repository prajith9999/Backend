using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;

namespace LandWind.Interfaces
{
    public interface IBodyHandler
    {
        Task<List<Body>> GetAllBodies();
        Task<Body> GetBodyById(int id);
        Task<Body> CreateBody(Body body);
        Task<Body> UpdateBody(int id, Body body);
        Task<bool> DeleteBody(int id);
    }
}
