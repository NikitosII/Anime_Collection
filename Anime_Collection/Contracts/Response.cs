namespace Anime_Collection.Contracts
{
    public record Response (Guid Id, string Title, string Status, double Rating, List<string> Genres);
    
}
