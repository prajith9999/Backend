using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vueproject_asp.Data;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class SubscriptionDetailsRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<SubscriptionDetails>> GetSubscriptionDetails()
        {
            return await _context.SubscriptionDetails.ToListAsync();
        }

        public async Task<SubscriptionDetails> GetSubscriptionDetailById(int id)
        {
            return await _context.SubscriptionDetails.FirstOrDefaultAsync(s => s.DetailID == id);
        }

        public async Task InsertSubscriptionDetail(SubscriptionDetails subscriptionDetail)
        {
            await _context.SubscriptionDetails.AddAsync(subscriptionDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubscriptionDetail(SubscriptionDetails subscriptionDetail)
        {
            _context.SubscriptionDetails.Update(subscriptionDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscriptionDetail(int id)
        {
            var subscriptionDetail = await _context.SubscriptionDetails.FindAsync(id);
            if (subscriptionDetail != null)
            {
                _context.SubscriptionDetails.Remove(subscriptionDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
