using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;

namespace Silverhand.PL.Controllers.Viewer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Viewer")]
    [Authorize(Roles = "Customer")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _service;

        public AssetsController(IAssetService service)
        {
            _service = service;
        }

        // GET: api/assets
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assets = await _service.GetAllAsync(Request);
            return Ok(assets);
        }

        // GET: api/assets/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var asset = await _service.GetByIdAsync(id,Request);
            if (asset is null)
                return NotFound();

            return Ok(asset);
        }

      
    }

}
