using Microsoft.AspNetCore.Identity;

namespace AzureLearningDocker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<UserChapterSubscription> ChapterSubscriptions { get; set; } = new List<UserChapterSubscription>();
        public ICollection<UserTopicSubscription> TopicSubscriptions { get; set; } = new List<UserTopicSubscription>();
    }
}
