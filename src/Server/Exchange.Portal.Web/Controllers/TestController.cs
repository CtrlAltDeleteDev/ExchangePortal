using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exchange.Portal.Web.Controllers;

[ApiController]
[Route("api")]
public class TestController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("test")]
    public IActionResult Get()
    {
        return Ok("Hey!");
    }
    
    [Authorize]
    [HttpGet("test-auth")]
    public IActionResult GetAuth()
    {
        return Ok("I'm in!");
    }
}