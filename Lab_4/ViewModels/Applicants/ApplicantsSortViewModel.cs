namespace Lab_4.ViewModels.Applicants
{
    public enum SortState
    {
        NameAsc, NameDesc,
        SurnameAsc, SurnameDesc,
        MiddlenameAsc, MiddlenameDesc,
        IdDocAsc, IdDocDesc,
        EdDocAsc, EdDocDesc,
        AvGradeAsc, AvGradeDesc,
        AddressAsc, AddressDesc,
        PhoneAsc, PhoneDesc,
        EmailAsc, EmailDesc,
        GradYearAsc, GradYearDesc,
        ForeignLanguageAsc, ForeignLanguageDesc,
        ParentAsc, ParentDesc
    }
    public class ApplicantsSortViewModel
    {
        public SortState NameSort { get; }

        public SortState SurnameSort { get; }

        public SortState MiddlenameSort { get; }

        public SortState IdDocSort {get;}

        public SortState EdDocSort { get;}

        public SortState AvGradeSort { get; }

        public SortState AddressSort { get; }

        public SortState PhoneSort { get; }

        public SortState EmailSort { get; }

        public SortState GradYearSort { get; }

        public SortState ForeignLanguageSort { get; }

        public SortState ParentSort { get; }

        public SortState Current { get; }

        public ApplicantsSortViewModel(SortState state)
        {
            NameSort = state == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            SurnameSort = state == SortState.SurnameAsc ? SortState.SurnameDesc : SortState.SurnameAsc;
            MiddlenameSort = state == SortState.MiddlenameAsc ? SortState.MiddlenameDesc : SortState.MiddlenameAsc;
            IdDocSort = state == SortState.IdDocAsc ? SortState.IdDocDesc : SortState.IdDocAsc;
            EdDocSort = state == SortState.EdDocAsc ? SortState.EdDocDesc : SortState.EdDocAsc;
            AvGradeSort = state == SortState.AvGradeAsc ? SortState.AvGradeDesc : SortState.AvGradeAsc;
            AddressSort = state == SortState.AddressAsc ? SortState.AddressDesc : SortState.AddressAsc;
            PhoneSort = state == SortState.PhoneAsc ? SortState.PhoneDesc : SortState.PhoneAsc;
            EmailSort = state == SortState.EmailAsc ? SortState.EmailDesc : SortState.EmailAsc;
            GradYearSort = state == SortState.GradYearAsc ? SortState.GradYearDesc : SortState.GradYearAsc;
            ForeignLanguageSort = state == SortState.ForeignLanguageAsc ? SortState.ForeignLanguageDesc : SortState.ForeignLanguageAsc;
            ParentSort = state == SortState.ParentAsc ? SortState.ParentDesc : SortState.ParentAsc;

            Current = state;
        }
    }
}
