# Quick Start Guide - Online Learning App

## Step-by-Step Setup

### 1. **Initialize the Database**

Open **Package Manager Console** in Visual Studio and run:

```powershell
Add-Migration InitialCreate
Update-Database
```

Or use the **dotnet CLI**:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 2. **Run the Application**

Press **F5** in Visual Studio or run:

```bash
dotnet run
```

The app will start at `https://localhost:5001`

---

## Application Navigation

### Main Menu Structure

```
?? Home
?? Categories      ? Create and manage learning categories
?? Chapters        ? Day-wise chapters within categories
?? Topics          ? Topics/lessons within chapters
? Q&A             ? Question & Answer pairs for chapters
?? Resources       ? PDF attachments for chapters
?? Privacy
```

---

## Creating Your First Learning Path

### Step 1: Create a Category

1. Click **Categories** in the menu
2. Click **Create New Category**
3. Enter:
   - **Name**: e.g., "Azure Fundamentals"
   - **Description**: e.g., "Learn the basics of Azure cloud platform"
4. Click **Save**

### Step 2: Create Chapters (Day-wise)

1. Click on the category you created
2. Click **Add New Chapter**
3. Fill in:
   - **Category**: (auto-selected)
   - **Day Number**: 1
   - **Title**: e.g., "Introduction to Azure"
   - **Description**: Chapter description
4. Click **Create**

### Step 3: Add Topics to a Chapter

1. View the chapter you created
2. Click **Add Topic** in the Topics section
3. Fill in:
   - **Sequence Number**: Order of topics (1, 2, 3...)
   - **Title**: e.g., "What is Cloud Computing?"
   - **Content**: Detailed content about the topic
4. Click **Create**

### Step 4: Add Question & Answers

1. In the same chapter, click **Add Q&A** in the Q&A section
2. Fill in:
   - **Sequence Number**: Order of Q&A (1, 2, 3...)
   - **Question**: e.g., "What are the main benefits of Azure?"
   - **Answer**: Detailed answer
3. Click **Create**

### Step 5: Upload PDF Resources

1. In the same chapter, click **Add PDF** in the Resources section
2. Click **Select PDF File** or drag-drop a PDF
3. Click **Upload**

---

## Data Model Explained

### Category
```
Example: "Azure for Beginners"
??? Multiple Chapters
??? Each Chapter = One Day
??? Full learning curriculum
```

### Chapter
```
Example: "Day 1 - Fundamentals"
??? Multiple Topics (Lessons)
??? Multiple Q&A Pairs (for revision)
??? Multiple PDF Resources (materials, references)
??? Organized by day number
```

### Topic
```
Example: "Introduction to Cloud Computing"
??? Detailed content
??? Learning material
??? Sequence numbering
??? Display only
```

### Question & Answer
```
Example: Q: "What is Azure?"
         A: "Azure is Microsoft's cloud platform..."
??? For learning reinforcement
??? Display only (not MCQ)
??? Multiple per chapter
??? Sequence numbering for order
```

### PDF Attachment
```
Example: "Azure_Overview.pdf"
??? Learning materials
??? Reference documents
??? Multiple per chapter
??? Downloadable resources
??? File metadata stored
```

---

## Common Tasks

### ?? Edit Content

1. Navigate to the item you want to edit
2. Click the **Edit** button
3. Make your changes
4. Click **Save Changes**

### ??? Delete Content

1. Navigate to the item you want to delete
2. Click the **Delete** button
3. Confirm the deletion
4. Click **Delete** again in the confirmation page

### ?? Download PDF

1. Find the PDF in the chapter
2. Click the **Download** button next to the PDF name
3. File will download to your default downloads folder

### ?? View Organization

1. Click **Categories** to see all learning paths
2. Click on a category to see all chapters
3. Click on a chapter to see all topics, Q&A, and PDFs
4. Click on topics/Q&A to view full details

---

## Tips & Best Practices

### ?? Organization Tips
- Use consistent naming conventions (e.g., "Day 1", "Day 2")
- Number topics and Q&A sequentially
- Organize PDFs by topic or chapter
- Use descriptive titles for all content

### ?? Content Tips
- Keep topics focused and concise
- Write clear, well-structured Q&A pairs
- Use questions that help reinforce key concepts
- Attach relevant PDFs for additional reading

### ?? Structure Recommendation
```
1 Category per subject or course
??? 7-30 Chapters (Day 1 through Day N)
??? 3-5 Topics per chapter
??? 5-10 Q&A per chapter
??? 1-3 PDF resources per chapter
```

---

## Troubleshooting

### Database Error?
```powershell
# Reset the database
Drop-Database
Add-Migration InitialCreate
Update-Database
```

### Can't Upload PDF?
- Ensure file is PDF format
- Check file size (< 10MB recommended)
- Verify folder permissions for `wwwroot/uploads/pdfs/`

### Can't See Changes?
- Refresh the page (Ctrl+F5 or Cmd+Shift+R)
- Clear browser cache
- Restart the application

### Missing Content?
- Check if chapter/topic is linked to correct category
- Verify sequence numbers are set correctly
- Ensure content was saved successfully

---

## Menu Navigation Reference

| Location | What You Can Do |
|----------|----------------|
| **Categories** | Create/Edit/Delete categories, View all chapters |
| **Category Details** | View chapters, Add new chapter, See statistics |
| **Chapters** | List all chapters, Filter by category |
| **Chapter Details** | View/Edit all chapter content, Add topics/Q&A/PDFs |
| **Topics** | List all topics, Edit individual topics |
| **Q&A** | View all Q&A pairs in accordion format |
| **Resources** | List all PDFs, Download, Delete |

---

## Sample Learning Structure

### Example: "Azure Fundamentals Course"

**Day 1: Cloud Computing Basics**
- Topics:
  - What is Cloud Computing?
  - Benefits of Cloud Computing
  - Cloud Service Models
- Q&A:
  - Q: What are the 3 service models?
  - A: IaaS, PaaS, SaaS
- Resources:
  - azure-basics.pdf
  - cloud-models.pdf

**Day 2: Azure Services**
- Topics:
  - Introduction to Azure
  - Core Azure Services
  - Azure Regions
- Q&A:
  - Q: Name 5 Azure services
  - A: Virtual Machines, App Service, SQL Database...
- Resources:
  - azure-services-overview.pdf

---

## Next Steps

1. ? Set up the database
2. ? Run the application
3. ? Create your first category
4. ? Add chapters for each day
5. ? Populate topics with content
6. ? Add Q&A for revision
7. ? Upload reference PDFs
8. ? Share the link with learners

---

**Need help?** Refer to the full [README.md](README.md) for detailed documentation.

Happy creating! ????
