using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VGCManagement.DOMAIN;
using VGCManagement.VMC.Data;

namespace VGCManagement.VMC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseEnrolmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseEnrolmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseEnrolments
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CourseEnrolments.Include(c => c.Course).Include(c => c.StudentProfile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CourseEnrolments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEnrolment = await _context.CourseEnrolments
                .Include(c => c.Course)
                .Include(c => c.StudentProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseEnrolment == null)
            {
                return NotFound();
            }

            return View(courseEnrolment);
        }

        // GET: CourseEnrolments/Create
        [Authorize(Roles = "Admin, Faculty")]
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "StudentNumber");
            return View();
        }

        // POST: CourseEnrolments/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentProfileId,CourseId,EnrolDate,Status")] CourseEnrolment courseEnrolment)
        {
            ModelState.Remove("StudentProfile");
            ModelState.Remove("Course");

            if (ModelState.IsValid)
            {
                _context.Add(courseEnrolment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseEnrolment.CourseId);
            ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "StudentNumber", courseEnrolment.StudentProfileId);
            return View(courseEnrolment);
        }

        // GET: CourseEnrolments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEnrolment = await _context.CourseEnrolments.FindAsync(id);
            if (courseEnrolment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseEnrolment.CourseId);
            ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Address", courseEnrolment.StudentProfileId);
            return View(courseEnrolment);
        }

        // POST: CourseEnrolments/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentProfileId,CourseId,EnrolDate,Status")] CourseEnrolment courseEnrolment)
        {
            if (id != courseEnrolment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseEnrolment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseEnrolmentExists(courseEnrolment.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseEnrolment.CourseId);
            ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Address", courseEnrolment.StudentProfileId);
            return View(courseEnrolment);
        }

        // GET: CourseEnrolments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEnrolment = await _context.CourseEnrolments
                .Include(c => c.Course)
                .Include(c => c.StudentProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseEnrolment == null)
            {
                return NotFound();
            }

            return View(courseEnrolment);
        }

        // POST: CourseEnrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseEnrolment = await _context.CourseEnrolments.FindAsync(id);
            if (courseEnrolment != null)
            {
                _context.CourseEnrolments.Remove(courseEnrolment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseEnrolmentExists(int id)
        {
            return _context.CourseEnrolments.Any(e => e.Id == id);
        }
    }
}
