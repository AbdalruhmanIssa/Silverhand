using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Stripe;

namespace Silverhand.PL.Controllers.Viewer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Viewer")]
    [Authorize(Roles = "Customer")]
    public class ThumbnailsController : ControllerBase
    {
        private readonly IThumbmailService _service;

        public ThumbnailsController(IThumbmailService service)
        {
            _service = service;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdThumbnail(id, Request);

            return result == null ? NotFound() : Ok(result);
        }
    }
}
