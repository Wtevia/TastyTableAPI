using System.Security.Claims;
using BLL.Interfaces;
using Core;
using Core.CredentialModels;
using Core.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TastyTableAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;
    private readonly UserManager<User> _userManager;

    public AuthController(IConfiguration configuration, ILogger<AuthController> logger,
        IAuthService authService, UserManager<User> userManager)
    {
        _configuration = configuration;
        _logger = logger;
        _authService = authService;
        _userManager = userManager;
    }

    [HttpPost("login", Name = "Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<JwtTokens>> Login([FromBody]LogInPassword logInPassword)
    {
        var user = await _authService.PasswordLogIn(logInPassword);
        
        if (user == null)
        {
            return NotFound("We didn't find such user");
        }

        var jwtTokens = await _authService.GenerateJwtTokens(user);

        return Ok(jwtTokens);
    }
    
    [HttpPost("register", Name = "Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<JwtTokens>> Register([FromBody]RegisterUserModel registerModel)
    {
        var user = await _authService.RegisterUser(registerModel);
        
        var jwtTokens = await _authService.GenerateJwtTokens(user);
        HttpContext.Response.ContentType = "application/json";
        
        return Ok(jwtTokens);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("test")]
    public IActionResult Test()
    {
        Console.WriteLine("CLAIMS: ");
        foreach (var v in User.Claims)
            Console.Write(v.Type + " " + v.Value + ", ");
        Console.WriteLine();

        return Ok(new {message="Success Auth", 
            Id=this.User.FindFirstValue(ClaimTypes.NameIdentifier)});
    }
    
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    [HttpGet("testGoogle")]
    public IActionResult TestGoogle()
    {
        Console.WriteLine("CLAIMS: ");
        foreach (var v in User.Claims)
            Console.Write(v.Type + " " + v.Value + ", ");
        Console.WriteLine();

        return Ok(new {message="Success Google Auth", 
            Id=this.User.FindFirstValue(ClaimTypes.NameIdentifier)});
    }
    
    [HttpGet("testNoAuth")]
    public IActionResult TestNotAuth()
    {
        Console.WriteLine("CLAIMS: ");
        foreach (var v in User.Claims)
            Console.Write(v.Type + " " + v.Value + ", ");
        Console.WriteLine();
        
        return Ok(new {message="Success No Auth"});
    }
    
    /// <summary>
    /// Endpoint to verify access token received from frontend,
    /// when using fronted based external socials auth 
    /// </summary>
    /// <param name="socialLogin"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("verifySocialToken", Name = "LoginViaSocial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<JwtTokens>> LoginViaSocialToken([FromBody] SocialLogin socialLogin)
    {
        var validationResult = (await _authService.ValidateSocialToken(socialLogin)).Value;
        var user = await _authService.SocialLoginOrRegister(socialLogin, validationResult);
        var jwtTokens = await _authService.GenerateJwtTokens(user);

        return Ok(jwtTokens);
    }
    
    /*
     * Login using external socials and server side based auth
     */
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    [HttpGet("socialLogin")]
    public async Task<ActionResult<JwtTokens>> GetJwtTokenFromSocialAuth(
        string? role, string? provider)
    {
        if (!Consts.LoginProviders.IsValid(provider))
            return BadRequest("Invalid provider");
        if (!Consts.UserRoles.IsValid(role))
            return BadRequest("Invalid role");
        Console.WriteLine("Google login role: " + role);
        
        var socialLogin = new SocialLogin()
        {
            Role = role,
            Provider = provider
        };
        var validationResult = new SocialTokenValidationResult()
        {
            Email = User.FindFirst(
                c => c.Type == ClaimTypes.Email).Value,
            Name = User.FindFirst(
                c => c.Type == ClaimTypes.Name).Value,
            Provider = Consts.LoginProviders.Google,
            ProviderKey = User.FindFirst(
                c => c.Type == ClaimTypes.NameIdentifier).Value
        };
        var user = await _authService.SocialLoginOrRegister(socialLogin, validationResult);
        var jwtTokens = await _authService.GenerateJwtTokens(user);

        return Ok(jwtTokens);
    }
}