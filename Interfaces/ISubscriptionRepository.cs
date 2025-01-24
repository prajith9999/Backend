using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;

namespace LandWind.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<List<Subscription>> GetAll();
        Task<Subscription> GetById(int id);
        Task<Subscription> Create(Subscription item);
        Task<Subscription> Update(int id, Subscription item);
        Task<bool> Delete(int id);
    }
}
