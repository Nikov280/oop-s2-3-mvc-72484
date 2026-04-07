using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VGCManagement.DOMAIN;
using VGCManagement.VMC.Data;
using System.Security.Claims;


namespace VGCManagement.VMC.Controllers
{
    public class AttendanceRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
                
        public AttendanceRecordsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AttendanceRecords
        public async Task<IActionResult> Index()
        {
            
            var userEmail = User.Identity.Name?.Trim().ToLower();

            
            var query = _context.AttendanceRecords
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(e => e.Course)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(e => e.StudentProfile)
                .AsQueryable();

            
            if (User.IsInRole("Student"))
            {
                
                query = query.Where(a => a.CourseEnrolment.StudentProfile.Email.Contains("agustin@agustin.com"));
            }

            var results = await query.ToListAsync();
            return View(results);
        }

        // GET: AttendanceRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                        
            var attendanceRecord = await _context.AttendanceRecords
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(e => e.StudentProfile)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (attendanceRecord == null)
            {
                return NotFound();
            }

            return View(attendanceRecord);
        }

        // GET: AttendanceRecords/Create
        public async Task<IActionResult> Create()
        {
            
            var enrolments = await _context.CourseEnrolments
                .Include(e => e.StudentProfile)
                .Include(e => e.Course)
                .ToListAsync();

            var selectListItems = enrolments.Select(e => new
            {
                Id = e.Id,
                DisplayText = $"{e.StudentProfile.FullName} - {e.Course.Name}"
            }).ToList();

            
            ViewData["CourseEnrolmentId"] = new SelectList(selectListItems, "Id", "DisplayText");

            return View();
        }

        // POST: AttendanceRecords/Create
        [Authorize(Roles = "Admin,Faculty")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseEnrolmentId,WeekNumber,Present")] AttendanceRecord attendanceRecord)
        {
            
            ModelState.Remove("CourseEnrolment");

            if (ModelState.IsValid)
            {
                _context.Add(attendanceRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            var enrolments = await _context.CourseEnrolments
                .Include(e => e.StudentProfile)
                .Include(e => e.Course)
                .ToListAsync();

            var selectListItems = enrolments.Select(e => new
            {
                Id = e.Id,
                DisplayText = $"{e.StudentProfile.FullName} - {e.Course.Name}"
            }).ToList();

            ViewData["CourseEnrolmentId"] = new SelectList(selectListItems, "Id", "DisplayText", attendanceRecord.CourseEnrolmentId);

            return View(attendanceRecord);
        }

        // GET: AttendanceRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var attendanceRecord = await _context.AttendanceRecords
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(e => e.StudentProfile)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (attendanceRecord == null)
            {
                return NotFound();
            }

            
            ViewData["CourseEnrolmentId"] = new SelectList(_context.CourseEnrolments, "Id", "Id", attendanceRecord.CourseEnrolmentId);

            return View(attendanceRecord);
        }

        // POST: AttendanceRecords/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseEnrolmentId,WeekNumber,Present")] AttendanceRecord attendanceRecord)
        {
            if (id != attendanceRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendanceRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceRecordExists(attendanceRecord.Id))
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
            ViewData["CourseEnrolmentId"] = new SelectList(_context.CourseEnrolments, "Id", "Id", attendanceRecord.CourseEnrolmentId);
            return View(attendanceRecord);
        }

        // GET: AttendanceRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

             var attendanceRecord = await _context.AttendanceRecords
                .Include(a => a.CourseEnrolment).ThenInclude(e => e.StudentProfile)
                .Include(a => a.CourseEnrolment).ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceRecord == null)
            {
                return NotFound();
            }

            return View(attendanceRecord);
        }

        // POST: AttendanceRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendanceRecord = await _context.AttendanceRecords.FindAsync(id);
            if (attendanceRecord != null)
            {
                _context.AttendanceRecords.Remove(attendanceRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceRecordExists(int id)
        {
            return _context.AttendanceRecords.Any(e => e.Id == id);
        }
    }
}
