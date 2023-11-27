namespace Lab_4.ViewModels.Parents
{
    public class ParentsFilterViewMode
    {
        public string Parent1 { get; }

        public string Parent2 { get; }

        public ParentsFilterViewMode(string parent1, string parent2)
        {
            Parent1 = parent1;
            Parent2 = parent2;
        }
    }
}
