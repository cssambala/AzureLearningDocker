namespace AzureLearningDocker.Models
{
    public class UserTopicSubscription
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int TopicId { get; set; }
        public DateTime SubscribedDate { get; set; } = DateTime.Now;
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedDate { get; set; }

        // Navigation properties
        public ApplicationUser? User { get; set; }
        public Topic? Topic { get; set; }
    }
}
