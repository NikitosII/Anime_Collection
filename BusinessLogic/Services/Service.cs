
using Core.Models;
using DataAccess.Repository;

namespace BusinessLogic.Services
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Anime>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<Guid> AddAsync(Anime anime)
        {
            return await _repository.Add(anime);
        }

        public async Task<Guid> UpdateAsync(Anime anime)
        {
            return await _repository.Update(anime);
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            return await _repository.Delete(id);
        }
        public async Task<Anime> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

    }
}
