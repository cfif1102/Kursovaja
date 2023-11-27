namespace Lab_4.ViewModels.ApplicantCertificates
{
    public enum SortState
    {
        GradeAsc, GradeDesc,
        ApplicantAsc, ApplicantDesc,
    }

    public class ApplicantCertificatesSortViewModel
    {
        public SortState GradeSort { get; }

        public SortState ApplicantSort { get; }

        public SortState Current { get; }

        public ApplicantCertificatesSortViewModel(SortState state)
        {
            GradeSort = state == SortState.GradeAsc ? SortState.GradeDesc : SortState.GradeAsc;
            ApplicantSort = state == SortState.ApplicantAsc ? SortState.ApplicantDesc : SortState.ApplicantAsc;

            Current = state;
        }
    }
}
