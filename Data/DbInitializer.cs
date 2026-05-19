using AzureLearningDocker.Models;

namespace AzureLearningDocker.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LearningContext context)
        {
            context.Database.EnsureCreated();

            // Check if database already has data
            if (context.Categories.Any())
            {
                return; // Database has been seeded
            }

            // Create sample categories
            var categories = new Category[]
            {
                new Category
                {
                    Name = "Azure Fundamentals",
                    Description = "Learn the basics of Microsoft Azure cloud platform including core concepts, services, and benefits.",
                    CreatedDate = DateTime.Now
                },
                new Category
                {
                    Name = "ASP.NET Core Mastery",
                    Description = "Complete guide to building modern web applications using ASP.NET Core and Entity Framework.",
                    CreatedDate = DateTime.Now
                },
                new Category
                {
                    Name = "Cloud Architecture",
                    Description = "Design and deploy scalable cloud solutions using Azure and industry best practices.",
                    CreatedDate = DateTime.Now
                }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Create sample chapters for Azure Fundamentals
            var chapters = new Chapter[]
            {
                new Chapter
                {
                    CategoryId = categories[0].Id,
                    Title = "Introduction to Cloud Computing",
                    Description = "Understand the fundamentals of cloud computing and how it differs from traditional on-premises infrastructure.",
                    DayNumber = 1,
                    CreatedDate = DateTime.Now
                },
                new Chapter
                {
                    CategoryId = categories[0].Id,
                    Title = "Azure Core Services",
                    Description = "Deep dive into the most important Azure services including compute, storage, and networking.",
                    DayNumber = 2,
                    CreatedDate = DateTime.Now
                },
                new Chapter
                {
                    CategoryId = categories[0].Id,
                    Title = "Azure Data Services",
                    Description = "Learn about databases, data warehousing, and analytics services in Azure.",
                    DayNumber = 3,
                    CreatedDate = DateTime.Now
                }
            };

            context.Chapters.AddRange(chapters);
            context.SaveChanges();

            // Create sample topics for Day 1
            var topics = new Topic[]
            {
                new Topic
                {
                    ChapterId = chapters[0].Id,
                    Title = "What is Cloud Computing?",
                    Content = "Cloud computing is the delivery of computing services including servers, storage, databases, networking, software, and analytics over the internet (the cloud) to offer faster innovation, flexible resources, and economies of scale.",
                    SequenceNumber = 1,
                    CreatedDate = DateTime.Now
                },
                new Topic
                {
                    ChapterId = chapters[0].Id,
                    Title = "Advantages of Cloud Computing",
                    Content = "Key benefits include reduced costs, scalability, reliability, security, and the ability to access services from anywhere. Organizations can focus on their core business while cloud providers handle infrastructure management.",
                    SequenceNumber = 2,
                    CreatedDate = DateTime.Now
                },
                new Topic
                {
                    ChapterId = chapters[0].Id,
                    Title = "Cloud Service Models",
                    Content = "There are three main cloud service models: Infrastructure as a Service (IaaS), Platform as a Service (PaaS), and Software as a Service (SaaS). Each offers different levels of management and control.",
                    SequenceNumber = 3,
                    CreatedDate = DateTime.Now
                }
            };

            context.Topics.AddRange(topics);
            context.SaveChanges();

            // Create sample Q&A for Day 1
            var questionAnswers = new QuestionAnswer[]
            {
                new QuestionAnswer
                {
                    ChapterId = chapters[0].Id,
                    Question = "What are the three cloud service models?",
                    Answer = "The three cloud service models are: IaaS (Infrastructure as a Service), PaaS (Platform as a Service), and SaaS (Software as a Service). Each provides different levels of control and management.",
                    SequenceNumber = 1,
                    CreatedDate = DateTime.Now
                },
                new QuestionAnswer
                {
                    ChapterId = chapters[0].Id,
                    Question = "Name three advantages of cloud computing.",
                    Answer = "Three key advantages are: (1) Cost-effective - pay-as-you-go pricing, (2) Scalability - easily scale resources up or down, (3) Accessibility - access services from anywhere with internet connection.",
                    SequenceNumber = 2,
                    CreatedDate = DateTime.Now
                },
                new QuestionAnswer
                {
                    ChapterId = chapters[0].Id,
                    Question = "How is IaaS different from PaaS?",
                    Answer = "IaaS provides virtual computing resources over the internet with customer managing applications, data, runtime, and middleware. PaaS provides development tools and platforms, allowing developers to focus on application development without managing infrastructure.",
                    SequenceNumber = 3,
                    CreatedDate = DateTime.Now
                }
            };

            context.QuestionAnswers.AddRange(questionAnswers);
            context.SaveChanges();
        }
    }
}
