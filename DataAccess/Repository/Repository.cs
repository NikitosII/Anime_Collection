
using Core.Models;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        private Anime ToModel(Entity entity) => Anime.Create(
            entity.Id,
            entity.Title,
            entity.Status.ToString(),
            entity.Rating,
            entity.Genres
        ).anime;

        private Entity ToEntity(Anime model) => new Entity
        {
            Id = model.Id,
            Title = model.Title,
            Status = (Statuses)Enum.Parse(typeof(Statuses), model.Status),
            Rating = model.Rating,
            Genres = model.Genres,
        };

        public async Task<IEnumerable<Anime>> GetAll()
        {
            var entities = await _context.Anime.ToListAsync();
            var objects = entities.Select(ToModel);
            return (objects);
        }
        public async Task<Guid> Add(Anime anime)
        {
            var entities = ToEntity(anime);
            await _context.Anime.AddAsync(entities);
            await _context.SaveChangesAsync();
            return entities.Id;

        }

        public async Task<Guid> Update(Anime anime)
        {
            var entities = await _context.Anime
                .Where(x => x.Id == anime.Id)
                .ExecuteUpdateAsync(s => s
                   .SetProperty(b => b.Title, b => anime.Title)
                   .SetProperty(b => b.Status, b => (Statuses)Enum.Parse(typeof(Statuses), anime.Status))
                    .SetProperty(b => b.Rating, b => anime.Rating)
                    .SetProperty(b => b.Genres, b => anime.Genres));

            if (entities == 0)
            {
                throw new KeyNotFoundException($"Book with ID {anime.Id} not found.");
            }
            return anime.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var entities = await _context.Anime.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (entities == 0)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
            return id;
        }

        public async Task<Anime> GetById(Guid id)
        {
            var entity = await _context.Anime.FindAsync(id);
            return entity != null ? ToModel(entity) : null;
        }

        public async Task<(IEnumerable<Anime> animes, int count)> GetAsync(string title, string status, string genres, string sortBy, bool sortDesc, int page, int pageSize)
        {
            var query = _context.Anime.AsQueryable();

            // по названию
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(a => a.Title.ToLower().Contains(title.ToLower()));
            }

            // по жанрам
            if (!string.IsNullOrEmpty(genres))
            {
                var list = genres.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(a => a.Trim().ToLower())
                    .ToList();

                foreach (var item in list)
                {
                    query = query.Where(a => a.Genres.Any(x => x.ToLower() == genres));
                }
            }

            // по статусу
            if (!string.IsNullOrEmpty(status) && Enum.TryParse<Statuses>(status, true, out var status_v))
            {
                query = query.Where(a => a.Status == status_v);
            }

            var count = await query.CountAsync();

            // сортировка собств
            query = sortBy.ToLower() switch
            {
                "title" => sortDesc ? query.OrderByDescending(a => a.Title) : query.OrderBy(a => a.Title),
                "rating" => sortDesc ? query.OrderByDescending(a => a.Rating) : query.OrderBy(a => a.Rating),
                "status" => sortDesc ? query.OrderByDescending(a => a.Status) : query.OrderBy(a => a.Status)
            };

            // пагинацию сделать
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var entities = await query.ToListAsync();
            var animes = entities.Select(ToModel);
            return (animes, count);

        }
    }
}
