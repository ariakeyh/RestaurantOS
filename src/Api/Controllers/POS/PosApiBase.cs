using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.POS;
[Authorize]
[ApiController]
[Route("api/pos/[controller]")]
public class PosApiBase : ControllerBase;