using AzureLearningDocker.Data;
using AzureLearningDocker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AzureLearningDocker.Controllers
{
    public class TopicsController : Controller
    {
        private readonly LearningContext _context;

        public TopicsController(LearningContext context)
        {
            _context = context;
        }

        // GET: Topics
        public async Task<IActionResult> Index(int? chapterId)
        {
            IQueryable<Topic> topics = _context.Topics
                .Include(t => t.Chapter);

            if (chapterId.HasValue)
            {
                topics = topics.Where(t => t.ChapterId == chapterId);
            }

            return View(await topics.OrderBy(t => t.SequenceNumber).ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.Chapter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Topics/Create
        public IActionResult Create(int? chapterId)
        {
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", chapterId);
            return View();
        }

        // POST: Topics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChapterId,Title,Content,SequenceNumber")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { chapterId = topic.ChapterId });
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", topic.ChapterId);
            return View(topic);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", topic.ChapterId);
            return View(topic);
        }

        // POST: Topics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChapterId,Title,Content,SequenceNumber,CreatedDate")] Topic topic)
        {
            if (id != topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { chapterId = topic.ChapterId });
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", topic.ChapterId);
            return View(topic);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.Chapter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                var chapterId = topic.ChapterId;
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { chapterId = chapterId });
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }
    }
}
