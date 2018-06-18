namespace Searchfight.SL.Models
{
    public class GoogleModel
    {
        public string Kind { get; set; }
        public SearchInformation SearchInformation { get; set; }
    }

    public class SearchInformation
    {
        public string FormattedSearchTime { get; set; }
        public string TotalResults { get; set; }
    }
}
