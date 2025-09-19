namespace Anime_Collection.Contracts
{
    public record Request(string Title, string Status, double Rating, List<string> Genres);
}
