using Core.Models;

namespace BusinessLogic.Services
{
    public interface IService
    {
        Task<Guid> AddAsync(Anime anime);
        Task<Guid> DeleteAsync(Guid id);
        Task<IEnumerable<Anime>> GetAllAsync();
        Task<Guid> UpdateAsync(Anime anime);
        Task<Anime> GetById(Guid id);
        Task<(IEnumerable<Anime> animes, int count)> GetWithFiltr
            (string title, string status, string genres, string sortBy, bool sortDesc, int page, int pageSize);
        }
}