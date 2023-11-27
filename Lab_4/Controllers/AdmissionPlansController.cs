using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_4.Data;
using Lab_4.ViewModels.AdmissionApplications;
using Lab_4.ViewModels.AdmissionsPlans;
using Microsoft.Data.SqlClient;
using SortState = Lab_4.ViewModels.AdmissionsPlans.SortState;
using Lab_4.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Lab_4.Controllers
{
    public class AdmissionPlansController : Controller
    {
        private readonly StudentsContext _context;

        public AdmissionPlansController(StudentsContext context)
        {
            _context = context;
        }

        // GET: AdmissionPlans
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        [AllowAnonymous]

        public async Task<IActionResult> Index(int page = 1, int specialityId = 0, SortState sortOrder = SortState.YearAsc)
        {
            IQueryable<AdmissionPlan> plans = _context.AdmissionPlans.Include(a => a.Specialty);

            int pageSize = 10;

            if (specialityId != 0)
            {
                plans = plans.Where(p => p.SpecialtyId == specialityId);
            }

            var count = plans.Count();
            var items = plans.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.SeatsDesc:
                    items = items.OrderByDescending(s => s.NumberOfSeats);
                    break;
                case SortState.YearDesc:
                    items = items.OrderByDescending(s => s.Year);
                    break;
                case SortState.SpecialityDesc:
                    items = items.OrderByDescending(s => s.Specialty.SpecialtyName);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<AdmissionPlan, AdmissionPlansFilterViewModel, AdmissionsPlansSortViewModel> viewModel = new
                (items, pageViewModel, new AdmissionPlansFilterViewModel(_context.Specialties.ToList(), specialityId), new AdmissionsPlansSortViewModel(sortOrder));


            return View(viewModel);
        }

        // GET: AdmissionPlans/Details/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdmissionPlans == null)
            {
                return NotFound();
            }

            var admissionPlan = await _context.AdmissionPlans
                .Include(a => a.Specialty)
                .FirstOrDefaultAsync(m => m.AdmissionPlanId == id);
            if (admissionPlan == null)
            {
                return NotFound();
            }

            return View(admissionPlan);
        }

        // GET: AdmissionPlans/Create
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public IActionResult Create()
        {
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "SpecialtyId", "SpecialtyName");
            return View();
        }

        // POST: AdmissionPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Create([Bind("AdmissionPlanId,Year,SpecialtyId,NumberOfSeats")] AdmissionPlan admissionPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admissionPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "SpecialtyId", "SpecialtyName", admissionPlan.SpecialtyId);
            return View(admissionPlan);
        }

        // GET: AdmissionPlans/Edit/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdmissionPlans == null)
            {
                return NotFound();
            }

            var admissionPlan = await _context.AdmissionPlans.FindAsync(id);
            if (admissionPlan == null)
            {
                return NotFound();
            }
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "SpecialtyId", "SpecialtyName", admissionPlan.SpecialtyId);
            return View(admissionPlan);
        }

        // POST: AdmissionPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("AdmissionPlanId,Year,SpecialtyId,NumberOfSeats")] AdmissionPlan admissionPlan)
        {
            if (id != admissionPlan.AdmissionPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admissionPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionPlanExists(admissionPlan.AdmissionPlanId))
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
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "SpecialtyId", "SpecialtyName", admissionPlan.SpecialtyId);
            return View(admissionPlan);
        }

        // GET: AdmissionPlans/Delete/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdmissionPlans == null)
            {
                return NotFound();
            }

            var admissionPlan = await _context.AdmissionPlans
                .Include(a => a.Specialty)
                .FirstOrDefaultAsync(m => m.AdmissionPlanId == id);
            if (admissionPlan == null)
            {
                return NotFound();
            }

            return View(admissionPlan);
        }

        // POST: AdmissionPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdmissionPlans == null)
            {
                return Problem("Entity set 'StudentsContext.AdmissionPlans'  is null.");
            }
            var admissionPlan = await _context.AdmissionPlans.FindAsync(id);
            if (admissionPlan != null)
            {
                _context.AdmissionPlans.Remove(admissionPlan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionPlanExists(int id)
        {
          return (_context.AdmissionPlans?.Any(e => e.AdmissionPlanId == id)).GetValueOrDefault();
        }
    }
}
