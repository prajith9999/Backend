using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vueproject_asp.Data;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class FaqRepository
    {
        private readonly AppDbContext _context;

        public FaqRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all FAQs
        public async Task<List<Faq>> GetFaqs()
        {
            return await _context.Faqs.ToListAsync();
        }

        // Get FAQ by ID
        public async Task<Faq> GetFaqById(int id)
        {
            return await _context.Faqs.FirstOrDefaultAsync(f => f.ID == id);
        }

        // Create a new FAQ
        public async Task<Faq> CreateFaq(Faq faq)
        {
            await _context.Faqs.AddAsync(faq);
            await _context.SaveChangesAsync();
            return faq;
        }

        // Update an existing FAQ
        public async Task<Faq> UpdateFaq(int id, Faq faq)
        {
            var existingFaq = await GetFaqById(id);
            if (existingFaq == null)
            {
                return null;
            }

            // Update properties according to the new model
            existingFaq.Title = faq.Title;
            existingFaq.Description = faq.Description;
            existingFaq.OrderNumber = faq.OrderNumber;
            existingFaq.ModifiedBy = faq.ModifiedBy;
            existingFaq.ModifiedDate = faq.ModifiedDate;

            _context.Faqs.Update(existingFaq);
            await _context.SaveChangesAsync();
            return existingFaq;
        }

        // Delete a FAQ
        public async Task<bool> DeleteFaq(int id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq != null)
            {
                _context.Faqs.Remove(faq);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
