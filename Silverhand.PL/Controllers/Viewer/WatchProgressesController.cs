using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;

namespace Silverhand.PL.Controllers.Viewer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Viewer")]
    [Authorize(Roles = "Customer")]
    public class WatchProgressesController : ControllerBase
    {
        private readonly IWatchProgressService _service;

        public WatchProgressesController(IWatchProgressService service)
        {
            _service = service;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(WatchProgressRequest request)
        {
            return Ok(await _service.UpsertAsync(request));
        }

        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetForProfile(Guid profileId)
        {
            return Ok(await _service.GetForProfileAsync(profileId));
        }
    }

}
