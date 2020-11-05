using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models.Events;

namespace University.Controllers.EventControllers
{
    public class LabWorksController : Controller
    {
        private readonly SchoolContext _context;

        public LabWorksController(SchoolContext context)
        {
            _context = context;
        }

        // GET: LabWorks
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.LabWork.Include(l => l.Course);
            return View(await schoolContext.ToListAsync());
        }

        // GET: LabWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labWork = await _context.LabWork
                .Include(l => l.Course)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (labWork == null)
            {
                return NotFound();
            }

            return View(labWork);
        }

        // GET: LabWorks/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title");
            return View();
        }

        // POST: LabWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,Title,Description,StartDateTime,CourseID")] LabWork labWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", labWork.CourseID);
            return View(labWork);
        }

        // GET: LabWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labWork = await _context.LabWork.FindAsync(id);
            if (labWork == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", labWork.CourseID);
            return View(labWork);
        }

        // POST: LabWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,Title,Description,StartDateTime,CourseID")] LabWork labWork)
        {
            if (id != labWork.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabWorkExists(labWork.EventID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", labWork.CourseID);
            return View(labWork);
        }

        // GET: LabWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labWork = await _context.LabWork
                .Include(l => l.Course)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (labWork == null)
            {
                return NotFound();
            }

            return View(labWork);
        }

        // POST: LabWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labWork = await _context.LabWork.FindAsync(id);
            _context.LabWork.Remove(labWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabWorkExists(int id)
        {
            return _context.LabWork.Any(e => e.EventID == id);
        }
    }
}
