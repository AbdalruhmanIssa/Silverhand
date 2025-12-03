using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using Stripe;

namespace Silverhand.PL.Controllers.Admin
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeService _service;

        public EpisodesController(IEpisodeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EpisodeRequest request)
        {
            var created = await _service.CreateAsync(request);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EpisodeRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            return updated == 0 ? NotFound() : Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted == 0 ? NotFound() : Ok();
        }
    }
}
