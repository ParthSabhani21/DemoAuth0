using DemoAuth0.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoAuth0.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> UserRegistration(string name, string email, string pass)
    {
        await _userService.RegisterUser(name, email, pass);

        return Ok("User Registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> UserLogin(string email, string pass)
    {
        var token = await _userService.UserLogin(email, pass);

        return Ok(token);
    }

    [HttpGet("GetCurrentUser")]
    [Authorize]
    public IActionResult GetCurrentUser()
    {
        var ssoIdentifier = User.Claims.FirstOrDefault(x => x.Properties.FirstOrDefault().Value == "sub").Value;
        
        return Ok(ssoIdentifier);
    }
}
