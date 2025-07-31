namespace Portfolio.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Technologies { get; set; }

        public string GitHubLink { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
