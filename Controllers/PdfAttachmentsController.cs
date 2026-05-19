using AzureLearningDocker.Data;
using AzureLearningDocker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AzureLearningDocker.Controllers
{
    public class PdfAttachmentsController : Controller
    {
        private readonly LearningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PdfAttachmentsController(LearningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: PdfAttachments
        public async Task<IActionResult> Index(int? chapterId)
        {
            IQueryable<PdfAttachment> pdfAttachments = _context.PdfAttachments
                .Include(pa => pa.Chapter);

            if (chapterId.HasValue)
            {
                pdfAttachments = pdfAttachments.Where(pa => pa.ChapterId == chapterId);
            }

            return View(await pdfAttachments.ToListAsync());
        }

        // GET: PdfAttachments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pdfAttachment = await _context.PdfAttachments
                .Include(pa => pa.Chapter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pdfAttachment == null)
            {
                return NotFound();
            }

            return View(pdfAttachment);
        }

        // GET: PdfAttachments/Create
        public IActionResult Create(int? chapterId)
        {
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", chapterId);
            return View();
        }

        // POST: PdfAttachments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int chapterId, IFormFile? file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "Please select a file to upload.");
                ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", chapterId);
                return View();
            }

            if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("file", "Only PDF files are allowed.");
                ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", chapterId);
                return View();
            }

            try
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "pdfs");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var pdfAttachment = new PdfAttachment
                {
                    ChapterId = chapterId,
                    FileName = file.FileName,
                    FileUrl = $"/uploads/pdfs/{uniqueFileName}",
                    FileSizeInBytes = file.Length,
                    CreatedDate = DateTime.Now
                };

                _context.Add(pdfAttachment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { chapterId = chapterId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("file", $"Error uploading file: {ex.Message}");
                ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", chapterId);
                return View();
            }
        }

        // GET: PdfAttachments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pdfAttachment = await _context.PdfAttachments
                .Include(pa => pa.Chapter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pdfAttachment == null)
            {
                return NotFound();
            }

            return View(pdfAttachment);
        }

        // POST: PdfAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pdfAttachment = await _context.PdfAttachments.FindAsync(id);
            if (pdfAttachment != null)
            {
                var chapterId = pdfAttachment.ChapterId;

                // Delete the physical file
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, pdfAttachment.FileUrl.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.PdfAttachments.Remove(pdfAttachment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { chapterId = chapterId });
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PdfAttachmentExists(int id)
        {
            return _context.PdfAttachments.Any(e => e.Id == id);
        }
    }
}
