namespace AzureLearningDocker.Models
{
    public class PdfAttachment
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public long FileSizeInBytes { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation property
        public Chapter? Chapter { get; set; }
    }
}
