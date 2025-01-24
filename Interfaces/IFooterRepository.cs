using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;

namespace LandWind.Interfaces
{
    public interface IFooterRepository
    {
        Task<List<Footer>> GetAll();
        Task<Footer> GetById(int id);
        Task<Footer> Create(Footer item);
        Task<Footer> Update(int id, Footer item);
        Task<bool> Delete(int id);
    }
}
