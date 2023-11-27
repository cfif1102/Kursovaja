namespace Lab_4.ViewModels.EducationInstitutions
{
    public enum SortState
    {
        NameAsc, NameDesc,
    }

    public class EducationInstitutionsSortViewModel
    {
        public SortState NameSort { get; }

        public SortState Current { get; }

        public EducationInstitutionsSortViewModel(SortState state)
        {
            NameSort = state == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            Current = state;
        }
    }
}
