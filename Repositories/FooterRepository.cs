using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vueproject_asp.Data;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class FooterRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        // Method to fetch all Footer records
        public async Task<List<Footer>> GetFooters()
        {
            return await _context.Footers.ToListAsync();
        }

        // Method to fetch a single Footer record by ID
        public async Task<Footer> GetFooterById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Footers.FirstOrDefaultAsync(f => f.ID == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        // Method to insert a new Footer record
        public async Task<Footer> CreateFooter(Footer footer)
        {
            if (footer == null)
            {
                throw new ArgumentNullException(nameof(footer), "Footer cannot be null.");
            }

            await _context.Footers.AddAsync(footer);
            await _context.SaveChangesAsync();
            return footer;  // Return the created Footer
        }

        // Method to insert a new Footer record (alternative method name)
        public async Task InsertFooter(Footer footer)
        {
            if (footer == null)
            {
                throw new ArgumentNullException(nameof(footer), "Footer cannot be null.");
            }

            await _context.Footers.AddAsync(footer);
            await _context.SaveChangesAsync();
        }

        // Method to update an existing Footer record
        public async Task UpdateFooter(Footer footer)
        {
            if (footer == null)
            {
                throw new ArgumentNullException(nameof(footer), "Footer cannot be null.");
            }

            _context.Footers.Update(footer);
            await _context.SaveChangesAsync();
        }

        // Method to delete a Footer record by ID
        public async Task DeleteFooter(int id)
        {
            var footer = await _context.Footers.FindAsync(id);
            if (footer != null)
            {
                _context.Footers.Remove(footer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
