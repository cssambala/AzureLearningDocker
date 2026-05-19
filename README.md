# Azure Learning Docker - Online Learning App

A comprehensive ASP.NET Core 8 Razor Pages application for managing online learning content with a structured hierarchy of Categories, Chapters (organized by days), Topics, Question & Answers, and PDF resources.

## Project Structure

### Models
- **Category**: Top-level organization unit for learning paths
- **Chapter**: Day-wise chapters within a category (e.g., Day 1, Day 2, etc.)
- **Topic**: Individual topics/lessons within each chapter
- **QuestionAnswer**: Q&A display items for each chapter (not for MCQ)
- **PdfAttachment**: PDF resources/references for each chapter

### Features

? **Category Management**
- Create, edit, view, and delete learning categories
- View all chapters within a category
- Track number of topics and resources per chapter

? **Chapter Management**
- Organize chapters by day number
- Link chapters to categories
- View detailed chapter information with all related content

? **Topic Management**
- Create multiple topics per chapter
- Organize topics with sequence numbers
- Rich text content support for each topic

? **Question & Answer Management**
- Create Q&A pairs for learning reinforcement
- Display-only format (not multiple choice)
- Sequence numbering for organization
- Accordion view for easy browsing

? **PDF Resources**
- Upload PDF attachments to chapters
- Track file size and upload date
- Download functionality
- Automatic file management

? **User Interface**
- Bootstrap 5 responsive design
- Intuitive navigation menu
- Beautiful card and accordion layouts
- Mobile-friendly interface

## Database Setup

### Connection String
The app uses a local SQL Server database (LocalDB) by default:
```
Server=(localdb)\mssqllocaldb;Database=AzureLearningDb;Trusted_Connection=true;
```

To change the connection string, edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-connection-string-here"
  }
}
```

### Creating the Database

1. **Using Package Manager Console**:
   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

2. **Using dotnet CLI**:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Project File Structure

```
AzureLearningDocker/
??? Models/
?   ??? Category.cs
?   ??? Chapter.cs
?   ??? Topic.cs
?   ??? QuestionAnswer.cs
?   ??? PdfAttachment.cs
??? Controllers/
?   ??? CategoriesController.cs
?   ??? ChaptersController.cs
?   ??? TopicsController.cs
?   ??? QuestionAnswersController.cs
?   ??? PdfAttachmentsController.cs
??? Data/
?   ??? LearningContext.cs
??? Views/
?   ??? Categories/
?   ??? Chapters/
?   ??? Topics/
?   ??? QuestionAnswers/
?   ??? PdfAttachments/
?   ??? Shared/
??? wwwroot/
?   ??? uploads/
?       ??? pdfs/
??? Program.cs
??? appsettings.json
??? AzureLearningDocker.csproj
```

## API Endpoints

### Categories
- `GET /Categories` - List all categories
- `GET /Categories/Details/{id}` - View category details with chapters
- `GET /Categories/Create` - Create new category form
- `POST /Categories/Create` - Create new category
- `GET /Categories/Edit/{id}` - Edit category form
- `POST /Categories/Edit/{id}` - Save category changes
- `GET /Categories/Delete/{id}` - Delete confirmation
- `POST /Categories/Delete/{id}` - Delete category

### Chapters
- `GET /Chapters` - List all chapters
- `GET /Chapters?categoryId={id}` - List chapters by category
- `GET /Chapters/Details/{id}` - View chapter with all resources
- `GET /Chapters/Create` - Create new chapter form
- `POST /Chapters/Create` - Create new chapter
- `GET /Chapters/Edit/{id}` - Edit chapter form
- `POST /Chapters/Edit/{id}` - Save chapter changes
- `GET /Chapters/Delete/{id}` - Delete confirmation
- `POST /Chapters/Delete/{id}` - Delete chapter

### Topics
- `GET /Topics` - List all topics
- `GET /Topics?chapterId={id}` - List topics by chapter
- `GET /Topics/Details/{id}` - View topic details
- `GET /Topics/Create?chapterId={id}` - Create new topic form
- `POST /Topics/Create` - Create new topic
- `GET /Topics/Edit/{id}` - Edit topic form
- `POST /Topics/Edit/{id}` - Save topic changes
- `GET /Topics/Delete/{id}` - Delete confirmation
- `POST /Topics/Delete/{id}` - Delete topic

### Question & Answers
- `GET /QuestionAnswers` - List all Q&A
- `GET /QuestionAnswers?chapterId={id}` - List Q&A by chapter
- `GET /QuestionAnswers/Details/{id}` - View Q&A details
- `GET /QuestionAnswers/Create?chapterId={id}` - Create new Q&A form
- `POST /QuestionAnswers/Create` - Create new Q&A
- `GET /QuestionAnswers/Edit/{id}` - Edit Q&A form
- `POST /QuestionAnswers/Edit/{id}` - Save Q&A changes
- `GET /QuestionAnswers/Delete/{id}` - Delete confirmation
- `POST /QuestionAnswers/Delete/{id}` - Delete Q&A

### PDF Attachments
- `GET /PdfAttachments` - List all PDFs
- `GET /PdfAttachments?chapterId={id}` - List PDFs by chapter
- `GET /PdfAttachments/Details/{id}` - View PDF details
- `GET /PdfAttachments/Create?chapterId={id}` - Upload PDF form
- `POST /PdfAttachments/Create` - Upload PDF
- `GET /PdfAttachments/Delete/{id}` - Delete confirmation
- `POST /PdfAttachments/Delete/{id}` - Delete PDF

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB recommended)
- Visual Studio 2022 or VS Code

### Installation

1. Clone or extract the project
2. Open the project in Visual Studio
3. Restore NuGet packages: `dotnet restore`
4. Create the database:
   ```
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
5. Run the application: `F5` or `dotnet run`
6. Open browser to `https://localhost:5001`

### First Steps

1. Navigate to **Categories** in the top menu
2. Click **Create New Category**
3. Add a category name and description
4. Click **View** on your category
5. Click **Add New Chapter**
6. Fill in chapter details (Day 1, 2, etc.)
7. In chapter details, add:
   - Topics (use **Add Topic**)
   - Q&A pairs (use **Add Q&A**)
   - PDF resources (use **Add PDF**)

## File Upload Configuration

### PDF Upload Location
PDFs are stored in: `wwwroot/uploads/pdfs/`

### File Validation
- Only PDF files allowed
- Recommended max size: 10MB
- File size validation in JavaScript and server-side

### Configure Upload Path
To change the upload location, modify the path in `PdfAttachmentsController.cs`:
```csharp
var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "pdfs");
```

## Database Relationships

```
Category (1) ??? (Many) Chapter
   ?
Chapter (1) ??? (Many) Topic
Chapter (1) ??? (Many) QuestionAnswer
Chapter (1) ??? (Many) PdfAttachment
```

### Cascade Delete
- Deleting a Category removes all its Chapters
- Deleting a Chapter removes all its Topics, Q&A, and PDFs
- All relationships use cascade delete for data integrity

## Technology Stack

- **Framework**: ASP.NET Core 8
- **Database**: Entity Framework Core 8
- **Database Provider**: SQL Server
- **Frontend**: Bootstrap 5, HTML5, CSS3
- **Language**: C#

## NuGet Packages

- `Microsoft.EntityFrameworkCore` (8.0.0)
- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (8.0.0)
- `Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation` (8.0.0)

## Future Enhancements

- User authentication and authorization
- Student progress tracking
- Quiz/Assessment functionality
- Search and filtering
- Content export to PDF
- Statistics and analytics dashboard
- Email notifications
- Mobile app integration
- Multilingual support

## Troubleshooting

### Database Connection Issues
- Ensure SQL Server (LocalDB) is running
- Check connection string in `appsettings.json`
- Run: `sqllocaldb start` (if using LocalDB)

### File Upload Issues
- Ensure `wwwroot/uploads/pdfs/` folder exists
- Check folder permissions
- Verify PDF file is not corrupted

### Missing Tables
Run migrations:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Support & Documentation

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Bootstrap 5](https://getbootstrap.com/docs/5.0)

## License

This project is open source and available for educational purposes.

---

**Happy Learning!** ????
