using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.DTO.Updates;

namespace Silverhand.PL.Controllers.Admin
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ThumbnailsController : ControllerBase
    {
        private readonly IThumbmailService _service;

        public ThumbnailsController(IThumbmailService service)
        {
            _service = service;
        }
        [HttpPost("")]
        
        public async Task<IActionResult> Create([FromForm] ThumbnailRequest request)
        {
            var result = await _service.CreateFile(request);
            return Ok(result);
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateThumbnailRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok() : NotFound();
        }
        

    }
}
