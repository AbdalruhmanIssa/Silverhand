using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;

namespace Silverhand.PL.Controllers.Viewer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class AvailabilityWindowsController : ControllerBase
    {
        private readonly IAvailabilityWindowService _service;

        public AvailabilityWindowsController(IAvailabilityWindowService service)
        {
            _service = service;
        }

     
      

        [HttpGet("title/{titleId:guid}")]
        public async Task<IActionResult> GetForTitle(Guid titleId)
        {
            var result = await _service.GetForTitleAsync(titleId);
            return Ok(result);
        }

        [HttpGet("episode/{episodeId:guid}")]
        public async Task<IActionResult> GetForEpisode(Guid episodeId)
        {
            var result = await _service.GetForEpisodeAsync(episodeId);
            return Ok(result);
        }
    }

}
