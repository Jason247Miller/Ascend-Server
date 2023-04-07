using Data;
using IServices;
using Services;
using Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Dto;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Post(LoginRequest request)
    {
        User? user;

        try
        {
            user = _userService.FindUserByEmail(request.Email!);

            var isAuthorized = _userService.VerifyPassword(user!, request.Password!);

            if (!isAuthorized)
            {
                throw new Exception();
            }

        }
        catch (Exception)
        {
            return Unauthorized();
        }

        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

        var loginResponse = new LoginResponse
        {
            Token = token
        };

        return Ok(loginResponse);
    }
}