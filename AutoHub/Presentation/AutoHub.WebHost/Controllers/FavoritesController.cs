using AutoHub.Application.Services.Abstractions;
using AutoHub.WebHost.Responses.Favorite;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.WebHost.Controllers;

[ApiController]
[Route("api/v1/customers/{customerId:guid}/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly IFavoritesApplicationService _service;
    private readonly IMapper _mapper;

    public FavoritesController(IFavoritesApplicationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetFavorites(Guid customerId, CancellationToken ct)
    {
        var favorites = await _service.GetFavoritesAsync(customerId, ct);
        return Ok(_mapper.Map<IEnumerable<FavoriteResponse>>(favorites));
    }

    [HttpPost("{listingId:guid}")]
    public async Task<IActionResult> AddFavorite(Guid customerId, Guid listingId, CancellationToken ct)
    {
        var ok = await _service.AddFavoriteAsync(customerId, listingId, ct);
        return ok ? NoContent() : BadRequest("Cannot add favorite");
    }

    [HttpDelete("{listingId:guid}")]
    public async Task<IActionResult> RemoveFavorite(Guid customerId, Guid listingId, CancellationToken ct)
    {
        var ok = await _service.RemoveFavoriteAsync(customerId, listingId, ct);
        return ok ? NoContent() : BadRequest("Cannot remove favorite");
    }
}