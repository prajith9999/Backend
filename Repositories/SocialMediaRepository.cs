using vueproject_asp.Models;

namespace YourNamespace.Repositories
{
    public class SocialMediaRepository
    {
        private readonly List<SocialMedia> _socialMedias;

        // Constructor initializes the list
        public SocialMediaRepository()
        {
            _socialMedias = new List<SocialMedia>();
        }

        // Get all social media
        public Task<List<SocialMedia>> GetSocialMediaAsync()
        {
            return Task.FromResult(_socialMedias);
        }

        // Get social media by ID
        public Task<SocialMedia> GetSocialMediaByIdAsync(int id)
        {
            var socialMedia = _socialMedias.FirstOrDefault(s => s.ID == id);
            return Task.FromResult(socialMedia);
        }

        // Create new social media
        public Task<SocialMedia> CreateSocialMediaAsync(SocialMedia socialMedia)
        {
            socialMedia.ID = _socialMedias.Count + 1; // Assigning ID based on list count
            _socialMedias.Add(socialMedia);
            return Task.FromResult(socialMedia);
        }

        // Update existing social media by ID
        public Task<SocialMedia> UpdateSocialMediaAsync(int id, SocialMedia socialMedia)
        {
            var existingSocialMedia = _socialMedias.FirstOrDefault(s => s.ID == id);
            if (existingSocialMedia == null)
            {
                return Task.FromResult<SocialMedia>(null); // Return null if not found
            }

            // Update properties
            existingSocialMedia.PlatformName = socialMedia.PlatformName;
            existingSocialMedia.IconUrl = socialMedia.IconUrl;
            existingSocialMedia.ProfileUrl = socialMedia.ProfileUrl;
            existingSocialMedia.Description = socialMedia.Description;

            return Task.FromResult(existingSocialMedia);
        }

        // Delete social media by ID
        public Task<bool> DeleteSocialMediaAsync(int id)
        {
            var socialMedia = _socialMedias.FirstOrDefault(s => s.ID == id);
            if (socialMedia == null)
            {
                return Task.FromResult(false); // Return false if not found
            }

            _socialMedias.Remove(socialMedia);
            return Task.FromResult(true);
        }
    }
}
