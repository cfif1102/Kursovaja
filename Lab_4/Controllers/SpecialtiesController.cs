using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_4.Data;
using Lab_4.ViewModels.Specialities;
using Lab_4.ViewModels.Faculties;
using Lab_4.ViewModels;
using SortState = Lab_4.ViewModels.Specialities.SortState;
using Microsoft.AspNetCore.Authorization;

namespace Lab_4.Controllers
{
    public class SpecialtiesController : Controller
    {
        private readonly StudentsContext _context;

        public SpecialtiesController(StudentsContext context)
        {
            _context = context;
        }

        // GET: Specialties
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, int facultyId = 0, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Specialty> specialties = _context.Specialties.Include(s => s.Faculty);

            int pageSize = 10;

            if (facultyId != 0)
            {
                specialties = specialties.Where(a => a.FacultyId == facultyId);
            }

            var count = specialties.Count();
            var items = specialties.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    items = items.OrderByDescending(s => s.SpecialtyName);
                    break;
                case SortState.FacultyDesc:
                    items = items.OrderByDescending(s => s.Faculty.FacultyName);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Specialty, SpecialitiesFilterViewModel, SpecialitiesSortViewModel> viewModel = new
                (items, pageViewModel, new SpecialitiesFilterViewModel(_context.Faculties.ToList(), facultyId), new SpecialitiesSortViewModel(sortOrder));

            return View(viewModel);
        }

        // GET: Specialties/Details/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Specialties == null)
            {
                return NotFound();
            }

            var specialty = await _context.Specialties
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(m => m.SpecialtyId == id);
            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }

        // GET: Specialties/Create
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName");
            return View();
        }

        // POST: Specialties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Create([Bind("SpecialtyId,SpecialtyName,FacultyId")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", specialty.FacultyId);
            return View(specialty);
        }

        // GET: Specialties/Edit/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Specialties == null)
            {
                return NotFound();
            }

            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", specialty.FacultyId);
            return View(specialty);
        }

        // POST: Specialties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("SpecialtyId,SpecialtyName,FacultyId")] Specialty specialty)
        {
            if (id != specialty.SpecialtyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialtyExists(specialty.SpecialtyId))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", specialty.FacultyId);
            return View(specialty);
        }

        // GET: Specialties/Delete/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Specialties == null)
            {
                return NotFound();
            }

            var specialty = await _context.Specialties
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(m => m.SpecialtyId == id);
            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }

        // POST: Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Specialties == null)
            {
                return Problem("Entity set 'StudentsContext.Specialties'  is null.");
            }
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty != null)
            {
                _context.Specialties.Remove(specialty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialtyExists(int id)
        {
          return (_context.Specialties?.Any(e => e.SpecialtyId == id)).GetValueOrDefault();
        }
    }
}
