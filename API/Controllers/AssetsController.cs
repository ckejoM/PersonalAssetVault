using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public sealed class AssetsController : ApiController // Inherits from our custom base class
{
    private readonly IAssetService _assetService;

    // TODO (Phase 4): We will extract this dynamically from the JWT User Claims.
    private readonly Guid _mockUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");

    public AssetsController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _assetService.GetByUserIdAsync(_mockUserId, cancellationToken);

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
        var result = await _assetService.CreateAsync(_mockUserId, request, cancellationToken);

        // A Senior standard: Return 201 Created with a Location header pointing to the new resource
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
            : HandleFailure(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAssetRequest request, CancellationToken cancellationToken)
    {
        var result = await _assetService.UpdateAsync(id, _mockUserId, request, cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _assetService.DeleteAsync(id, _mockUserId, cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}
