namespace Portfolio.Models.ViewModels
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public Func<int, string> GetPageUrl { get; set; }
    }

}
