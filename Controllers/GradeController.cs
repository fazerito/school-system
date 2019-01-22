using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class GradeController : Controller
    {
        private readonly SchoolDbContext _context;

        public GradeController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Grade
        public async Task<IActionResult> Index()
        {
            var schoolDbContext = _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Student.PersonalData)
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .Include(g => g.Teacher.PersonalData);
            return View(await schoolDbContext.ToListAsync());
        }

        // GET: Grade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grades = await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grades == null)
            {
                return NotFound();
            }

            return View(grades);
        }

        // GET: Grade/Create
        public IActionResult Create()
        {
            var studentName = _context.Students
                .Include(s => s.PersonalData)
                .Select(s => s.PersonalData.FullName)
                .FirstOrDefault();

            var teacherName = _context.Teachers
                .Include(s => s.PersonalData)
                .Select(s => s.PersonalData.FullName)
                .FirstOrDefault();

            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", studentName);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", teacherName);
            return View();
        }

        // POST: Grade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeId,Value,Note,Date,SubjectId,TeacherId,StudentId")] Grades grades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            

            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "PersonalData.FullName", grades.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", grades.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", grades.TeacherId);
            return View(grades);
        }

        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grades = await _context.Grades.FindAsync(id);
            if (grades == null)
            {
                return NotFound();
            }

            var students = _context.Students
                .Include(s => s.PersonalData)
                .Include(s => s.Class.TeacherId)
                .Include(s => s.Class.Teacher.PersonalData);

            var teachers = _context.Teachers
                .Include(s => s.PersonalData);

            ViewData["StudentId"] = new SelectList(students, "StudentId", "FullName", grades.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", grades.SubjectId);
            ViewData["TeacherId"] = new SelectList(teachers, "TeacherId", "FullName", grades.TeacherId);
            return View(grades);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeId,Value,Note,Date,SubjectId,TeacherId,StudentId")] Grades grades)
        {
            if (id != grades.GradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradesExists(grades.GradeId))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", grades.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", grades.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", grades.TeacherId);
            return View(grades);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grades = await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grades == null)
            {
                return NotFound();
            }

            return View(grades);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grades = await _context.Grades.FindAsync(id);
            _context.Grades.Remove(grades);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradesExists(int id)
        {
            return _context.Grades.Any(e => e.GradeId == id);
        }
    }
}
