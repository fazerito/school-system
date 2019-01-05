using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;
using SchoolProject.ViewModels;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
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
        public IActionResult Create(TeachersViewModel model)
        {
            using (var context = new SchoolDbContext())
            {
                if (ModelState.IsValid)
                {
                    var logins = context.Users.Select(u => u.Login).ToList();
                    if (logins.Contains(model.User.Login))
                    {
                        return View("Error");
                    }

                    context.Addresses.Add(model.Address);
                    model.PersonalData.AddressId = model.Address.AddressId;
                    context.PersonalDatas.Add(model.PersonalData);
                    context.Teachers.Add(model.Teacher);
                    model.Teacher.PersonalDataId = model.PersonalData.PersonalDataId;
                    context.Users.Add(model.User);
                    model.User.TeacherId = model.Teacher.TeacherId;

                    context.SaveChanges();

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