namespace AzureLearningDocker.Models
{
    public class UserChapterSubscription
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ChapterId { get; set; }
        public DateTime SubscribedDate { get; set; } = DateTime.Now;
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedDate { get; set; }
        public double ProgressPercentage { get; set; } = 0;

        // Navigation properties
        public ApplicationUser? User { get; set; }
        public Chapter? Chapter { get; set; }
    }
}
