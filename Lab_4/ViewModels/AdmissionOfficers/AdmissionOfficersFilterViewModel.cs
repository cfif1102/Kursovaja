using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_4.ViewModels.AdmissionOfficers
{
    public class AdmissionOfficersFilterViewModel
    {
        public string Department { get; }

        public string Name { get; }

        public AdmissionOfficersFilterViewModel(string deparment, string name)
        {
            Department = deparment;
            Name = name;
        }
    }
}
