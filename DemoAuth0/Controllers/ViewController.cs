using DemoAuth0.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoAuth0.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ViewController : ControllerBase
{
    private readonly IUserService _userService;

    public ViewController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("getUsers")]
    public async Task GetUsers()
    {
        await _userService.GetUser();
    }
}
