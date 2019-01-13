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
    public class ParentController : Controller
    {
        private readonly SchoolDbContext _context;

        public ParentController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Parent
        public async Task<IActionResult> Index()
        {
            var schoolDbContext = _context.Parents.Include(p => p.PersonalData);
            return View(await schoolDbContext.ToListAsync());
        }

        // GET: Parent/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Parent/Create
        public IActionResult Create()
        {
            ViewData["PersonalDataId"] = new SelectList(_context.PersonalDatas, "PersonalDataId", "FirstName");
            return View();
        }

        // POST: Parent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParentId,PersonalDataId")] Parents parents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonalDataId"] = new SelectList(_context.PersonalDatas, "PersonalDataId", "FirstName", parents.PersonalDataId);
            return View(parents);
        }

        // GET: Parent/Edit/5
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

        // POST: Parent/Edit/5
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

        // GET: Parent/Delete/5
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

        // POST: Parent/Delete/5
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
