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
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // ---------------------------------------------------------
        //  START PAYMENT (Create Stripe Checkout Session)
        // ---------------------------------------------------------

       
        [HttpPost("start")]
        public async Task<IActionResult> Start([FromBody] PaymentRequest request)
        {
            var idClaim = User.FindFirstValue("Id");

            if (idClaim == null)
                return Unauthorized("User ID missing in token.");

            Guid userId = Guid.Parse(idClaim);

            var response = await _paymentService.StartAsync(request, userId, Request);

            return Ok(response);
        }


        // ---------------------------------------------------------
        //  SUCCESS CALLBACK (Stripe redirects here)
        // ---------------------------------------------------------
        [HttpGet("success/{subscriptionId:guid}")]
        public async Task<IActionResult> Success(Guid subscriptionId)
        {
            var success = await _paymentService.HandleSuccessAsync(subscriptionId);

            if (!success)
                return BadRequest("Failed to activate subscription.");

            return Ok("Subscription activated successfully.");
        }
    }
}
