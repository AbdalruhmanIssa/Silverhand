using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;

namespace Silverhand.PL.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
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

    }
}
