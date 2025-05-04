using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;

namespace LandWind.Interfaces
{
    public interface ISocialMediaRepository
    {
        Task<List<SocialMedia>> GetAll();
        Task<SocialMedia> GetById(int id);
        Task<SocialMedia> Create(SocialMedia item);
        Task<SocialMedia> Update(int id, SocialMedia item);
        Task<bool> Delete(int id);
    }
}
