namespace Lab_4.ViewModels.AdmissionOfficers
{
    public enum SortState
    {
        NameAsc, NameDesc,
        DepartmentAsc, DepartmentDesc,
    }

    public class AdmissionOfficersSortViewModel
    {
        public SortState NameSort { get; }

        public SortState DepartmentSort { get; }

        public SortState Current { get; }

        public AdmissionOfficersSortViewModel(SortState state)
        {
            NameSort = state == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            DepartmentSort = state == SortState.DepartmentAsc ? SortState.DepartmentDesc : SortState.DepartmentAsc;

            Current = state;
        }
    }
}
