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
    public class InjestJobsController : ControllerBase
    {
        private readonly IIngestJobService _service;

        public InjestJobsController(IIngestJobService service)
        {
            _service = service;
        }
        [HttpPost("")]
        
        public async Task<IActionResult> Create([FromForm] IngestJobRequest request)
        {
            var result = await _service.CreateFile(request);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateIngestJobRequest request)
        {
            var result = await _service.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);

            if (!ok)
                return NotFound(new { message = "Ingest job not found" });

            return Ok(new { message = "Ingest job deleted successfully" });
        }


    }
}
