﻿namespace Lab_4.ViewModels.Specialities
{
    public enum SortState
    {
        NameAsc, NameDesc,
        FacultyAsc, FacultyDesc,
        CountAsc, CountDesc,
    }

    public class SpecialitiesSortViewModel
    {
        public SortState NameSort { get; set; }

        public SortState FacultySort { get; set; }

        public SortState CountSort { get; set; }

        public SortState Current { get; set; }

        public SpecialitiesSortViewModel(SortState state) 
        {
            NameSort = state == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            FacultySort = state == SortState.FacultyAsc ? SortState.FacultyDesc : SortState.FacultyAsc;
            CountSort = state == SortState.CountAsc ? SortState.CountDesc : SortState.CountAsc;

            Current = state;
        }
    }
}
