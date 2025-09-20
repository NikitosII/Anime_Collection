namespace Anime_Collection.Contracts
{
    public class SearchParams
    {
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Genres { get; set; } = string.Empty;
        public string SortBy { get; set; } = string.Empty;
        public bool SortDesc { get; set; } = false;
        public int Page { get; set; } = 1;
        public int Count { get; set; } = 10;


    }
}
