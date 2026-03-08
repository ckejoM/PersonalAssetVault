using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;
[Authorize]
[Route("/api/[controller]")]
public class CategoriesController : ApiController
{
    private readonly ICategoryService _categoryService;
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var randGuid = Guid.NewGuid();

        var result = await _categoryService.GetAllAsync(UserId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
}
