using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_4.ViewModels.AdmissionApplications
{
    public class AdmissionApplicationFilterViewModel
    {
        public SelectList Specialities { get; }

        public SelectList Applicants { get; }

        public int SpecialityId { get; }

        public int ApplicantId { get; }

        public AdmissionApplicationFilterViewModel(List<Specialty> specialties, List<Applicant> applicants, int spId, int apId)
        {
            specialties.Insert(0, new Specialty { SpecialtyId = 0, SpecialtyName = "Все" });
            
            Specialities = new SelectList(specialties, "SpecialtyId", "SpecialtyName");

            applicants.Insert(0, new Applicant { ApplicantId = 0, Name = "Все" });

            Applicants = new SelectList(applicants, "ApplicantId", "Name");

            SpecialityId = spId;

            ApplicantId = apId;
        }
    }
}
