namespace Lab_4.ViewModels.Faculties
{
    public enum SortState
    {
        NameAsc, NameDesc
    }

    public class FacultiesSortViewModel
    {
        public SortState NameSort { get; }

        public SortState Current { get; }

        public FacultiesSortViewModel(SortState state)
        {
            NameSort = state == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            Current = state;
        }
    }
}
