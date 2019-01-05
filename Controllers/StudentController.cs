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
    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Student
        [Route("students/get")]
        public IActionResult Index()
        {
            var students = _context.Students
                .Include(s => s.Class)
                .Include(s => s.PersonalData)
                .ToList();

            

            return View(students);
        }

        // GET: Student/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student =  _context.Students
                .Include(s => s.Class)
                .Include(s => s.Parent)
                .Include(s => s.PersonalData)
                .FirstOrDefault(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            var studentPersonal = _context.Students
                .Where(s => s.StudentId == id)
                .Select(s => s.PersonalData)
                .FirstOrDefault();

            var studentAddress = _context.Students
                .Where(s => s.StudentId == id)
                .Select(s => s.PersonalData.Address)
                .FirstOrDefault();


            student.PersonalData = studentPersonal;
            student.PersonalData.Address = studentAddress;

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name");
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId");
            ViewData["PersonalDataId"] = new SelectList(_context.PersonalDatas, "PersonalDataId", "FirstName");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,ParentId,ClassId,PersonalDataId")] Students students)
        {
            if (ModelState.IsValid)
            {
                _context.Add(students);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name", students.ClassId);
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", students.ParentId);
            ViewData["PersonalDataId"] = new SelectList(_context.PersonalDatas, "PersonalDataId", "FirstName", students.PersonalDataId);
            return View(students);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name", students.ClassId);
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", students.ParentId);
            ViewData["PersonalDataId"] = new SelectList(_context.PersonalDatas, "PersonalDataId", "FirstName", students.PersonalDataId);
            return View(students);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,ParentId,ClassId,PersonalDataId")] Students students)
        {
            if (id != students.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(students);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.StudentId))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name", students.ClassId);
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", students.ParentId);
            ViewData["PersonalDataId"] = new SelectList(_context.PersonalDatas, "PersonalDataId", "FirstName", students.PersonalDataId);
            return View(students);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.Parent)
                .Include(s => s.PersonalData)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var students = await _context.Students.FindAsync(id);
            _context.Students.Remove(students);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
