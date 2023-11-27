using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_4.ViewModels.AdmissionsPlans
{
    public class AdmissionPlansFilterViewModel
    {
        public SelectList Specialities { get; }

        public int SpecialityId { get; }

        public AdmissionPlansFilterViewModel(List<Specialty> specialities, int specialityId)
        {
            specialities.Insert(0, new Specialty { SpecialtyName = "Все", SpecialtyId = 0 });

            Specialities = new SelectList(specialities, "SpecialtyId", "SpecialtyName");    
            SpecialityId = specialityId;
        }
    }
}
