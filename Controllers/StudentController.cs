using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;
using SchoolProject.ViewModels;

namespace SchoolProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;
        private SignInManager<Users> _signManager;
        private UserManager<Users> _userManager;

        public StudentController(SchoolDbContext context, UserManager<Users> userManager, SignInManager<Users> signManager)
        {
            _context = context;
            _signManager = signManager;
            _userManager = userManager;
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

            var student = _context.Students
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

            var studentGrades = _context.Students
                .Where(s => s.StudentId == id)
                .SelectMany(s => s.Grades)
                .Where(s => s.StudentId == id)
                .Include(s => s.Subject)
                .OrderBy(s => s.Subject.Name)
                .ToList();
                //.Select(s => s.Grades)
                //.FirstOrDefault();

            var studentAttendances = _context.Students
                .Where(s => s.StudentId == id)
                .SelectMany(s => s.Attendances)
                .Where(s => s.StudentId == id)
                .Include(s => s.Lesson)
                .Include(s => s.Lesson.Subject)
                .ToList();
            
            student.PersonalData = studentPersonal;
            student.PersonalData.Address = studentAddress;
            student.Grades = studentGrades;
            student.Attendances = studentAttendances;

            return View(student);
        }

        // GET: Student/Create
        [Route("students/add")]
        public IActionResult Create()
        {
            var teacherId = _context.Users
                .Where(u => u.Login == User.Identity.Name)
                .Select(u => u.TeacherId)
                .FirstOrDefault();

            var principalId = _context.Users
                .Where(u => u.Login == User.Identity.Name)
                .Select(u => u.PrincipalId)
                .FirstOrDefault();

            if (teacherId == null && principalId == null)
            {
                TempData["message"] = "Brak wymaganych uprawnien.";
                return RedirectToAction("Index", "Home");
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name");
            return View();
            //ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId");
            //ViewData["PersonalDataId"] = new SelectList(_context.PersonalDatas, "PersonalDataId", "FirstName");
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("students/add")]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            var teacherId = _context.Users
                .Where(u => u.Login == User.Identity.Name)
                .Select(u => u.TeacherId)
                .FirstOrDefault();

            var principalId = _context.Users
                .Where(u => u.Login == User.Identity.Name)
                .Select(u => u.PrincipalId)
                .FirstOrDefault();

            if (teacherId == null && principalId == null)
            {
                TempData["message"] = "Brak wymaganych uprawnien.";
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            { 
                _context.Addresses.Add(model.Address);
                model.PersonalData.AddressId = model.Address.AddressId;
                _context.PersonalDatas.Add(model.PersonalData);
                _context.Students.Add(model.Student);
                model.Student.PersonalDataId = model.PersonalData.PersonalDataId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name", model.Classes.ClassId);
            return View(model);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = new StudentEditViewModel();

            model.Student = await _context.Students.FindAsync(id);
            if (model.Student == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name");

            return View(model);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentEditViewModel model)
        {
            if (id != model.Student.StudentId)
            {
                return NotFound();
            }

            model.Student = await _context.Students
                .Include(s => s.PersonalData)
                .Include(s => s.PersonalData.Address)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (model.AptNumber != null)
            {
                model.Student.PersonalData.Address.AptNumber = model.AptNumber;
            }
            if (model.City != null)
            {
                model.Student.PersonalData.Address.City = model.City;
            }
            if (model.StreetName != null)
            {
                model.Student.PersonalData.Address.StreetName = model.StreetName;
            }
            if (model.StreetNumber != 0)
            {
                model.Student.PersonalData.Address.StreetNumber = model.StreetNumber;
            }
            if (model.StreetNumber != 0)
            {
                model.Student.PersonalData.Address.StreetNumber = model.StreetNumber;
            }
            if (model.ZipCode != null)
            {
                model.Student.PersonalData.Address.ZipCode = model.ZipCode;
            }
            if (model.FirstName != null)
            {
                model.Student.PersonalData.FirstName = model.FirstName;
            }
            if (model.LastName != null)
            {
                model.Student.PersonalData.LastName = model.LastName;
            }
            if (model.Email != null)
            {
                model.Student.PersonalData.Email = model.Email;
            }
            if (model.Class != null)
            {
                model.Student.Class = model.Class;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.Student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(model.Student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name");

                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var _context = new SchoolDbContext())
            {
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
