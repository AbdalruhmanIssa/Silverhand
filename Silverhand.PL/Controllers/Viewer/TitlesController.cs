using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Classes;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.Repository.Repositories;
using System.Threading.Tasks;

namespace Silverhand.PL.Controllers.Controller
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Viewer")]
    [Authorize(Roles = "Customer")]
    public class TitlesController : ControllerBase
    {
        private readonly ITitlesService _titlesService;

        public TitlesController(ITitlesService titlesService)
        {
            _titlesService = titlesService;
        }
        
        [HttpGet("")]
        public  async Task<IActionResult> GetAll(
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 5)
        {
            var products =await _titlesService.GetTitles(Request, false, pageNumber, pageSize);
            return Ok(products);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var title = await _titlesService.GetByIdTitle(id, Request);

            if (title == null)
                return NotFound("Title not found");

            return Ok(title);
        }

    }
}
