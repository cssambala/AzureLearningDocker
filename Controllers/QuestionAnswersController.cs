using AzureLearningDocker.Data;
using AzureLearningDocker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AzureLearningDocker.Controllers
{
    public class QuestionAnswersController : Controller
    {
        private readonly LearningContext _context;

        public QuestionAnswersController(LearningContext context)
        {
            _context = context;
        }

        // GET: QuestionAnswers
        public async Task<IActionResult> Index(int? chapterId)
        {
            IQueryable<QuestionAnswer> questionAnswers = _context.QuestionAnswers
                .Include(qa => qa.Chapter);

            if (chapterId.HasValue)
            {
                questionAnswers = questionAnswers.Where(qa => qa.ChapterId == chapterId);
            }

            return View(await questionAnswers.OrderBy(qa => qa.SequenceNumber).ToListAsync());
        }

        // GET: QuestionAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _context.QuestionAnswers
                .Include(qa => qa.Chapter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            return View(questionAnswer);
        }

        // GET: QuestionAnswers/Create
        public IActionResult Create(int? chapterId)
        {
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", chapterId);
            return View();
        }

        // POST: QuestionAnswers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChapterId,Question,Answer,SequenceNumber")] QuestionAnswer questionAnswer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { chapterId = questionAnswer.ChapterId });
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", questionAnswer.ChapterId);
            return View(questionAnswer);
        }

        // GET: QuestionAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _context.QuestionAnswers.FindAsync(id);
            if (questionAnswer == null)
            {
                return NotFound();
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", questionAnswer.ChapterId);
            return View(questionAnswer);
        }

        // POST: QuestionAnswers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChapterId,Question,Answer,SequenceNumber,CreatedDate")] QuestionAnswer questionAnswer)
        {
            if (id != questionAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionAnswerExists(questionAnswer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { chapterId = questionAnswer.ChapterId });
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Title", questionAnswer.ChapterId);
            return View(questionAnswer);
        }

        // GET: QuestionAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _context.QuestionAnswers
                .Include(qa => qa.Chapter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            return View(questionAnswer);
        }

        // POST: QuestionAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionAnswer = await _context.QuestionAnswers.FindAsync(id);
            if (questionAnswer != null)
            {
                var chapterId = questionAnswer.ChapterId;
                _context.QuestionAnswers.Remove(questionAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { chapterId = chapterId });
            }
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionAnswerExists(int id)
        {
            return _context.QuestionAnswers.Any(e => e.Id == id);
        }
    }
}
