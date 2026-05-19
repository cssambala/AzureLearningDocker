namespace AzureLearningDocker.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int SequenceNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation property
        public Chapter? Chapter { get; set; }
        public ICollection<UserTopicSubscription> UserSubscriptions { get; set; } = new List<UserTopicSubscription>();
    }
}
