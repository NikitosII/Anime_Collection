namespace Anime_Collection.Contracts
{
    public class SearchParams
    {
        public string Search { get; set; } = string.Empty;
        public string SortBy { get; set; } = "title"; // variants : title, genre, status
        public bool SortDesc { get; set; } = false;
        public int Page { get; set; } = 1;
        public int Count { get; set; } = 10;


    }
}
