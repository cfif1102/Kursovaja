using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_4.Data;
using Lab_4.ViewModels.ApplicantCertificates;
using Lab_4.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Lab_4.Controllers
{
    public class ApplicantCertificatesController : Controller
    {
        private readonly StudentsContext _context;

        public ApplicantCertificatesController(StudentsContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        [AllowAnonymous]

        // GET: ApplicantCertificates
        public async Task<IActionResult> Index(int page = 1, int applicantId = 0, decimal grade = 0, SortState sortOrder = SortState.GradeAsc)
        {
            IQueryable<ApplicantCertificate> certificates = _context.ApplicantCertificates.Include(a => a.Applicant);

            int pageSize = 10;

            if (applicantId != 0)
            {
                certificates = certificates.Where(a => a.ApplicantId == applicantId);
            }

            if (grade != 0)
            {
                certificates = certificates.Where(a => a.Grade >= grade);

            }

            var count = certificates.Count();
            var items = certificates.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.ApplicantDesc:
                    items = items.OrderByDescending(s => s.Applicant.Name);
                    break;
                case SortState.GradeDesc:
                    items = items.OrderByDescending(s => s.Grade);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<ApplicantCertificate, ApplicantCertificatesFilterViewModel, ApplicantCertificatesSortViewModel> viewModel = new
                (items, pageViewModel, new ApplicantCertificatesFilterViewModel(_context.Applicants.ToList(), applicantId, grade), new ApplicantCertificatesSortViewModel(sortOrder));


            return View(viewModel);
        }

        // GET: ApplicantCertificates/Details/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ApplicantCertificates == null)
            {
                return NotFound();
            }

            var applicantCertificate = await _context.ApplicantCertificates
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.CertificateId == id);
            if (applicantCertificate == null)
            {
                return NotFound();
            }

            return View(applicantCertificate);
        }

        // GET: ApplicantCertificates/Create
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.Applicants, "ApplicantId", "Name");
            return View();
        }

        // POST: ApplicantCertificates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Create([Bind("CertificateId,ApplicantId,Grade")] ApplicantCertificate applicantCertificate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantCertificate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicants, "ApplicantId", "Name", applicantCertificate.ApplicantId);
            return View(applicantCertificate);
        }

        // GET: ApplicantCertificates/Edit/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ApplicantCertificates == null)
            {
                return NotFound();
            }

            var applicantCertificate = await _context.ApplicantCertificates.FindAsync(id);
            if (applicantCertificate == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicants, "ApplicantId", "Name", applicantCertificate.ApplicantId);
            return View(applicantCertificate);
        }

        // POST: ApplicantCertificates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Edit(int id, [Bind("CertificateId,ApplicantId,Grade")] ApplicantCertificate applicantCertificate)
        {
            if (id != applicantCertificate.CertificateId)
            {
                return NotFound();
            }

     

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantCertificate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantCertificateExists(applicantCertificate.CertificateId))
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
            ViewData["ApplicantId"] = new SelectList(_context.Applicants, "ApplicantId", "Name", applicantCertificate.ApplicantId);
            return View(applicantCertificate);
        }

        // GET: ApplicantCertificates/Delete/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ApplicantCertificates == null)
            {
                return NotFound();
            }

            var applicantCertificate = await _context.ApplicantCertificates
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.CertificateId == id);
            if (applicantCertificate == null)
            {
                return NotFound();
            }

            return View(applicantCertificate);
        }

        // POST: ApplicantCertificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ApplicantCertificates == null)
            {
                return Problem("Entity set 'StudentsContext.ApplicantCertificates'  is null.");
            }
            var applicantCertificate = await _context.ApplicantCertificates.FindAsync(id);
            if (applicantCertificate != null)
            {
                _context.ApplicantCertificates.Remove(applicantCertificate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantCertificateExists(int id)
        {
          return (_context.ApplicantCertificates?.Any(e => e.CertificateId == id)).GetValueOrDefault();
        }
    }
}
