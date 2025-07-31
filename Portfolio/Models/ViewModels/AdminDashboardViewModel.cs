namespace Portfolio.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<ContactMessage> Messages { get; set; }
    }
}
