using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
[Route("api/[controller]")]
public sealed class AssetsController : ApiController // Inherits from our custom base class
{
    private readonly IAssetService _assetService;

    public AssetsController(IAssetService assetService)
    {
        _assetService = assetService;
    }
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _assetService.GetByUserIdAsync(UserId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _assetService.GetByIdAsync(id, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssetRequest request, CancellationToken cancellationToken)
    {
        var result = await _assetService.CreateAsync(UserId, request, cancellationToken);

        // A Senior standard: Return 201 Created with a Location header pointing to the new resource
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
            : HandleFailure(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAssetRequest request, CancellationToken cancellationToken)
    {
        var result = await _assetService.UpdateAsync(id, UserId, request, cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _assetService.DeleteAsync(id, UserId, cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}
