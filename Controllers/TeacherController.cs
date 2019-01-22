using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;
using SchoolProject.ViewModels;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        private SignInManager<Users> _signManager;
        private UserManager<Users> _userManager;

        public TeacherController(UserManager<Users> userManager, SignInManager<Users> signManager)
        {
            _signManager = signManager;
            _userManager = userManager;
        }

        [Route("teachers/get")]
        public IActionResult Get()
        {
            using (var context = new SchoolDbContext())
            {
                var teacherId = context.Users
                    .Where(u => u.Login == User.Identity.Name)
                    .Select(u => u.TeacherId)
                    .FirstOrDefault();

                var principalId = context.Users
                    .Where(u => u.Login == User.Identity.Name)
                    .Select(u => u.PrincipalId)
                    .FirstOrDefault();

                if (teacherId == null && principalId == null)
                {
                    TempData["message"] = "Brak wymaganych uprawnien.";
                    return RedirectToAction("Index", "Home");
                }

                var teachers = context.Teachers.Include(p => p.PersonalData).ToList();
                return View(teachers);
            }
        }

        [Route("teachers/add")]
        public IActionResult Create()
        {
            using (var context = new SchoolDbContext())
            {
                var teacherId = context.Users
                    .Where(u => u.Login == User.Identity.Name)
                    .Select(u => u.TeacherId)
                    .FirstOrDefault();

                var principalId = context.Users
                    .Where(u => u.Login == User.Identity.Name)
                    .Select(u => u.PrincipalId)
                    .FirstOrDefault();

                if (teacherId == null && principalId == null)
                {
                    TempData["message"] = "Brak wymaganych uprawnien.";
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("teachers/add")]
        public async Task<IActionResult> Create(TeachersViewModel model)
        {
            using (var context = new SchoolDbContext())
            {
                var teacherId = context.Users
                    .Where(u => u.Login == User.Identity.Name)
                    .Select(u => u.TeacherId)
                    .FirstOrDefault();

                var principalId = context.Users
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
                    var logins = context.Users.Select(u => u.Login).ToList();
                    if (logins.Contains(model.User.Login))
                    {
                        return NotFound("login already exists");
                    }
    
                    context.Addresses.Add(model.Address);
                   
                    await context.SaveChangesAsync();

                    var pdata = model.PersonalData;
                    pdata.Address = model.Address;
                    context.PersonalDatas.Add(pdata);
                    await context.SaveChangesAsync();

                    var teacher = model.Teacher;
                    teacher.PersonalData = pdata;
                    context.Teachers.Add(teacher);

                    await context.SaveChangesAsync();
                    var user = new Users
                    {
                        Login = model.User.Login,
                        Password = model.User.Password,
                        TeacherId = teacher.TeacherId
                    };

                    var result = await _userManager.CreateAsync(user, user.Password);
                    
                    await context.SaveChangesAsync();

                    return RedirectToAction("Get");
                }

                return View();
            }
        }

        public IActionResult Details(int? id)
        {

            using (var context = new SchoolDbContext())
            {
                var teacherId = context.Users
                    .Where(u => u.Login == User.Identity.Name)
                    .Select(u => u.TeacherId)
                    .FirstOrDefault();

                var principalId = context.Users
                    .Where(u => u.Login == User.Identity.Name)
                    .Select(u => u.PrincipalId)
                    .FirstOrDefault();

                if (teacherId == null && principalId == null)
                {
                    TempData["message"] = "Brak wymaganych uprawnien.";
                    return RedirectToAction("Index", "Home");
                }

                Teachers teacher = context.Teachers.Find(id);
                if (teacher == null)
                {
                    return NotFound();
                }

                


                var teachersQuali = context.Teachers
                    .Where(t => t.TeacherId == id)
                    .SelectMany(c => c.QualificationTeachers)
                    .Where(c => c.TeacherTeacherId == id)
                    .Include(c => c.QualificationQualification)
                    .ToList();
                

                var teacherPersonal = context.Teachers
                    .Where(t => t.TeacherId == id)
                    .Select(t => t.PersonalData)
                    .FirstOrDefault();

                var teacherAddress = context.Teachers
                    .Where(t => t.TeacherId == id)
                    .Select(t => t.PersonalData.Address)
                    .FirstOrDefault();


                teacher.QualificationTeachers = teachersQuali;
                teacher.PersonalData = teacherPersonal;
                teacher.PersonalData.Address = teacherAddress;


                return View(teacher);
            }
                
        }

        // GET: Teacher/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var _context = new SchoolDbContext())
            {
                var teacher = await _context.Teachers
                    .Include(p => p.PersonalData)
                    .FirstOrDefaultAsync(m => m.TeacherId == id);
                if (teacher == null)
                {
                    return NotFound();
                }
            
                return View(teacher);
            }     
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var _context = new SchoolDbContext())
            {
                var teacher = await _context.Teachers.FindAsync(id);
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Get));
            }
        }
    }
}