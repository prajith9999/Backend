using Microsoft.EntityFrameworkCore;
using vueproject_asp.Models;

namespace vueproject_asp.Data  // Ensure the namespace is correct
{
    public class AppDbContext : DbContext
    {
        // Primary Constructor for AppDbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet properties for each table in your database
        public DbSet<PageContent> PageContents { get; set; }
        public DbSet<Body> Bodies { get; set; }
        public DbSet<FeaturePage> FeaturePages { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionDetails> SubscriptionDetails { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<User> Users { get; set; }

        // Optionally, override OnModelCreating for custom configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customizing table names (if needed)
            modelBuilder.Entity<PageContent>().ToTable("PageContents");
            modelBuilder.Entity<Body>().ToTable("Bodies");
            modelBuilder.Entity<FeaturePage>().ToTable("FeaturePages");
            modelBuilder.Entity<Subscription>().ToTable("Subscriptions");
            modelBuilder.Entity<SubscriptionDetails>().ToTable("SubscriptionDetails");
            modelBuilder.Entity<Faq>().ToTable("Faqs");
            modelBuilder.Entity<Footer>().ToTable("Footers");
            modelBuilder.Entity<SocialMedia>().ToTable("SocialMedias");
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
