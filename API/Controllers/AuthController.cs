using Application.Interfaces;
using Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ModuloCadastro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginHandler _loginHandler;
    private readonly ICreateUserCredentialsHandler _createUserCredentialsHandler;
    private readonly IValidateJwtTokenHandler _validateJwtTokenHandler;
    
    public AuthController(ILoginHandler loginHandler, ICreateUserCredentialsHandler createUserCredentialsHandler, 
        IValidateJwtTokenHandler validateJwtTokenHandler)
    {
        _loginHandler = loginHandler;
        _createUserCredentialsHandler = createUserCredentialsHandler;
        _validateJwtTokenHandler = validateJwtTokenHandler;
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] CreateCredentialsRequest request)
    {
        try
        {
            _createUserCredentialsHandler.Handle(request);
            return StatusCode(201, new { Message = "Credenciais criadas com sucesso." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = _loginHandler.Handle(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return Unauthorized(new { ex.Message });
        }
    }
    
    [Authorize]
    [HttpGet("validate")]
    public IActionResult Validate()
    {
        var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();

        if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer "))
            return Unauthorized(new { Message = "Token não informado." });

        var token = authorizationHeader.Replace("Bearer ", "");

        var result = _validateJwtTokenHandler.Handle(token);

        if (!result.IsValid)
            return Unauthorized(new { result.Message });

        return Ok(result);
    }
}