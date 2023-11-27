namespace Lab_4.ViewModels.AdmissionApplications
{
    public enum SortState
    {
        ApplicationDateAsc, ApplicationDateDesc,
        ApplicantAsc, ApplicantDesc,
        SpecialityAsc, SpecialityDesc,
        AdmissionOfficerAsc, AdmissionOfficerDesc,
        OtherDetailsAsc, OtherDetailsDesc
    }

    public class AdmissionApplicationSortViewModel
    {
        public SortState ApplicationDateSort { get; }
        public SortState ApplicantSort { get; }
        public SortState SpecialitySort { get; }

        public SortState AdmissionOfiicerSort { get; }

        public SortState OtherDetailsSort { get; }

        public SortState Current { get; }

        public AdmissionApplicationSortViewModel(SortState state)
        {
            ApplicationDateSort = state == SortState.ApplicationDateAsc ? SortState.ApplicationDateDesc : SortState.ApplicationDateAsc;
            ApplicantSort = state == SortState.ApplicantAsc ? SortState.ApplicantDesc : SortState.ApplicantAsc;
            SpecialitySort = state == SortState.SpecialityAsc ? SortState.SpecialityDesc : SortState.SpecialityAsc;
            AdmissionOfiicerSort = state == SortState.AdmissionOfficerAsc ? SortState.AdmissionOfficerDesc : SortState.AdmissionOfficerAsc;
            OtherDetailsSort = state == SortState.OtherDetailsAsc ? SortState.OtherDetailsDesc : SortState.OtherDetailsAsc;

            Current = state;
        }
    }
}
