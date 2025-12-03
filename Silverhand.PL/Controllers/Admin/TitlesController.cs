using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Classes;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Updates;

namespace Silverhand.PL.Controllers.Admin
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class TitlesController : ControllerBase
    {
        private readonly ITitlesService _titlesService;

        public TitlesController(ITitlesService titlesService)
        {
            _titlesService = titlesService;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] TitleRequest request)
        {
            var result = await _titlesService.CreateFile(request);
            return Ok(result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateTitleRequest request)
        {
            var response = await _titlesService.UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _titlesService.GetByIdAsync(id);

            if (entity is null)
                return NotFound("Title not found");

            var result = await _titlesService.DeleteAsync(id);

            if (!result)
                return BadRequest("Failed to delete title");

            return Ok("Title deleted successfully");
        }

    }
}
