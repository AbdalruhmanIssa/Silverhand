using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using System.Security.Claims;

namespace Silverhand.PL.Controllers.Admin
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        // -----------------------------------
        // SUBSCRIBE
        // -----------------------------------
        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequest request)
        {
            var idClaim = User.FindFirstValue("Id");
            if (idClaim == null)
                return Unauthorized("User ID missing in token.");

            var userId = Guid.Parse(idClaim);

            var result = await _subscriptionService.SubscribeAsync(userId, request);
            return Ok(result);
        }
    }
}
