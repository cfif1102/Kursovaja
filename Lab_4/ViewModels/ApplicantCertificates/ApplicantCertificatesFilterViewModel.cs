using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_4.ViewModels.ApplicantCertificates
{
    public class ApplicantCertificatesFilterViewModel
    {
        public SelectList Applicants { get; }

        public int ApplicantId { get; }

        public decimal Grade { get; }

        public ApplicantCertificatesFilterViewModel(List<Applicant> applicants, int applicantId, decimal grade)
        {
            applicants.Insert(0, new Applicant { ApplicantId = 0, Name = "Все" });

            Applicants = new SelectList(applicants, "ApplicantId", "Name");

            ApplicantId = applicantId;

            Grade = grade;
        }
    }
}
