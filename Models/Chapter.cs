namespace AzureLearningDocker.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DayNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public Category? Category { get; set; }
        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
        public ICollection<QuestionAnswer> QuestionAnswers { get; set; } = new List<QuestionAnswer>();
        public ICollection<PdfAttachment> PdfAttachments { get; set; } = new List<PdfAttachment>();
        public ICollection<UserChapterSubscription> UserSubscriptions { get; set; } = new List<UserChapterSubscription>();
    }
}
