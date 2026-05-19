namespace AzureLearningDocker.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public int SequenceNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation property
        public Chapter? Chapter { get; set; }
    }
}
