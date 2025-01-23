using System;

namespace vueproject_asp.Models
{
    public class SocialMedia
    {
        public int ID { get; set; }  // Unique identifier for the social media record
        public string Name { get; set; }  // Name of the social media platform (e.g., "Facebook", "Twitter")
        public string URL { get; set; }   // URL to the social media page (e.g., "https://www.facebook.com")

        // Constructor to initialize the SocialMedia object
        public SocialMedia(int id, string name, string url)
        {
            ID = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));  // Ensure name is not null
            URL = url ?? throw new ArgumentNullException(nameof(url));      // Ensure URL is not null
        }

        // Parameterless constructor for flexibility (used for deserialization, etc.)
        public SocialMedia() { }
    }
}
