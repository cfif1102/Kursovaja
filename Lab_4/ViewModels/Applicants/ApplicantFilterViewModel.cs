using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_4.ViewModels.Applicants
{
    public class ApplicantFilterViewModel
    {
        public SelectList Univers { get; }

        public int UniverId { get; }

        public SelectList Parents { get; }

        public int ParentId { get; }

        public ApplicantFilterViewModel(List<EducationInstitution> univers, List<Parent> parents, int univerId, int parentId)
        {
            univers.Insert(0, new EducationInstitution { EducationInstitutionId = 0, InstitutionName = "Все" });

            Univers = new SelectList(univers, "EducationInstitutionId", "InstitutionName");
            UniverId = univerId;

            parents.Insert(0, new Parent { ParentsId = 0, Parent1Name = "Все" });

            Parents = new SelectList(parents, "ParentsId", "Parent1Name");
            ParentId = parentId;
        }
    }
}
