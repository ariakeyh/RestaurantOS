using Application.Services;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Webhooks;

public class PgVerificationWebhookController (InternalPaymentService paymentService) : WebhookBase
{
    [HttpPost("verify")]
    public async Task<IActionResult> VerifyPayment(
        [FromQuery] string authority,
        CancellationToken cancellationToken)
    {
        try
        {
            await paymentService.VerifyPaymentLink(authority, cancellationToken);
            return Ok(new { message = "Payment verified successfully" });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", details = ex.Message });
        }
    }
}