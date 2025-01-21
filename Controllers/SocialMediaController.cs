using vueproject_asp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vueproject_asp.Repositories
{
    public class SocialMediaRepository
    {
        private readonly List<SocialMedia> _socialMedias = new List<SocialMedia>();

        // Get all social media
        public async Task<List<SocialMedia>> GetSocialMedia()
        {
            return await Task.FromResult(_socialMedias);
        }

        // Get social media by ID
        public async Task<SocialMedia> GetSocialMediaById(int id)
        {
            var socialMedia = _socialMedias.FirstOrDefault(s => s.ID == id);
            return await Task.FromResult(socialMedia);
        }

        // Create social media
        public async Task<SocialMedia> CreateSocialMedia(SocialMedia socialMedia)
        {
            socialMedia.ID = _socialMedias.Count + 1;
            _socialMedias.Add(socialMedia);
            return await Task.FromResult(socialMedia);
        }

        // Update social media
        public async Task<SocialMedia> UpdateSocialMedia(int id, SocialMedia socialMedia)
        {
            var existingSocialMedia = _socialMedias.FirstOrDefault(s => s.ID == id);
            if (existingSocialMedia == null) return null;

            existingSocialMedia.Name = socialMedia.Name;
            existingSocialMedia.URL = socialMedia.URL;
            existingSocialMedia.Description = socialMedia.Description;

            return await Task.FromResult(existingSocialMedia);
        }

        // Delete social media
        public async Task<bool> DeleteSocialMedia(int id)
        {
            var socialMedia = _socialMedias.FirstOrDefault(s => s.ID == id);
            if (socialMedia == null) return false;

            _socialMedias.Remove(socialMedia);
            return await Task.FromResult(true);
        }
    }
}
