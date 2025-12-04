using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        // -----------------------------------
        // CANCEL SUBSCRIPTION
        // -----------------------------------
        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel()
        {
            var idClaim = User.FindFirstValue("Id");
            if (idClaim == null)
                return Unauthorized("User ID missing in token.");

            var userId = Guid.Parse(idClaim);

            var result = await _subscriptionService.CancelAsync(userId);
            return Ok(new { canceled = result });
        }

        // -----------------------------------
        // GET CURRENT SUBSCRIPTION
        // -----------------------------------
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent()
        {
            var idClaim = User.FindFirstValue("Id");
            if (idClaim == null)
                return Unauthorized("User ID missing in token.");

            var userId = Guid.Parse(idClaim);

            var result = await _subscriptionService.GetCurrentAsync(userId);
            return Ok(result);
        }
    }
}

