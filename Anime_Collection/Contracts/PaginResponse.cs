namespace Anime_Collection.Contracts
{
    public record PaginResponse<T> (IEnumerable<T> Data, int Page, int PageSize, int TotalCount,int TotalPages );

}
