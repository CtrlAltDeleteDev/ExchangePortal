// using System.Security.Claims;
// using Exchange.Portal.ApplicationCore.Features.User.Commands;
// using MediatR;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
//
// namespace Exchange.Portal.Web.Controllers;
//
// [AllowAnonymous]
// [ApiController]
// [Route("api/[controller]")]
// public class UserController : ControllerBase
// {
//     private readonly IMediator _mediator; 
//     
//     public UserController(IMediator mediator)
//     {
//         _mediator = mediator;
//     }
//     
//     [HttpPost("sign-in")]
//     public async Task<IActionResult> SignIn([FromBody]SignInCommand.User user)
//     {
//         await _mediator.Send(user);
//
//         // var claimsIdentity = new ClaimsIdentity(
//         //     new []{new Claim(ClaimTypes.Role, "Admin"), }, CookieAuthenticationDefaults.AuthenticationScheme);
//         //
//         // await HttpContext.SignInAsync(
//         //     CookieAuthenticationDefaults.AuthenticationScheme, 
//         //     new ClaimsPrincipal(claimsIdentity));
//         return Ok();
//     }
// }