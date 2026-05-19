using AzureLearningDocker.Models;
using AzureLearningDocker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AzureLearningDocker.Data
{
    public class LearningContext : IdentityDbContext<ApplicationUser>
    {
        public LearningContext(DbContextOptions<LearningContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<PdfAttachment> PdfAttachments { get; set; }
        public DbSet<UserChapterSubscription> UserChapterSubscriptions { get; set; }
        public DbSet<UserTopicSubscription> UserTopicSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category relationships
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Chapters)
                .WithOne(ch => ch.Category)
                .HasForeignKey(ch => ch.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Chapter relationships
            modelBuilder.Entity<Chapter>()
                .HasMany(c => c.Topics)
                .WithOne(t => t.Chapter)
                .HasForeignKey(t => t.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chapter>()
                .HasMany(c => c.QuestionAnswers)
                .WithOne(qa => qa.Chapter)
                .HasForeignKey(qa => qa.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chapter>()
                .HasMany(c => c.PdfAttachments)
                .WithOne(pa => pa.Chapter)
                .HasForeignKey(pa => pa.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            // User Chapter Subscription relationships
            modelBuilder.Entity<UserChapterSubscription>()
                .HasOne(ucs => ucs.User)
                .WithMany(u => u.ChapterSubscriptions)
                .HasForeignKey(ucs => ucs.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserChapterSubscription>()
                .HasOne(ucs => ucs.Chapter)
                .WithMany(c => c.UserSubscriptions)
                .HasForeignKey(ucs => ucs.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            // User Topic Subscription relationships
            modelBuilder.Entity<UserTopicSubscription>()
                .HasOne(uts => uts.User)
                .WithMany(u => u.TopicSubscriptions)
                .HasForeignKey(uts => uts.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserTopicSubscription>()
                .HasOne(uts => uts.Topic)
                .WithMany(t => t.UserSubscriptions)
                .HasForeignKey(uts => uts.TopicId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add unique constraint on subscriptions
            modelBuilder.Entity<UserChapterSubscription>()
                .HasIndex(ucs => new { ucs.UserId, ucs.ChapterId })
                .IsUnique();

            modelBuilder.Entity<UserTopicSubscription>()
                .HasIndex(uts => new { uts.UserId, uts.TopicId })
                .IsUnique();
        }
    }
}
