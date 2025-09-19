using Anime_Collection.Contracts;
using BusinessLogic.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anime_Collection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly IService _service;
        public Controller(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Response>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            var response = entities.Select(x => new Response(x.Id, x.Title, x.Status, x.Rating,  x.Genres));
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(Request request)
        {
            var (anime, error) = Anime.Create(
                Guid.NewGuid(),
                request.Title,
                request.Status,
                request.Rating,
                request.Genres
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            var response = await _service.AddAsync(anime);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> Delete(Guid id)
        {
            var response = await _service.DeleteAsync(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> Update (Guid id, Request request)
        {
            var entity = await _service.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            var (anime, error) = Anime.Create(Guid.NewGuid(), request.Title, request.Status, request.Rating, request.Genres);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            var response = await _service.UpdateAsync(entity);
            return Ok(response);

        }

    }
}
