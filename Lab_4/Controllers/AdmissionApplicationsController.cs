using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_4.Data;
using Lab_4.ViewModels.AdmissionApplications;
using Lab_4.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Lab_4.Controllers
{
    public class AdmissionApplicationsController : Controller
    {
        private readonly StudentsContext _context;

        public AdmissionApplicationsController(StudentsContext context)
        {
            _context = context;
        }

        // GET: AdmissionApplications
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        [AllowAnonymous]

        public async Task<IActionResult> Index(int page = 1, int specialityId = 0, int applicantId = 0, SortState sortOrder = SortState.AdmissionOfficerAsc)
        {
            IQueryable<AdmissionApplication> applicants = _context.AdmissionApplications.Include(a => a.AdmissionsOfficer).Include(a => a.Applicant).Include(a => a.Specialty);

            int pageSize = 10;

            if (applicantId != 0)
            {
                applicants = applicants.Where(p => p.ApplicantId == applicantId);
            }

            if (specialityId != 0)
            {
                applicants = applicants.Where(p => p.SpecialtyId == specialityId);
            }

            var count = applicants.Count();
            var items = applicants.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.ApplicantDesc:
                    items = items.OrderByDescending(s => s.Applicant.Name);
                    break;
                case SortState.OtherDetailsDesc:
                    items = items.OrderByDescending(s => s.OtherDetails);
                    break;
                case SortState.ApplicationDateDesc:
                    items = items.OrderByDescending(s => s.ApplicationDate);
                    break;
                case SortState.AdmissionOfficerDesc:
                    items = items.OrderByDescending(s => s.AdmissionsOfficer.FullName);
                    break;
                case SortState.SpecialityDesc:
                    items = items.OrderByDescending(s => s.Specialty.SpecialtyName);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<AdmissionApplication, AdmissionApplicationFilterViewModel, AdmissionApplicationSortViewModel> viewModel = new
                (items, pageViewModel, new AdmissionApplicationFilterViewModel(_context.Specialties.ToList(), _context.Applicants.ToList(), specialityId, applicantId), new AdmissionApplicationSortViewModel(sortOrder));

            return View(viewModel);
        }

        // GET: AdmissionApplications/Details/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdmissionApplications == null)
            {
                return NotFound();
            }

            var admissionApplication = await _context.AdmissionApplications
                .Include(a => a.AdmissionsOfficer)
                .Include(a => a.Applicant)
                .Include(a => a.Specialty)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (admissionApplication == null)
            {
                return NotFound();
            }

            return View(admissionApplication);
        }

        // GET: AdmissionApplications/Create
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public IActionResult Create()
        {
            ViewData["AdmissionsOfficerId"] = new SelectList(_context.AdmissionsOfficers, "AdmissionsOfficerId", "FullName");
            ViewData["ApplicantId"] = new SelectList(_context.Applicants, "ApplicantId", "Name");
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "SpecialtyId", "SpecialtyName");
            return View();
        }

        // POST: AdmissionApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Create([Bind("ApplicationId,ApplicationDate,ApplicantId,SpecialtyId,AdmissionsOfficerId,OtherDetails")] AdmissionApplication admissionApplication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admissionApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdmissionsOfficerId"] = new SelectList(_context.AdmissionsOfficers, "AdmissionsOfficerId", "FullName", admissionApplication.AdmissionsOfficerId);
            ViewData["ApplicantId"] = new SelectList(_context.Applicants, "ApplicantId", "Name", admissionApplication.ApplicantId);
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "SpecialtyId", "SpecialtyName", admissionApplication.SpecialtyId);
            return View(admissionApplication);
        }

        // GET: AdmissionApplications/Edit/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdmissionApplications == null)
            {
                return NotFound();
            }

            var admissionApplication = await _context.AdmissionApplications.FindAsync(id);
            if (admissionApplication == null)
            {
                return NotFound();
            }
            ViewData["AdmissionsOfficerId"] = new SelectList(_context.AdmissionsOfficers, "AdmissionsOfficerId", "FullName", admissionApplication.AdmissionsOfficerId);
            ViewData["ApplicantId"] = new SelectList(_context.Applicants, "ApplicantId", "Name", admissionApplication.ApplicantId);
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "SpecialtyId", "SpecialtyName", admissionApplication.SpecialtyId);
            return View(admissionApplication);
        }

        // POST: AdmissionApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,ApplicationDate,ApplicantId,SpecialtyId,AdmissionsOfficerId,OtherDetails")] AdmissionApplication admissionApplication)
        {
            if (id != admissionApplication.ApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admissionApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionApplicationExists(admissionApplication.ApplicationId))
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
            ViewData["AdmissionsOfficerId"] = new SelectList(_context.AdmissionsOfficers, "AdmissionsOfficerId", "FullName", admissionApplication.AdmissionsOfficerId);
            ViewData["ApplicantId"] = new SelectList(_context.Applicants, "ApplicantId", "Name", admissionApplication.ApplicantId);
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "SpecialtyId", "SpecialtyName", admissionApplication.SpecialtyId);
            return View(admissionApplication);
        }

        // GET: AdmissionApplications/Delete/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdmissionApplications == null)
            {
                return NotFound();
            }

            var admissionApplication = await _context.AdmissionApplications
                .Include(a => a.AdmissionsOfficer)
                .Include(a => a.Applicant)
                .Include(a => a.Specialty)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (admissionApplication == null)
            {
                return NotFound();
            }

            return View(admissionApplication);
        }

        // POST: AdmissionApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdmissionApplications == null)
            {
                return Problem("Entity set 'StudentsContext.AdmissionApplications'  is null.");
            }
            var admissionApplication = await _context.AdmissionApplications.FindAsync(id);
            if (admissionApplication != null)
            {
                _context.AdmissionApplications.Remove(admissionApplication);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionApplicationExists(int id)
        {
          return (_context.AdmissionApplications?.Any(e => e.ApplicationId == id)).GetValueOrDefault();
        }
    }
}
