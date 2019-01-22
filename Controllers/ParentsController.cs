using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SchoolProject.Models;
using SchoolProject.ViewModels;

namespace SchoolProject.Controllers
{
    public class ParentsController : Controller
    {
        private SignInManager<Users> _signManager;
        private UserManager<Users> _userManager;
        private readonly SchoolDbContext _context;


        public ParentsController(UserManager<Users> userManager, SignInManager<Users> signManager, SchoolDbContext context)
        {
            _signManager = signManager;
            _userManager = userManager;
            _context = context;
        }

        // GET: Parents
        public async Task<IActionResult> Index()
        {
            var parents = _context.Parents
                .Include(p => p.PersonalData).ToList();

            return View(parents);
        }

        // GET: Parents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parents = await _context.Parents
                .Include(p => p.PersonalData)
                .Include(p => p.Students)
                .FirstOrDefaultAsync(m => m.ParentId == id);
            if (parents == null)
            {
                return NotFound();
            }

            return View(parents);
        }

        // GET: Parents/Create
        [Route("parents/add")]
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: Parents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("parents/add")]
        public async Task<IActionResult> Create(ParentViewModel model)
        {
            var logins = _context.Users.Select(u => u.Login).ToList();
            if (logins.Contains(model.User.Login))
            {
                return NotFound("login already exists");
            }

            if (ModelState.IsValid)
            {
                _context.Addresses.Add(model.Address);

                await _context.SaveChangesAsync();

                var pdata = model.PersonalData;
                pdata.Address = model.Address;
                _context.PersonalDatas.Add(pdata);
                await _context.SaveChangesAsync();

                model.Parent.PersonalData = pdata;
                var parent = model.Parent;
                parent.PersonalData = pdata;

                parent.Students = model.Students;
                _context.Parents.Add(parent);

                await _context.SaveChangesAsync();
                var user = new Users
                {
                    Login = model.User.Login,
                    Password = model.User.Password,
                    ParentId = parent.ParentId
                };

                var result = await _userManager.CreateAsync(user, user.Password);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
   
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // GET: Parents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parents = await _context.Parents.FindAsync(id);
            if (parents == null)
            {
                return NotFound();
            }
            ViewData["PersonalDataId"] = new SelectList(_context.PersonalDatas, "PersonalDataId", "FirstName", parents.PersonalDataId);
            return View(parents);
        }

        // POST: Parents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParentId,PersonalDataId")] Parents parents)
        {
            if (id != parents.ParentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentsExists(parents.ParentId))
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
            ViewData["PersonalDataId"] = new SelectList(_context.PersonalDatas, "PersonalDataId", "FirstName", parents.PersonalDataId);
            return View(parents);
        }

        // GET: Parents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parents = await _context.Parents
                .Include(p => p.PersonalData)
                .FirstOrDefaultAsync(m => m.ParentId == id);
            if (parents == null)
            {
                return NotFound();
            }

            return View(parents);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parents = await _context.Parents.FindAsync(id);
            _context.Parents.Remove(parents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentsExists(int id)
        {
            return _context.Parents.Any(e => e.ParentId == id);
        }
    }
}
