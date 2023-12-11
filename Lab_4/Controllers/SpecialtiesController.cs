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
using Microsoft.Data.SqlClient;
using System.Drawing.Printing;

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

        public async Task<IActionResult> SpecialitiesCount(int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<SpecialitiesCountViewModel> counts = _context.Specialties
                .Include(s => s.AdmissionApplications)
                .Select(s => new SpecialitiesCountViewModel
                {
                    Specialty = s,
                    Count = s.AdmissionApplications.Count()
                });

            int pageSize = 10;

            var count = counts.Count();
            var items = counts.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    items = items.OrderByDescending(s => s.Specialty.SpecialtyName);
                    break;
                case SortState.CountDesc:
                    items = items.OrderByDescending(s => s.Count);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<SpecialitiesCountViewModel, SpecialitiesFilterViewModel, SpecialitiesSortViewModel> viewModel = new
                (items, pageViewModel, null, new SpecialitiesSortViewModel(sortOrder));

            return View(viewModel);

        }

        public async Task<IActionResult> ApplicantsRating(int page = 1, int year = 0)
        {

            IQueryable<Specialty> specialities = _context.Specialties;

            if (year == 0)
            {
                year = DateTime.Now.Year;
            }

            specialities = from s in specialities
                           join ad in _context.AdmissionApplications on s.SpecialtyId equals ad.SpecialtyId
                           where ad.ApplicationDate.Year == year
                           select s;

            IQueryable<ApplicantsRatingViewModel> specs = specialities
                .Select(s => new ApplicantsRatingViewModel
                {
                    Speciality = s,
                    ApplicantsGrade = s.AdmissionApplications.Select(ad => new ApplicantGrade
                    {
                        Applicant = ad.Applicant,
                        Grade = ad.Applicant.ApplicantCertificates.Sum(cer => cer.Grade),
                    }).OrderByDescending(a => a.Grade).ToList(),
                    TakeAmount = _context.AdmissionPlans.FirstOrDefault(p => p.SpecialtyId == s.SpecialtyId && p.Year == year).NumberOfSeats,
                    EnterGrade = s.AdmissionApplications.Select(ad => new ApplicantGrade
                    {
                        Applicant = ad.Applicant,
                        Grade = ad.Applicant.ApplicantCertificates.Sum(cer => cer.Grade)
                    }).OrderByDescending(a => a.Grade).Take(_context.AdmissionPlans.FirstOrDefault(p => p.SpecialtyId == s.SpecialtyId && p.Year == year).NumberOfSeats).Last().Grade
                })
                .Where(p => p.ApplicantsGrade.Any());

            int pageSize = 5;
            

            var count = specs.Count();
            var items = specs.Skip((page - 1) * pageSize).Take(pageSize);


            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<ApplicantsRatingViewModel, SpecialitiesFilterViewModel, SpecialitiesSortViewModel> viewModel = new
                (items, pageViewModel, new SpecialitiesFilterViewModel(null, 0, year), null);

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
