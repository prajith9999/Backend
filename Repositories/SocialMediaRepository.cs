using vueproject_asp.Data;
using vueproject_asp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vueproject_asp.Repositories
{
    public class SocialMediaRepository
    {
        private readonly AppDbContext _context;

        // Constructor for SocialMediaRepository
        public SocialMediaRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all social media entries
        public async Task<List<SocialMedia>> GetSocialMedia()
        {
            return await _context.SocialMedias.ToListAsync();  // Correct DbSet name
        }

        // Get a single social media entry by its ID
        public async Task<SocialMedia> GetSocialMediaById(string id)  // ID is a string in your model
        {
            return await _context.SocialMedias.FirstOrDefaultAsync(s => s.ID == id);  // Correct comparison for string ID
        }

        // Create a new social media entry
        public async Task<SocialMedia> CreateSocialMedia(SocialMedia socialMedia)
        {
            ArgumentNullException.ThrowIfNull(socialMedia);

            await _context.SocialMedias.AddAsync(socialMedia);  // Correct DbSet name
            await _context.SaveChangesAsync();
            return socialMedia;
        }

        // Update an existing social media entry
        public async Task<SocialMedia?> UpdateSocialMedia(string id, SocialMedia socialMedia)  // ID is a string
        {
            var existingSocialMedia = await _context.SocialMedias.FindAsync(id);  // FindAsync with string ID

            if (existingSocialMedia == null) return null;

            existingSocialMedia.PlatformName = socialMedia.PlatformName;
            existingSocialMedia.ProfileUrl = socialMedia.ProfileUrl;

            // Update other fields as needed
            await _context.SaveChangesAsync();
            return existingSocialMedia;
        }

        // Delete a social media entry
        public async Task<bool> DeleteSocialMedia(string id)  // ID is a string
        {
            var socialMedia = await _context.SocialMedias.FindAsync(id);  // FindAsync with string ID

            if (socialMedia == null) return false;

            _context.SocialMedias.Remove(socialMedia);  // Correct DbSet name
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
