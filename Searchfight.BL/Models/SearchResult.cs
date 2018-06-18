namespace Searchfight.BL.Models
{
    public class SearchResult
    {
        public string SearchEngine { get; set; }
        public string Term { get; set; }
        public long TotalResults { get; set; }
    }
}
