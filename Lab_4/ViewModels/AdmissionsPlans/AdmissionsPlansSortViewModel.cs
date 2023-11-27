namespace Lab_4.ViewModels.AdmissionsPlans
{
    public enum SortState
    {
        YearAsc, YearDesc,
        SpecialityAsc, SpecialityDesc,
        SeatsAsc, SeatsDesc,
    }

    public class AdmissionsPlansSortViewModel
    {
        public SortState YearSort { get; }

        public SortState SpecialitySort { get; }

        public SortState SeatsSort { get; }

        public SortState Current { get; }

        public AdmissionsPlansSortViewModel(SortState state)
        {
            YearSort = state == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;
            SpecialitySort = state == SortState.SpecialityAsc ? SortState.SpecialityDesc : SortState.SpecialityAsc;
            SeatsSort = state == SortState.SeatsAsc ? SortState.SeatsDesc : SortState.SeatsAsc;

            Current = state;
        }
    }
}
