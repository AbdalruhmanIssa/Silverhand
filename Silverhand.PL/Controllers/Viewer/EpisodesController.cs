using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;

namespace Silverhand.PL.Controllers.Viewer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Viewer")]
    [Authorize(Roles = "Customer")]

    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeService _service;

        public EpisodesController(IEpisodeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
    [FromQuery] Guid titleId,
    [FromQuery] int? seasonNumber)
        {
            if (titleId == Guid.Empty)
                return BadRequest("titleId is required");

            IEnumerable<EpisodeResponse> episodes;

            if (seasonNumber.HasValue)
            {
                // filter by season
                episodes = await _service.GetWhere(
                    e => e.TitleId == titleId && e.SeasonNumber == seasonNumber.Value
                );
            }
            else
            {
                // get all episodes
                episodes = await _service.GetWhere(
                    e => e.TitleId == titleId
                );
            }

            if (episodes == null || !episodes.Any())
                return NotFound("No episodes found for this title/season");

            return Ok(episodes);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

       
    }


}
