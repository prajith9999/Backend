using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;

namespace LandWind.Interfaces
{
    public interface ISubscriptionDetailsRepository
    {
        Task<List<SubscriptionDetails>> GetAll();
        Task<SubscriptionDetails> GetById(int id);
        Task<SubscriptionDetails> Create(SubscriptionDetails item);
        Task<SubscriptionDetails> Update(int id, SubscriptionDetails item);
        Task<bool> Delete(int id);
    }
}
