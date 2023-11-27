namespace Lab_4.ViewModels.Parents
{
    public enum SortState
    {
        Parent1Asc, Parent1Desc, Parent2Asc, Parent2Desc
    }
    public class ParentsSortViewModel
    {
        public SortState Parent1Sort { get; }

        public SortState Parent2Sort { get; }

        public SortState Current { get; }

        public ParentsSortViewModel(SortState state) 
        {
            Parent1Sort = state == SortState.Parent1Asc ? SortState.Parent1Desc : SortState.Parent1Asc;
            Parent2Sort = state == SortState.Parent2Asc ? SortState.Parent2Desc : SortState.Parent2Asc;

            Current = state;
        }
    }
}
