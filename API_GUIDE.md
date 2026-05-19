# API Usage Guide - Online Learning App

This guide covers all the controllers and their endpoints in the Online Learning App.

## Base URL
```
https://localhost:5001
```

---

## 1. Categories Controller

### List All Categories
```
GET /Categories
```

**Response**: List of all categories with chapter counts

**Example**:
```
GET https://localhost:5001/Categories
```

---

### Get Category Details
```
GET /Categories/Details/{id}
```

**Parameters**:
- `id` (int): Category ID

**Response**: Category with all chapters, topics, Q&A, and PDFs

**Example**:
```
GET https://localhost:5001/Categories/Details/1
```

---

### Create Category (Form)
```
GET /Categories/Create
```

**Response**: HTML form for creating new category

---

### Create Category (Submit)
```
POST /Categories/Create
```

**Body**: Form data
```
{
  "name": "Azure Fundamentals",
  "description": "Learn Azure basics"
}
```

**Response**: Redirect to Categories/Index

---

### Edit Category (Form)
```
GET /Categories/Edit/{id}
```

**Parameters**:
- `id` (int): Category ID

**Response**: HTML form pre-populated with category data

---

### Edit Category (Submit)
```
POST /Categories/Edit/{id}
```

**Parameters**:
- `id` (int): Category ID

**Body**: Form data
```
{
  "id": 1,
  "name": "Azure Fundamentals",
  "description": "Updated description",
  "createdDate": "2024-01-15"
}
```

**Response**: Redirect to Categories/Index

---

### Delete Category (Confirmation)
```
GET /Categories/Delete/{id}
```

**Parameters**:
- `id` (int): Category ID

**Response**: HTML confirmation page

---

### Delete Category (Confirm)
```
POST /Categories/Delete/{id}
```

**Parameters**:
- `id` (int): Category ID

**Response**: Redirect to Categories/Index

---

## 2. Chapters Controller

### List All Chapters
```
GET /Chapters
```

**Query Parameters**:
- `categoryId` (int, optional): Filter by category

**Response**: List of chapters

**Examples**:
```
GET https://localhost:5001/Chapters
GET https://localhost:5001/Chapters?categoryId=1
```

---

### Get Chapter Details
```
GET /Chapters/Details/{id}
```

**Parameters**:
- `id` (int): Chapter ID

**Response**: Chapter with all topics, Q&A, and PDFs

**Example**:
```
GET https://localhost:5001/Chapters/Details/1
```

---

### Create Chapter (Form)
```
GET /Chapters/Create
```

**Response**: HTML form with category dropdown

---

### Create Chapter (Submit)
```
POST /Chapters/Create
```

**Body**: Form data
```
{
  "categoryId": 1,
  "title": "Introduction to Azure",
  "description": "Day 1 chapter",
  "dayNumber": 1
}
```

**Response**: Redirect to Chapters/Index

---

### Edit Chapter (Form)
```
GET /Chapters/Edit/{id}
```

**Parameters**:
- `id` (int): Chapter ID

---

### Edit Chapter (Submit)
```
POST /Chapters/Edit/{id}
```

**Body**: Form data
```
{
  "id": 1,
  "categoryId": 1,
  "title": "Updated Title",
  "description": "Updated description",
  "dayNumber": 1,
  "createdDate": "2024-01-15"
}
```

---

### Delete Chapter (Confirmation)
```
GET /Chapters/Delete/{id}
```

---

### Delete Chapter (Confirm)
```
POST /Chapters/Delete/{id}
```

---

## 3. Topics Controller

### List All Topics
```
GET /Topics
```

**Query Parameters**:
- `chapterId` (int, optional): Filter by chapter

**Response**: List of topics (sorted by sequence)

**Examples**:
```
GET https://localhost:5001/Topics
GET https://localhost:5001/Topics?chapterId=1
```

---

### Get Topic Details
```
GET /Topics/Details/{id}
```

**Parameters**:
- `id` (int): Topic ID

**Response**: Topic details with content

---

### Create Topic (Form)
```
GET /Topics/Create
```

**Query Parameters**:
- `chapterId` (int, optional): Pre-select chapter

**Response**: HTML form

---

### Create Topic (Submit)
```
POST /Topics/Create
```

**Body**: Form data
```
{
  "chapterId": 1,
  "title": "What is Cloud Computing?",
  "content": "Cloud computing is...",
  "sequenceNumber": 1
}
```

---

### Edit Topic (Form)
```
GET /Topics/Edit/{id}
```

---

### Edit Topic (Submit)
```
POST /Topics/Edit/{id}
```

**Body**: Form data
```
{
  "id": 1,
  "chapterId": 1,
  "title": "Updated Title",
  "content": "Updated content",
  "sequenceNumber": 1,
  "createdDate": "2024-01-15"
}
```

---

### Delete Topic (Confirmation)
```
GET /Topics/Delete/{id}
```

---

### Delete Topic (Confirm)
```
POST /Topics/Delete/{id}
```

**Response**: Redirect to Topics/Index with chapterId parameter

---

## 4. Question Answers Controller

### List All Q&A
```
GET /QuestionAnswers
```

**Query Parameters**:
- `chapterId` (int, optional): Filter by chapter

**Response**: List of Q&A pairs (sorted by sequence)

**Examples**:
```
GET https://localhost:5001/QuestionAnswers
GET https://localhost:5001/QuestionAnswers?chapterId=1
```

---

### Get Q&A Details
```
GET /QuestionAnswers/Details/{id}
```

**Parameters**:
- `id` (int): Question ID

**Response**: Full Q&A with chapter info

---

### Create Q&A (Form)
```
GET /QuestionAnswers/Create
```

**Query Parameters**:
- `chapterId` (int, optional): Pre-select chapter

**Response**: HTML form

---

### Create Q&A (Submit)
```
POST /QuestionAnswers/Create
```

**Body**: Form data
```
{
  "chapterId": 1,
  "question": "What is IaaS?",
  "answer": "Infrastructure as a Service is...",
  "sequenceNumber": 1
}
```

---

### Edit Q&A (Form)
```
GET /QuestionAnswers/Edit/{id}
```

---

### Edit Q&A (Submit)
```
POST /QuestionAnswers/Edit/{id}
```

**Body**: Form data
```
{
  "id": 1,
  "chapterId": 1,
  "question": "Updated question?",
  "answer": "Updated answer",
  "sequenceNumber": 1,
  "createdDate": "2024-01-15"
}
```

---

### Delete Q&A (Confirmation)
```
GET /QuestionAnswers/Delete/{id}
```

---

### Delete Q&A (Confirm)
```
POST /QuestionAnswers/Delete/{id}
```

---

## 5. PDF Attachments Controller

### List All PDFs
```
GET /PdfAttachments
```

**Query Parameters**:
- `chapterId` (int, optional): Filter by chapter

**Response**: List of PDF attachments

**Examples**:
```
GET https://localhost:5001/PdfAttachments
GET https://localhost:5001/PdfAttachments?chapterId=1
```

---

### Get PDF Details
```
GET /PdfAttachments/Details/{id}
```

**Parameters**:
- `id` (int): PDF ID

**Response**: PDF metadata and download link

---

### Upload PDF (Form)
```
GET /PdfAttachments/Create
```

**Query Parameters**:
- `chapterId` (int, optional): Pre-select chapter

**Response**: HTML form with file input

---

### Upload PDF (Submit)
```
POST /PdfAttachments/Create
```

**Parameters**:
- `chapterId` (int): Chapter ID (from form)

**Body**: Multipart form data with file
```
chapterId: 1
file: <PDF binary data>
```

**Validation**:
- File must be PDF format
- Maximum size: 10MB (configurable)
- File stored in: `/uploads/pdfs/`

**Response**: Redirect to PdfAttachments/Index

---

### Delete PDF (Confirmation)
```
GET /PdfAttachments/Delete/{id}
```

---

### Delete PDF (Confirm)
```
POST /PdfAttachments/Delete/{id}
```

**Behavior**:
- Deletes database record
- Deletes physical file from `/uploads/pdfs/`
- Returns redirect to PdfAttachments/Index

---

## Request/Response Examples

### Example 1: Create a Complete Learning Path

**Step 1**: Create Category
```
POST /Categories/Create
{
  "name": "Azure Certification",
  "description": "Prepare for Azure certifications"
}
```

**Step 2**: Create Chapter
```
POST /Chapters/Create
{
  "categoryId": 1,
  "title": "Fundamentals",
  "description": "Core Azure concepts",
  "dayNumber": 1
}
```

**Step 3**: Add Topic
```
POST /Topics/Create
{
  "chapterId": 1,
  "title": "Azure Services Overview",
  "content": "Azure provides a wide range of services...",
  "sequenceNumber": 1
}
```

**Step 4**: Add Q&A
```
POST /QuestionAnswers/Create
{
  "chapterId": 1,
  "question": "Name 5 Azure services",
  "answer": "Virtual Machines, App Service, SQL Database...",
  "sequenceNumber": 1
}
```

**Step 5**: Upload PDF
```
POST /PdfAttachments/Create
chapterId: 1
file: azure-services.pdf
```

---

### Example 2: Browse Learning Content

**Get all categories**:
```
GET /Categories
```

**Get category details**:
```
GET /Categories/Details/1
```

**Get chapter details**:
```
GET /Chapters/Details/1
```

**Get topics for chapter**:
```
GET /Topics?chapterId=1
```

**Get Q&A for chapter**:
```
GET /QuestionAnswers?chapterId=1
```

**Get PDFs for chapter**:
```
GET /PdfAttachments?chapterId=1
```

---

## Response Status Codes

| Code | Meaning |
|------|---------|
| 200 | Success - GET request |
| 302 | Redirect - After POST success |
| 404 | Not Found - Invalid ID |
| 400 | Bad Request - Invalid data |

---

## Error Handling

### Validation Errors
If form validation fails, the form page is re-rendered with error messages displayed.

### File Upload Errors
```
Error Messages:
- "File not selected" - No file chosen
- "Only PDF files are allowed" - Wrong file type
- "File size exceeds limit" - File too large
- "Error uploading file: {message}" - Server error
```

### Database Errors
If a database operation fails, the page returns 500 error with details.

---

## Data Constraints

### Category
- `Name`: Required, max 255 characters
- `Description`: Optional, max 1000 characters

### Chapter
- `CategoryId`: Required (foreign key)
- `Title`: Required, max 255 characters
- `Description`: Optional, max 1000 characters
- `DayNumber`: Required, positive integer

### Topic
- `ChapterId`: Required (foreign key)
- `Title`: Required, max 255 characters
- `Content`: Required, text
- `SequenceNumber`: Required, positive integer

### QuestionAnswer
- `ChapterId`: Required (foreign key)
- `Question`: Required, max 1000 characters
- `Answer`: Required, text
- `SequenceNumber`: Required, positive integer

### PdfAttachment
- `ChapterId`: Required (foreign key)
- `FileName`: Required, max 255 characters
- `FileUrl`: Required, max 255 characters
- `FileSizeInBytes`: Required, positive integer

---

## Form Data Format

All POST requests use `application/x-www-form-urlencoded` format except:
- **File uploads**: Use `multipart/form-data`

---

## Security Considerations

? **Implemented**:
- CSRF protection (AntiForgeryToken)
- SQL injection prevention (Entity Framework)
- File type validation
- File size limits

?? **Recommendations**:
- Add authentication/authorization
- Implement user roles (Admin, Instructor, Student)
- Add audit logging
- Encrypt sensitive data
- Use HTTPS in production

---

For more information, see [README.md](README.md) and [QUICKSTART.md](QUICKSTART.md).
