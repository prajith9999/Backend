using System;

namespace vueproject_asp.Models
{
    public class SocialMedia
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }  // Keep only this one

        // Constructor to initialize SocialMedia object
        public SocialMedia(int id, string name, string url)
        {
            ID = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            URL = url ?? throw new ArgumentNullException(nameof(url));
        }

        public SocialMedia() { } // Parameterless constructor for flexibility
    }
}
