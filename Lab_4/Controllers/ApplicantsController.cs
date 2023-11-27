using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_4.Data;
using Lab_4.ViewModels.Applicants;
using Lab_4.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Lab_4.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly StudentsContext _context;

        public ApplicantsController(StudentsContext context)
        {
            _context = context;
        }

        // GET: Applicants
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        [AllowAnonymous]

        public async Task<IActionResult> Index(int page = 1, int univerId = 0, int parentId = 0, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Applicant> applicants = _context.Applicants.Include(a => a.EducationInstitution).Include(a => a.Parents);

            int pageSize = 10;

            if (univerId != 0)
            {
                applicants = applicants.Where(a => a.EducationInstitutionId == univerId);
            }

            if (parentId != 0)
            {
                applicants = applicants.Where(a => a.ParentsId == parentId);
            }

            var count = applicants.Count();
            var items = applicants.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case SortState.SurnameDesc:
                    items = items.OrderByDescending(s => s.Surname);
                    break;
                case SortState.MiddlenameDesc:
                    items = items.OrderByDescending(s => s.Middlename);
                    break;
                case SortState.IdDocDesc:
                    items = items.OrderByDescending(s => s.IdentificationDocument);
                    break;
                case SortState.EdDocDesc:
                    items = items.OrderByDescending(s => s.EducationDocument);
                    break;
                case SortState.AvGradeDesc:
                    items = items.OrderByDescending(s => s.AverageGrade);
                    break;
                case SortState.AddressDesc:
                    items = items.OrderByDescending(s => s.ResidentialAddress);
                    break;
                case SortState.PhoneDesc:
                    items = items.OrderByDescending(s => s.Phone);
                    break;
                case SortState.EmailDesc:
                    items = items.OrderByDescending(s => s.Email);
                    break;
                case SortState.GradYearDesc:
                    items = items.OrderByDescending(s => s.GraduationYear);
                    break;
                case SortState.ForeignLanguageDesc:
                    items = items.OrderByDescending(s => s.ForeignLanguage);
                    break;
                case SortState.ParentDesc:
                    items = items.OrderByDescending(s => s.Parents.Parent1Name);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Applicant, ApplicantFilterViewModel , ApplicantsSortViewModel> viewModel = new
                (items, pageViewModel, new ApplicantFilterViewModel(_context.EducationInstitutions.ToList(), _context.Parents.ToList(), univerId, parentId), new ApplicantsSortViewModel(sortOrder));


            return View(viewModel);
        }

        // GET: Applicants
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        [AllowAnonymous]

        public async Task<IActionResult> IndexBig(int page = 1, int univerId = 0, int parentId = 0, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Applicant> applicants = _context.Applicants.Include(a => a.EducationInstitution).Include(a => a.Parents);

            int pageSize = 10;

            if (univerId != 0)
            {
                applicants = applicants.Where(a => a.EducationInstitutionId == univerId);
            }

            if (parentId != 0)
            {
                applicants = applicants.Where(a => a.ParentsId == parentId);
            }

            var count = applicants.Count();
            var items = applicants.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case SortState.SurnameDesc:
                    items = items.OrderByDescending(s => s.Surname);
                    break;
                case SortState.MiddlenameDesc:
                    items = items.OrderByDescending(s => s.Middlename);
                    break;
                case SortState.IdDocDesc:
                    items = items.OrderByDescending(s => s.IdentificationDocument);
                    break;
                case SortState.EdDocDesc:
                    items = items.OrderByDescending(s => s.EducationDocument);
                    break;
                case SortState.AvGradeDesc:
                    items = items.OrderByDescending(s => s.AverageGrade);
                    break;
                case SortState.AddressDesc:
                    items = items.OrderByDescending(s => s.ResidentialAddress);
                    break;
                case SortState.PhoneDesc:
                    items = items.OrderByDescending(s => s.Phone);
                    break;
                case SortState.EmailDesc:
                    items = items.OrderByDescending(s => s.Email);
                    break;
                case SortState.GradYearDesc:
                    items = items.OrderByDescending(s => s.GraduationYear);
                    break;
                case SortState.ForeignLanguageDesc:
                    items = items.OrderByDescending(s => s.ForeignLanguage);
                    break;
                case SortState.ParentDesc:
                    items = items.OrderByDescending(s => s.Parents.Parent1Name);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Applicant, ApplicantFilterViewModel, ApplicantsSortViewModel> viewModel = new
                (items, pageViewModel, new ApplicantFilterViewModel(_context.EducationInstitutions.ToList(), _context.Parents.ToList(), univerId, parentId), new ApplicantsSortViewModel(sortOrder));


            return View(viewModel);
        }

        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(a => a.EducationInstitution)
                .Include(a => a.Parents)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        // GET: Applicants/Create
        public IActionResult Create()
        {
            ViewData["EducationInstitutionId"] = new SelectList(_context.EducationInstitutions, "EducationInstitutionId", "InstitutionName");
            ViewData["ParentsId"] = new SelectList(_context.Parents, "ParentsId", "Parent1Name");
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Create([Bind("ApplicantId,Surname,Name,Middlename,IdentificationDocument,EducationDocument,AverageGrade,ResidentialAddress,Phone,Email,GraduationYear,EducationInstitutionId,ForeignLanguage,ParentsId")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EducationInstitutionId"] = new SelectList(_context.EducationInstitutions, "EducationInstitutionId", "InstitutionName", applicant.EducationInstitutionId);
            ViewData["ParentsId"] = new SelectList(_context.Parents, "ParentsId", "Parent1Name", applicant.ParentsId);
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Edit(int? id)
        {
            var applicant = await _context.Applicants.FindAsync(id);

            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (applicant == null)
                {
                    return NotFound();
                }
                ViewData["EducationInstitutionId"] = new SelectList(_context.EducationInstitutions, "EducationInstitutionId", "InstitutionName", applicant.EducationInstitutionId);
                ViewData["ParentsId"] = new SelectList(_context.Parents, "ParentsId", "Parent1Name", applicant.ParentsId);
                
            }

            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantId,Surname,Name,Middlename,IdentificationDocument,EducationDocument,AverageGrade,ResidentialAddress,Phone,Email,GraduationYear,EducationInstitutionId,ForeignLanguage,ParentsId")] Applicant applicant)
        {
            if (id != applicant.ApplicantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.ApplicantId))
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
            ViewData["EducationInstitutionId"] = new SelectList(_context.EducationInstitutions, "EducationInstitutionId", "InstitutionName", applicant.EducationInstitutionId);
            ViewData["ParentsId"] = new SelectList(_context.Parents, "ParentsId", "Parent1Name", applicant.ParentsId);
            return View(applicant);
        }

        // GET: Applicants/Delete/5
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(a => a.EducationInstitution)
                .Include(a => a.Parents)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JuniorAdmin,MainAdmin,AdmissionOfficer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applicants == null)
            {
                return Problem("Entity set 'StudentsContext.Applicants'  is null.");
            }
            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant != null)
            {
                _context.Applicants.Remove(applicant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantExists(int id)
        {
          return (_context.Applicants?.Any(e => e.ApplicantId == id)).GetValueOrDefault();
        }
    }
}
