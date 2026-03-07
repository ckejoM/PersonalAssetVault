using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
public class AuthController : ApiController
{
    private readonly IAuthService _authService;
    private readonly IJwtProvider _jwtProvider;
    public AuthController(IAuthService authService, IJwtProvider jwtProvider)
    {
        _authService = authService;
        _jwtProvider = jwtProvider;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.ValidateCredentialsAsync(request, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        var token = _jwtProvider.Generate(result.Value.Id, result.Value.Email);

        return Ok(new { User = result.Value, Token = token });
    }
}
