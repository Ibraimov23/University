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
    public class WebinarsController : Controller
    {
        private readonly SchoolContext _context;

        public WebinarsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Webinars
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Webinar.Include(w => w.Course);
            return View(await schoolContext.ToListAsync());
        }

        // GET: Webinars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webinar = await _context.Webinar
                .Include(w => w.Course)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (webinar == null)
            {
                return NotFound();
            }

            return View(webinar);
        }

        // GET: Webinars/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title");
            return View();
        }

        // POST: Webinars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,Title,Description,StartDateTime,CourseID")] Webinar webinar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webinar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", webinar.CourseID);
            return View(webinar);
        }

        // GET: Webinars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webinar = await _context.Webinar.FindAsync(id);
            if (webinar == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", webinar.CourseID);
            return View(webinar);
        }

        // POST: Webinars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,Title,Description,StartDateTime,CourseID")] Webinar webinar)
        {
            if (id != webinar.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webinar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebinarExists(webinar.EventID))
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
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", webinar.CourseID);
            return View(webinar);
        }

        // GET: Webinars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webinar = await _context.Webinar
                .Include(w => w.Course)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (webinar == null)
            {
                return NotFound();
            }

            return View(webinar);
        }

        // POST: Webinars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webinar = await _context.Webinar.FindAsync(id);
            _context.Webinar.Remove(webinar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebinarExists(int id)
        {
            return _context.Webinar.Any(e => e.EventID == id);
        }
    }
}
