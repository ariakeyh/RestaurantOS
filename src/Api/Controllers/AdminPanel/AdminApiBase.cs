using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;
[Authorize]
[ApiController]
[Route("api/admin-panel/[controller]")]
public class AdminApiBase : ControllerBase;