using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vueproject_asp.Data;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class FeaturePageRepository
    {
        private readonly AppDbContext _context;

        // Constructor for FeaturePageRepository
        public FeaturePageRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all feature page entries
        public async Task<List<FeaturePage>> GetFeaturePages()
        {
            return await _context.FeaturePages.ToListAsync();
        }

        // Get a single feature page entry by its ID
        public async Task<FeaturePage> GetFeaturePageById(int id)
        {
            return await _context.FeaturePages.FirstOrDefaultAsync(f => f.ID == id);
        }

        // Insert a new feature page entry
        public async Task InsertFeaturePage(FeaturePage featurePage)
        {
            await _context.FeaturePages.AddAsync(featurePage);
            await _context.SaveChangesAsync();
        }

        // Update an existing feature page entry
        public async Task UpdateFeaturePage(FeaturePage featurePage)
        {
            _context.FeaturePages.Update(featurePage);
            await _context.SaveChangesAsync();
        }

        // Delete a feature page entry by its ID
        public async Task<bool> DeleteFeaturePage(int id)
        {
            var featurePage = await _context.FeaturePages.FindAsync(id);
            if (featurePage != null)
            {
                _context.FeaturePages.Remove(featurePage);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Create feature page (this can be renamed and implemented)
        public async Task<FeaturePage> CreateFeaturePage(FeaturePage featurePage)
        {
            _context.FeaturePages.Add(featurePage);
            await _context.SaveChangesAsync();
            return featurePage;
        }
    }
}
