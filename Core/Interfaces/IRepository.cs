using Core.Models;

namespace DataAccess.Repository
{
    public interface IRepository
    {
        Task<Guid> Add(Anime anime);
        Task<Guid> Delete(Guid id);
        Task<IEnumerable<Anime>> GetAll();
        Task<Guid> Update(Anime anime);
        Task<Anime> GetById(Guid id);
        Task<(IEnumerable<Anime> animes, int count)> GetAsync(
            string search, string sortBy, bool sortDes, int page, int pageSize);
    }
}