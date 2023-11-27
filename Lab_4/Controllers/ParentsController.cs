using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_4.Data;
using Lab_4.ViewModels.Parents;
using Lab_4.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Lab_4.Controllers
{
    public class ParentsController : Controller
    {
        private readonly StudentsContext _context;

        public ParentsController(StudentsContext context)
        {
            _context = context;
        }

        // GET: Parents
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, string p1 = "", string p2 = "", SortState sortOrder = SortState.Parent1Asc)
        {
            IQueryable<Parent> parents = _context.Parents;

            int pageSize = 10;

            if (p1 != null && p1.Trim() != "")
            {
                parents = parents.Where(p => p.Parent1Name.Contains(p1));
            }

            if (p2 != null && p2.Trim() != "")
            {
                parents = parents.Where(p => p.Parent2Name.Contains(p2));
            }

            var count = parents.Count();
            var items = parents.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.Parent1Desc:
                    items = items.OrderByDescending(item => item.Parent1Name);
                    break;
                case SortState.Parent2Desc:
                    items = items.OrderByDescending(item => item.Parent2Name);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Parent, ParentsFilterViewMode, ParentsSortViewModel> viewModel = new(items, pageViewModel, new ParentsFilterViewMode(p1, p2), new ParentsSortViewModel(sortOrder));


            return _context.Parents != null ? 
                          View(viewModel) :
                          Problem("Entity set 'StudentsContext.Parents'  is null.");
        }

        // GET: Parents/Details/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parents == null)
            {
                return NotFound();
            }

            var parent = await _context.Parents
                .FirstOrDefaultAsync(m => m.ParentsId == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        // GET: Parents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Create([Bind("ParentsId,Parent1Name,Parent2Name")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parent);
        }

        // GET: Parents/Edit/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parents == null)
            {
                return NotFound();
            }

            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            return View(parent);
        }

        // POST: Parents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Edit(int id, [Bind("ParentsId,Parent1Name,Parent2Name")] Parent parent)
        {
            if (id != parent.ParentsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentExists(parent.ParentsId))
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
            return View(parent);
        }

        // GET: Parents/Delete/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parents == null)
            {
                return NotFound();
            }

            var parent = await _context.Parents
                .FirstOrDefaultAsync(m => m.ParentsId == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parents == null)
            {
                return Problem("Entity set 'StudentsContext.Parents'  is null.");
            }
            var parent = await _context.Parents.FindAsync(id);
            if (parent != null)
            {
                _context.Parents.Remove(parent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentExists(int id)
        {
          return (_context.Parents?.Any(e => e.ParentsId == id)).GetValueOrDefault();
        }
    }
}
