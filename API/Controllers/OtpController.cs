using Application.Interfaces;
using Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ModuloCadastro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OtpController : ControllerBase
{
    private readonly IGenerateOtpHandler _generateOtpHandler;
    private readonly IValidateOtpHandler _validateOtpHandler;

    public OtpController(IGenerateOtpHandler generateOtpHandler, IValidateOtpHandler validateOtpHandler)
    {
        _generateOtpHandler = generateOtpHandler;
        _validateOtpHandler = validateOtpHandler;
    }

    [HttpPost("generate")]
    public IActionResult Generate([FromBody] GenerateOtpRequest request)
    {
        _generateOtpHandler.Handle(request);
        return NoContent();
    }

    [HttpPost("validate")]
    public IActionResult Validate([FromBody] ValidateOtpRequest request)
    {
        var response = _validateOtpHandler.Handle(request);
        if (!response.IsValid)
            return BadRequest(response);

        return Ok(response);
    }
}