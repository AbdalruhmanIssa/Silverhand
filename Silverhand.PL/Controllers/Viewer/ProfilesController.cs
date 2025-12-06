using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using System.Security.Claims;


namespace Silverhand.PL.Controllers.Viewer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Viewer")]
    [Authorize(Roles = "Customer")]

    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProfileRequest request)
        {
            var idClaim = User.FindFirstValue("Id");//extracting the claim named "Id"

            if (idClaim == null)
                return Unauthorized("User ID missing in token.");

            var userId = Guid.Parse(idClaim);

            var result = await _profileService.CreateProfileAsync(userId, request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProfiles()
        {
            var idClaim = User.FindFirstValue("Id");   // <-- same claim we fixed
            if (idClaim == null)
                return Unauthorized("User ID missing from token.");

            var userId = Guid.Parse(idClaim);

            var profiles = await _profileService.GetAllByUserAsync(userId);

            return Ok(profiles);
        }

    }
}
