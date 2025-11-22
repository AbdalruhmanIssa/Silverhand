using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;

namespace Silverhand.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeService _service;

        public EpisodesController(IEpisodeService service)
        {
            _service = service;
        }

        // GET: api/episodes
        [HttpGet]
        public IActionResult GetAll()
        {
            var episodes = _service.GetAll();
            return Ok(episodes);
        }

        // GET: api/episodes/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var episode = _service.GetById(id);
            if (episode == null)
                return NotFound();

            return Ok(episode);
        }

        // POST: api/episodes
        [HttpPost]
        public IActionResult Create([FromBody] EpisodeRequest request)
        {
            var created = _service.Create(request);
            return CreatedAtAction(nameof(GetById), new {id= created }, new { message = request }); // returns number of rows affected (int)
        }

        // PUT: api/episodes/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] EpisodeRequest request)
        {
            var updated = _service.Update(id, request);
            return updated > 0 ? Ok() : NotFound(); 
        }

        // DELETE: api/episodes/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deleted = _service.Delete(id);
            return deleted > 0 ? Ok() : NotFound(); // returns int
        }
    }

}
