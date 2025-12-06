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
        private Guid GetUserId()
        {
            var id = User.FindFirstValue("Id");
            return Guid.Parse(id);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProfileRequest request)
        {
            var userId = GetUserId();

            var result = await _profileService.CreateProfileAsync(userId, request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProfiles()
        {
            var userId = GetUserId();

            var profiles = await _profileService.GetAllByUserAsync(userId);

            return Ok(profiles);
        }
        // UPDATE ---------------------------------------
        [HttpPut("{profileId}")]
        public async Task<IActionResult> Update(Guid profileId, [FromForm] ProfileRequest request)
        {
            var userId = GetUserId();

            var updated = await _profileService.UpdateAsync(profileId, request, userId);
            return Ok(updated);
        }

        // DELETE ---------------------------------------
        [HttpDelete("{profileId}")]
        public async Task<IActionResult> Delete(Guid profileId)
        {
            var userId = GetUserId();

            var result = await _profileService.DeleteAsync(profileId, userId);
            if (!result)
                return NotFound("Profile not found.");

            return Ok("Profile deleted successfully.");
        }

    }
}
