using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vueproject_asp.Data;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class PageContentRepository
    {
        private readonly AppDbContext _context;

        // Constructor for PageContentRepository
        public PageContentRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all page content entries
        public async Task<List<PageContent>> GetPageContents()
        {
            return await _context.PageContents.ToListAsync();
        }

        // Get a single page content entry by its ID
        public async Task<PageContent> GetPageContentById(int id)
        {
            var pageContent = await _context.PageContents.FirstOrDefaultAsync(p => p.ID == id);
            return pageContent;
        }

        // Insert a new page content entry
        public async Task InsertPageContent(PageContent pageContent)
        {
            await _context.PageContents.AddAsync(pageContent);
            await _context.SaveChangesAsync();
        }

        // Update an existing page content entry
        public async Task UpdatePageContent(PageContent pageContent)
        {
            _context.PageContents.Update(pageContent);
            await _context.SaveChangesAsync();
        }

        // Delete a page content entry by its ID
        public async Task<bool> DeletePageContent(int id)
        {
            var pageContent = await _context.PageContents.FindAsync(id);
            if (pageContent != null)
            {
                _context.PageContents.Remove(pageContent);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Create page content (can be renamed to create)
        public async Task<PageContent> CreatePageContent(PageContent pageContent)
        {
            _context.PageContents.Add(pageContent);
            await _context.SaveChangesAsync();
            return pageContent;
        }
    }
}
