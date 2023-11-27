using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_4.Data;
using Lab_4.ViewModels.EducationInstitutions;
using Lab_4.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Lab_4.Controllers
{
    public class EducationInstitutionsController : Controller
    {
        private readonly StudentsContext _context;

        public EducationInstitutionsController(StudentsContext context)
        {
            _context = context;
        }

        // GET: EducationInstitutions
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        [AllowAnonymous]

        public async Task<IActionResult> Index(int page = 1, string name = "", SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<EducationInstitution> institutions = _context.EducationInstitutions;

            int pageSize = 10;

            if (name != null && name.Trim() != "")
            {
                institutions = institutions.Where(a => a.InstitutionName.Contains(name));
            }

            var count = institutions.Count();
            var items = institutions.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    items = items.OrderByDescending(s => s.InstitutionName);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<EducationInstitution, EducationInstitutionsFilterViewModel, EducationInstitutionsSortViewModel> viewModel = new
                (items, pageViewModel, new EducationInstitutionsFilterViewModel(name), new EducationInstitutionsSortViewModel(sortOrder));


            return _context.EducationInstitutions != null ? 
                          View(viewModel) :
                          Problem("Entity set 'StudentsContext.EducationInstitutions'  is null.");
        }

        // GET: EducationInstitutions/Details/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EducationInstitutions == null)
            {
                return NotFound();
            }

            var educationInstitution = await _context.EducationInstitutions
                .FirstOrDefaultAsync(m => m.EducationInstitutionId == id);
            if (educationInstitution == null)
            {
                return NotFound();
            }

            return View(educationInstitution);
        }

        // GET: EducationInstitutions/Create
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducationInstitutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Create([Bind("EducationInstitutionId,InstitutionName")] EducationInstitution educationInstitution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationInstitution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educationInstitution);
        }

        // GET: EducationInstitutions/Edit/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EducationInstitutions == null)
            {
                return NotFound();
            }

            var educationInstitution = await _context.EducationInstitutions.FindAsync(id);
            if (educationInstitution == null)
            {
                return NotFound();
            }
            return View(educationInstitution);
        }

        // POST: EducationInstitutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("EducationInstitutionId,InstitutionName")] EducationInstitution educationInstitution)
        {
            if (id != educationInstitution.EducationInstitutionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationInstitution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationInstitutionExists(educationInstitution.EducationInstitutionId))
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
            return View(educationInstitution);
        }

        // GET: EducationInstitutions/Delete/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EducationInstitutions == null)
            {
                return NotFound();
            }

            var educationInstitution = await _context.EducationInstitutions
                .FirstOrDefaultAsync(m => m.EducationInstitutionId == id);
            if (educationInstitution == null)
            {
                return NotFound();
            }

            return View(educationInstitution);
        }

        // POST: EducationInstitutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EducationInstitutions == null)
            {
                return Problem("Entity set 'StudentsContext.EducationInstitutions'  is null.");
            }
            var educationInstitution = await _context.EducationInstitutions.FindAsync(id);
            if (educationInstitution != null)
            {
                _context.EducationInstitutions.Remove(educationInstitution);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationInstitutionExists(int id)
        {
          return (_context.EducationInstitutions?.Any(e => e.EducationInstitutionId == id)).GetValueOrDefault();
        }
    }
}
