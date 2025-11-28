using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;

namespace Silverhand.PL.Controllers.Viewer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        // ----------------------------------------------------
        // ADD FAVORITE
        // ----------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequest request)
        {
            var result = await _favoriteService.AddAsync(request);
            if (!result)
                return BadRequest("Cannot add favorite. Check Profile/Title or duplicate.");

            return Ok("Added to favorites.");
        }

        // ----------------------------------------------------
        // REMOVE FAVORITE
        // ----------------------------------------------------
        [HttpDelete("{profileId:guid}/{titleId:guid}")]
        public async Task<IActionResult> RemoveFavorite(Guid profileId, Guid titleId)
        {
            var result = await _favoriteService.RemoveAsync(profileId, titleId);
            if (!result)
                return NotFound("Favorite not found.");

            return Ok("Removed from favorites.");
        }

        // ----------------------------------------------------
        // GET ALL FAVORITES FOR PROFILE
        // ----------------------------------------------------
        [HttpGet("{profileId:guid}")]
        public async Task<IActionResult> GetFavorites(Guid profileId)
        {
            var list = await _favoriteService.GetAllByProfileAsync(profileId);
            return Ok(list);
        }
    }
}
