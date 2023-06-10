using System.Security.Claims;
using BLL.Interfaces;
using Core.CredentialModels;
using Core.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TastyTableAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;

    public AuthController(IConfiguration configuration, ILogger<AuthController> logger,
        IUserService userService, IAuthService authService)
    {
        _configuration = configuration;
        _logger = logger;
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("login", Name = "Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<User> Login([FromBody]LogInModel logInModel)
    {
        var user = _authService.LogIn(logInModel);
        
        if (user == null)
        {
            return NotFound("We didn't find such user");
        }

        var jwtTokens = _authService.GenerateJwtTokens(user);

        var response = new
        {
            access_token = jwtTokens.AccessToken,
            userId = user.Id,
            userEmail = user.Email
        };
        
        return Ok(response);
    }
    
    [HttpPost("register", Name = "Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult Register([FromBody]RegisterUserModel registerModel)
    {
        var user = _authService.RegisterUser(registerModel);
        
        var jwtTokens = _authService.GenerateJwtTokens(user);
        
        var response = new
        {
            access_token = jwtTokens.AccessToken,
            userId = user.Id,
            userEmail = user.Email
        };
        HttpContext.Response.ContentType = "application/json";
        return Ok(response);
    }

    [Authorize]
    [HttpGet("test")]
    public IActionResult Test()
    {
        Console.WriteLine("CLAIMS:");
        foreach (var v in User.Claims)
            Console.Write(v.Type + " " + v.Value + ", ");
        Console.WriteLine();
        
        return Ok(new {message="Success Auth", 
            Id=this.User.FindFirstValue(ClaimTypes.NameIdentifier)});
    }
    
    [HttpGet("testNoAuth")]
    public IActionResult TestNotAuth()
    {
        Console.WriteLine("CLAIMS:");
        foreach (var v in User.Claims)
            Console.Write(v.Type + " " + v.Value + ", ");
        Console.WriteLine();
        
        return Ok(new {message="Success No Auth"});
    }
    
    [HttpPost("signinGoogle", Name = "SignInGoogle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    public ActionResult SignInGoogle()
    {
        Console.WriteLine("CLAIMS:");
        foreach (var v in User.Claims)
            Console.Write(v.Type + " " + v.Value + ", ");
        Console.WriteLine();
        // var user = _authService.LogIn(logInModel);
        //
        // if (user == null)
        // {
        //     return NotFound("We didn't find such user");
        // }
        //
        // var jwtTokens = _authService.GenerateJwtTokens(user);
        //
        // var response = new
        // {
        //     access_token = jwtTokens.AccessToken,
        //     userId = user.Id,
        //     userEmail = user.Email
        // };
        //
        // return Ok(response);

        return Ok();
    }
}