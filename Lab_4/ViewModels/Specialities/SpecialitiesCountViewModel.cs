using System.ComponentModel.DataAnnotations;

namespace Lab_4.ViewModels.Specialities
{
    public class SpecialitiesCountViewModel
    {
        [Display(Name = "Специальность")]
        public Specialty Specialty { get; set; }

        [Display(Name = "Кол-во заявлений")]
        public int Count { get; set; }
    }
}
