
namespace vueproject_asp.Models
{
    public class SocialMedia
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public object PlatformName { get; internal set; }
        public object IconUrl { get; internal set; }
        public object ProfileUrl { get; internal set; }

        public SocialMedia(int id, string name, string url, string description)
        {
            ID = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            URL = url ?? throw new ArgumentNullException(nameof(url));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
