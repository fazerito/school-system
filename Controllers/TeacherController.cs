using System.Linq;
using System.Threading.Tasks;
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
                var teachers = context.Teachers.Include(p => p.PersonalData).ToList();
                return View(teachers);
            }
        }

        [Route("teachers/add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("teachers/add")]
        public async Task<IActionResult> Create(TeachersViewModel model)
        {
            using (var context = new SchoolDbContext())
            {
                if (ModelState.IsValid)
                {
                    var logins = context.Users.Select(u => u.Login).ToList();
                    if (logins.Contains(model.User.Login))
                    {
                        return NotFound("login already exists");
                    }

                    //var address = model.Address;
                    context.Addresses.Add(model.Address);
                    //var pdata = model.PersonalData;
                    //pdata.Address = address;
                    //context.PersonalDatas.Add(pdata);
                    //var address = model.Address;
                    //context.Addresses.Add(address);
                    await context.SaveChangesAsync();

                    //var pdata = model.PersonalData;
                    //pdata.Address = address;
                    //context.PersonalDatas.Add(pdata);
                    //await context.SaveChangesAsync();


                    context.Teachers.Add(model.Teacher);

                    await context.SaveChangesAsync();
                    var user = new Users
                    {
                        Login = model.User.Login,
                        Password = model.User.Password,
                    };

                    var result = await _userManager.CreateAsync(user, user.Password);
                    if (result.Succeeded)
                        await _userManager.AddToRoleAsync(user, "Teacher");
                    //context.Users.Add(user);

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
    }
}