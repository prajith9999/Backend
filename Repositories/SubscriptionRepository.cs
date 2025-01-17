using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vueproject_asp.Data;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class SubscriptionRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Subscription>> GetSubscriptions()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> GetSubscriptionById(int id)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task InsertSubscription(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubscription(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }
    }
}
