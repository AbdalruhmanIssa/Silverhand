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
    
    public class AvailabilityWindowsController : ControllerBase
    {
        private readonly IAvailabilityWindowService _service;

        public AvailabilityWindowsController(IAvailabilityWindowService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AvailabilityWindowRequest request)
        {
            var result = await _service.CreateAsync(request);
            return Ok(result);
        }

       
    }

}
