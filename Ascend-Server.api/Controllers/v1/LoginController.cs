using Ascend_Server.api.ActionFilters;
using Data;
using IServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ServiceFilter(typeof(ModelStateActionFilter))]
[ApiVersion("1.0")]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }
    /// <summary>
    /// Authenticates a user with their email and password, and returns an access token.
    /// </summary>
    /// <param name="request">The login request containing the user's email and password.</param>
    /// <returns>A <see cref="LoginResponse"/> object containing an access token if the user is authenticated.</returns>
    /// <response code="200">Returns a <see cref="LoginResponse"/> object containing an access token.</response>
    /// <response code="401">Returns an unauthorized error if the user is not authenticated.</response>
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