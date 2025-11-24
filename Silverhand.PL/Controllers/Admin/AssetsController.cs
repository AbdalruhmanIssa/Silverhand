using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;

namespace Silverhand.PL.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
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
            var assets = await _service.GetAllAsync();
            return Ok(assets);
        }

        // GET: api/assets/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var asset = await _service.GetByIdAsync(id);
            if (asset is null)
                return NotFound();

            return Ok(asset);
        }

        // POST: api/assets
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AssetRequest request)
        {
            var created = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },   // Return the created resource's ID
                created                    // Return full DTO, not "message"
            );
        }

        // PUT: api/assets/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AssetRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);

            if (updated == 0)
                return NotFound();

            return Ok();
        }

        // DELETE: api/assets/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (deleted == 0)
                return NotFound();

            return Ok();
        }
    }

}
