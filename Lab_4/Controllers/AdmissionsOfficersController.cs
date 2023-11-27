using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_4.Data;
using Lab_4.ViewModels.AdmissionOfficers;
using Lab_4.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Lab_4.Controllers
{
    public class AdmissionsOfficersController : Controller
    {
        private readonly StudentsContext _context;

        public AdmissionsOfficersController(StudentsContext context)
        {
            _context = context;
        }

        // GET: AdmissionsOfficers
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        [AllowAnonymous]

        public async Task<IActionResult> Index(int page = 1, string name = "", string department = "", SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<AdmissionsOfficer> applicants = _context.AdmissionsOfficers;

            int pageSize = 10;

            if (name != null && name.Trim() != "")
            {
                applicants = applicants.Where(a => a.FullName.Contains(name));
            }

            if (department != null && department.Trim() != "")
            {
                applicants = applicants.Where(a => a.Department.Contains(department));
            }

            var count = applicants.Count();
            var items = applicants.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    items = items.OrderByDescending(s => s.FullName);
                    break;
                case SortState.DepartmentDesc:
                    items = items.OrderByDescending(s => s.Department);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<AdmissionsOfficer, AdmissionOfficersFilterViewModel, AdmissionOfficersSortViewModel> viewModel = new
                (items, pageViewModel, new AdmissionOfficersFilterViewModel(department, name), new AdmissionOfficersSortViewModel(sortOrder));

            return _context.AdmissionsOfficers != null ? 
                          View(viewModel) :
                          Problem("Entity set 'StudentsContext.AdmissionsOfficers'  is null.");
        }

        // GET: AdmissionsOfficers/Details/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdmissionsOfficers == null)
            {
                return NotFound();
            }

            var admissionsOfficer = await _context.AdmissionsOfficers
                .FirstOrDefaultAsync(m => m.AdmissionsOfficerId == id);
            if (admissionsOfficer == null)
            {
                return NotFound();
            }

            return View(admissionsOfficer);
        }

        // GET: AdmissionsOfficers/Create
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdmissionsOfficers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Create([Bind("AdmissionsOfficerId,FullName,Department")] AdmissionsOfficer admissionsOfficer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admissionsOfficer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admissionsOfficer);
        }

        // GET: AdmissionsOfficers/Edit/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdmissionsOfficers == null)
            {
                return NotFound();
            }

            var admissionsOfficer = await _context.AdmissionsOfficers.FindAsync(id);
            if (admissionsOfficer == null)
            {
                return NotFound();
            }
            return View(admissionsOfficer);
        }

        // POST: AdmissionsOfficers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("AdmissionsOfficerId,FullName,Department")] AdmissionsOfficer admissionsOfficer)
        {
            if (id != admissionsOfficer.AdmissionsOfficerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admissionsOfficer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionsOfficerExists(admissionsOfficer.AdmissionsOfficerId))
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
            return View(admissionsOfficer);
        }

        // GET: AdmissionsOfficers/Delete/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdmissionsOfficers == null)
            {
                return NotFound();
            }

            var admissionsOfficer = await _context.AdmissionsOfficers
                .FirstOrDefaultAsync(m => m.AdmissionsOfficerId == id);
            if (admissionsOfficer == null)
            {
                return NotFound();
            }

            return View(admissionsOfficer);
        }

        // POST: AdmissionsOfficers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdmissionsOfficers == null)
            {
                return Problem("Entity set 'StudentsContext.AdmissionsOfficers'  is null.");
            }
            var admissionsOfficer = await _context.AdmissionsOfficers.FindAsync(id);
            if (admissionsOfficer != null)
            {
                _context.AdmissionsOfficers.Remove(admissionsOfficer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionsOfficerExists(int id)
        {
          return (_context.AdmissionsOfficers?.Any(e => e.AdmissionsOfficerId == id)).GetValueOrDefault();
        }
    }
}
