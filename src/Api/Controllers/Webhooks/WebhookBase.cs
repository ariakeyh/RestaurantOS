using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Webhooks;
[Authorize]
[ApiController]
[Route("webhook/[controller]")]
public class WebhookBase : ControllerBase;