using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;

namespace Silverhand.PL.Controllers.Admin
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _service;

        public PlansController(IPlanService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanRequest request)
        {
            var response = await _service.CreateAsync(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PlanRequest request)
        {
            var result = await _service.UpdateAsync(id, request);
            return result == 0 ? NotFound() : Ok();
        }

       

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var result = await _service.DeactivateAsync(id);
            return result == 0 ? NotFound() : Ok();
        }
    }
}
