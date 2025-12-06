using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Classes;
using Silverhand.BLL.Services.Interface;

namespace Silverhand.PL.Controllers.Viewer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Viewer")]
    [Authorize(Roles = "Customer")]
    public class SubtitlesController : ControllerBase
    {
        private readonly ISubtitleService _service;

        public SubtitlesController(ISubtitleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid titleId, Guid? episodeId)
        {
            var result = await _service.GetAllAsync(titleId, episodeId);
            return Ok(result);
        }

    }
}
