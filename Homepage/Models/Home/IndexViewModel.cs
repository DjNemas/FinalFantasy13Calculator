namespace Homepage.Models.Home
{
    public class IndexViewModel
    {
        public string? Message { get; set; }
        public bool IsSuccessMessage { get; set; } = false;
        public List<IndexNavigationContent> NavigationContent { get; set; } = new List<IndexNavigationContent>();
    }
}
