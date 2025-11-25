using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.Repository.Repositories;
using System.Threading.Tasks;

namespace Silverhand.PL.Controllers.Controller
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class TitlesController : ControllerBase
    {
        private readonly ITitlesService _productService;

        public TitlesController(ITitlesService productService)
        {
            _productService = productService;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] TitleRequest request)
        {
            var result = await _productService.CreateFile(request);
            return Ok(result);
        }
        [HttpGet("")]
        public  async Task<IActionResult> GetAll(
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 5)
        {
            var products =await _productService.GetTitles(Request, false, pageNumber, pageSize);
            return Ok(products);
        }
    }
}
