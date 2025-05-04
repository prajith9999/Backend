using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;

namespace LandWind.Interfaces
{
    public interface IFeaturePageRepository
    {
        Task<List<FeaturePage>> GetAll();
        Task<FeaturePage> GetById(int id);
        Task<FeaturePage> Create(FeaturePage item);
        Task<FeaturePage> Update(int id, FeaturePage item);
        Task<bool> Delete(int id);
    }
}
