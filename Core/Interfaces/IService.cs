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
    }
}