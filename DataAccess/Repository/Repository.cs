
using Core.Models;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class Repository
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        private Anime ToModel(Entity entity) => Anime.Create(
            entity.Id,
            entity.Title,  
            entity.Rating,
            entity.Status.ToString(),
            entity.Genres
        ).anime;

        private Entity ToEntity(Anime model) => new Entity
        {
            Id = model.Id,
            Title = model.Title,
            Status = (Status)Enum.Parse(typeof(Status), model.Status),
            Rating = model.Rating,
            Genres = model.Genres,
        };

        public async Task<IEnumerable<Anime>> GetAllAsync()
        {
            var entities = await _context.Anime.ToListAsync();
            var objects = entities.Select(ToModel);
            return (objects);
        }
        public async Task<Anime> AddAsync(Anime anime)
        {
            var entities = ToEntity(anime);
            _context.Anime.Add(entities);
            await _context.SaveChangesAsync();
            return ToModel(entities);

        }

       /* public async Task<Anime> UpdateAsync(Anime anime)
        {
            var entities = await _context.Anime.FindAsync(anime.Id);
            if (entities == null) return null;
            entities.Title = anime.Title;
            entities.Status = (Status)Enum.Parse(typeof(Status), anime.Status);
            entities.Rating = anime.Rating;
            entities.Genres = anime.Genres;

            await _context.SaveChangesAsync();
            return ToModel(entities);
         }*/



    }
}
