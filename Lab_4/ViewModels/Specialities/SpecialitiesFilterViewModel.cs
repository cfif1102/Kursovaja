using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_4.ViewModels.Specialities
{
    public class SpecialitiesFilterViewModel
    {
        public SelectList Faculties { get; set; }

        public int FacultyId { get; set; }

        public SpecialitiesFilterViewModel(List<Faculty> faculties, int facultyId)
        {
            faculties.Insert(0, new Faculty { FacultyId = 0, FacultyName = "Все" });

            Faculties = new SelectList(faculties, "FacultyId", "FacultyName");

            FacultyId = facultyId;
        }
    }
}
