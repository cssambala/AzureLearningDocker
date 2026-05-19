# Troubleshooting Guide

This guide helps you resolve common issues with the Online Learning App.

---

## Database Issues

### Issue 1: "Cannot connect to database"

**Symptoms**:
- Error on startup
- "A network-related or instance-specific error occurred"
- Connection timeout

**Solutions**:

1. **Verify LocalDB is running**:
   ```bash
   sqllocaldb start
   sqllocaldb info
   ```

2. **Check connection string** in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AzureLearningDb;Trusted_Connection=true;"
   }
   ```

3. **Verify SQL Server is installed**:
   ```bash
   sqllocaldb versions
   ```

4. **For SQL Server (full version)**:
   ```json
   "Server=YOUR_SERVER_NAME;Database=AzureLearningDb;Trusted_Connection=true;"
   ```

5. **Restart Visual Studio** and try again

---

### Issue 2: "There is already an object named 'Categories'"

**Symptoms**:
- Migration error during update-database
- Table already exists

**Solutions**:

1. **Drop existing database**:
   ```powershell
   Drop-Database -Force
   ```

2. **Or delete the database manually**:
   - Open SQL Server Management Studio
   - Find `AzureLearningDb`
   - Right-click ? Delete

3. **Re-run migrations**:
   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

---

### Issue 3: "Migration 'InitialCreate' has already been applied"

**Symptoms**:
- Error when running Update-Database twice

**Solutions**:

```powershell
# Option 1: Remove the migration
Remove-Migration

# Option 2: Create a new migration for changes
Add-Migration AddNewChanges
Update-Database

# Option 3: Use -Force to rerun
Update-Database -Force
```

---

### Issue 4: "No migrations found"

**Symptoms**:
- Error: "No migration was found"
- Migrations folder missing

**Solutions**:

```powershell
# Create initial migration
Add-Migration InitialCreate -Context LearningContext
Update-Database
```

---

## Application Issues

### Issue 5: Application won't start

**Symptoms**:
- Visual Studio shows errors
- Build fails

**Solutions**:

1. **Clean and rebuild**:
   ```bash
   dotnet clean
   dotnet build
   ```

2. **Restore NuGet packages**:
   ```bash
   dotnet restore
   ```

3. **Check for missing files**:
   - Verify all files exist in the project
   - Check Models/ folder
   - Check Controllers/ folder
   - Check Views/ folder

4. **Check Program.cs syntax**:
   - Verify no typos
   - Check using statements

---

### Issue 6: "The name 'Request' does not exist in the current context"

**Symptoms**:
- Razor view compilation error
- `@Request` not recognized

**Solutions**:

This is already fixed in the project, but if you encounter it:

1. **Use the injected context** (already in views)
2. **Or access via dependency injection** in controller
3. **Check View**: Ensure `@` syntax is correct

---

### Issue 7: Page shows 404 Not Found

**Symptoms**:
- Navigate to a page, get 404 error
- Controller action not found

**Solutions**:

1. **Verify URL spelling**:
   - `/Categories` (capital C)
   - `/Chapters` (capital C)
   - Check exact spelling

2. **Verify controller exists**:
   - File in `Controllers/` folder
   - Ends with `Controller`
   - Inherits from `Controller`

3. **Verify action method exists**:
   - Method name matches URL
   - Method is public
   - Has correct HTTP method (GET/POST)

4. **Clean browser cache**:
   - Hard refresh: Ctrl+Shift+R (Windows) or Cmd+Shift+R (Mac)
   - Clear cookies for localhost

---

### Issue 8: "No such file or directory"

**Symptoms**:
- Error when uploading PDFs
- File not found after upload

**Solutions**:

1. **Create upload folder manually**:
   ```bash
   mkdir wwwroot\uploads\pdfs
   ```

2. **Verify folder permissions**:
   - Right-click folder ? Properties
   - Ensure full control permissions
   - For IIS: Add permissions for IIS user

3. **Check appsettings.json path**:
   - Path must be correct
   - Use forward slashes: `/uploads/pdfs`

---

### Issue 9: PDF upload fails

**Symptoms**:
- "Error uploading file"
- Upload button doesn't work
- File not saved

**Solutions**:

1. **Verify file is PDF**:
   - File must have `.pdf` extension
   - Real PDF file, not renamed
   - Not corrupted

2. **Check file size**:
   - Max 10MB recommended
   - In bytes: 10,485,760
   - Edit limit in controller if needed

3. **Verify upload directory exists**:
   ```bash
   ls wwwroot/uploads/pdfs
   ```

4. **Check folder permissions**:
   ```bash
   # Windows
   icacls "wwwroot\uploads\pdfs" /grant Users:F

   # Linux/Mac
   chmod 755 wwwroot/uploads/pdfs
   ```

5. **Check browser console** for JavaScript errors:
   - Press F12 in browser
   - Go to Console tab
   - Look for error messages

---

### Issue 10: PDF download doesn't work

**Symptoms**:
- Download button does nothing
- 404 error when clicking download

**Solutions**:

1. **Verify file path** in database:
   - `/uploads/pdfs/filename.pdf` format
   - No backslashes
   - Correct case

2. **Verify physical file exists**:
   ```bash
   ls wwwroot/uploads/pdfs/
   # or
   dir wwwroot\uploads\pdfs\
   ```

3. **Check static file middleware**:
   - In `Program.cs`, verify `app.UseStaticFiles();` exists
   - Check that before `app.UseRouting();`

4. **Clear browser cache** and try again

---

### Issue 11: Styling looks broken

**Symptoms**:
- Bootstrap CSS not loading
- Buttons look plain
- Layout is misaligned

**Solutions**:

1. **Hard refresh browser**:
   - Ctrl+Shift+R (Windows)
   - Cmd+Shift+R (Mac)

2. **Clear browser cache**:
   - Open DevTools (F12)
   - Right-click refresh button
   - Select "Empty cache and hard refresh"

3. **Verify Bootstrap is loaded**:
   - Press F12 in browser
   - Go to Network tab
   - Reload page
   - Look for bootstrap.min.css
   - Should show 200 status

4. **Check _Layout.cshtml**:
   ```html
   <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
   ```

---

## NuGet Package Issues

### Issue 12: NuGet packages not restoring

**Symptoms**:
- Build fails
- Red squiggly lines under `using` statements
- Packages missing

**Solutions**:

1. **Restore packages**:
   ```bash
   dotnet restore
   ```

2. **Rebuild solution**:
   ```bash
   dotnet clean
   dotnet build
   ```

3. **In Visual Studio**:
   - Right-click Solution ? Restore NuGet Packages
   - Or: Tools ? NuGet Package Manager ? Manage Packages for Solution

4. **Check project file**:
   - Verify `AzureLearningDocker.csproj` has correct content
   - Verify PackageReference entries

---

### Issue 13: "Package X is incompatible with framework Y"

**Symptoms**:
- Version mismatch error
- Build fails with package error

**Solutions**:

1. **Check target framework**:
   - Should be `net8.0`
   - In `AzureLearningDocker.csproj`

2. **Update package**:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
   ```

3. **Or in Package Manager Console**:
   ```powershell
   Install-Package Microsoft.EntityFrameworkCore -Version 8.0.0
   ```

---

## Form/Validation Issues

### Issue 14: Form validation not working

**Symptoms**:
- Required fields not enforced
- Can submit empty forms
- No error messages

**Solutions**:

1. **Verify HTML attributes**:
   ```html
   <input asp-for="Name" class="form-control" />
   ```

2. **Check model properties**:
   ```csharp
   public string Name { get; set; } = string.Empty;
   ```

3. **Verify validation script is loaded**:
   ```html
   @await Html.RenderPartialAsync("_ValidationScriptsPartial")
   ```

---

### Issue 15: Form shows error but won't submit

**Symptoms**:
- Red error messages appear
- Submit button doesn't work
- Can't proceed

**Solutions**:

1. **Check error messages**:
   - Follow instructions in error messages
   - Fill required fields
   - Correct invalid data

2. **Verify form method**:
   ```html
   <form asp-action="Create" method="post">
   ```

3. **Clear browser cache** and reload

---

## Performance Issues

### Issue 16: Application is slow

**Symptoms**:
- Pages take long to load
- List views are sluggish
- Database queries seem slow

**Solutions**:

1. **Verify database connection**:
   - No network latency issues
   - Check query performance
   - Verify indexes

2. **Optimize queries**:
   - Use `.AsNoTracking()` for read-only queries
   - Eager load related data with `.Include()`
   - Avoid N+1 queries

3. **Check Event Viewer for SQL Server**:
   - Look for performance warnings

---

## Data Issues

### Issue 17: Data appears but won't delete

**Symptoms**:
- Delete button works
- No error message
- Data still there

**Solutions**:

1. **Check cascade delete settings**:
   - Model relationships configured
   - Database constraints set

2. **Verify foreign key relationships**:
   - Can't delete category if chapters exist (by design)
   - Delete chapters first

3. **Try manual deletion**:
   - SQL Server Management Studio
   - Delete test data

---

### Issue 18: Deleted data reappears

**Symptoms**:
- Delete and refresh
- Item still there
- Delete doesn't work

**Solutions**:

1. **Verify SaveChanges() called**:
   - After delete, must call `await _context.SaveChangesAsync();`
   - Check controller code

2. **Check transaction state**:
   - Verify EF Core tracked changes

3. **Refresh database**:
   - Run migrations again
   - Reset data

---

## Authentication/Authorization Issues

### Issue 19: User can access restricted content

**Symptoms**:
- Can view content shouldn't access
- No login required

**Solutions**:

Currently, the app doesn't have authentication. To add it:

1. **Add authentication middleware** in `Program.cs`
2. **Add authorization attributes** to controllers
3. **Implement login/register** views
4. **See documentation** for security best practices

---

## File Issues

### Issue 20: PDF file corrupted after upload

**Symptoms**:
- Can download file
- File can't be opened
- File appears corrupted

**Solutions**:

1. **Test with different PDF**:
   - Try a different PDF file
   - Small test file first

2. **Check file size**:
   - Verify upload size matches download size
   - Compare with original

3. **Verify upload process**:
   - Look at file in `wwwroot/uploads/pdfs/`
   - Try opening directly

4. **Check file permissions**:
   - Ensure read/write permissions
   - File not locked by another process

---

## Getting Help

### Before Contacting Support:

1. ? Check this troubleshooting guide
2. ? Check documentation files (README.md, etc.)
3. ? Check browser console (F12)
4. ? Check Visual Studio Output window
5. ? Check Event Viewer (Windows)

### Provide Information:

When reporting issues, include:
- Error message (exact text)
- Steps to reproduce
- What were you doing
- Expected vs actual result
- Screenshots if applicable

---

## Common Error Messages

| Error | Meaning | Solution |
|-------|---------|----------|
| 404 Not Found | Page doesn't exist | Check URL spelling |
| 500 Internal Error | Server error | Check logs, restart app |
| Connection timeout | Can't reach database | Start LocalDB, check connection string |
| Foreign key constraint | Related data exists | Delete related data first |
| File not found | PDF missing | Check file path, folder exists |
| Access denied | Permission error | Check folder permissions |

---

## Quick Reference

### Essential Commands

```bash
# Database
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef database drop

# Build & Run
dotnet clean
dotnet restore
dotnet build
dotnet run

# NuGet
dotnet add package PackageName
dotnet remove package PackageName
```

### File Locations

```
Models/          ? Data models
Controllers/     ? Business logic
Views/           ? User interface
Data/            ? Database context
wwwroot/         ? Static files
uploads/pdfs/    ? PDF storage
```

---

## Still Having Issues?

1. Review the [README.md](README.md)
2. Check [QUICKSTART.md](QUICKSTART.md)
3. Review [API_GUIDE.md](API_GUIDE.md)
4. Check ASP.NET Core documentation
5. Check Entity Framework Core documentation

---

**Last Updated**: 2024  
**Status**: Comprehensive Guide

Good luck! ??
