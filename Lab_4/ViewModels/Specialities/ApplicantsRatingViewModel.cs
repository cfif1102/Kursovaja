namespace Lab_4.ViewModels.Specialities
{
    public class ApplicantsRatingViewModel
    {

        public Specialty Speciality { get; set; }

        public IEnumerable<ApplicantGrade> ApplicantsGrade { get; set; }

        public decimal? EnterGrade { get; set; }

        public int TakeAmount { get; set; }
    }

    public class ApplicantGrade
    {
        public Applicant Applicant { get; set; }

        public decimal? Grade { get; set; }
    }
}
