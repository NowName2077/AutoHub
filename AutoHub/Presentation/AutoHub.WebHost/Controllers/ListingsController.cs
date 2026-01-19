using AutoHub.Application.Models.Listing;
using AutoHub.Application.Services.Abstractions;
using AutoHub.WebHost.Requests;
using AutoHub.WebHost.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.WebHost.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ListingsController : ControllerBase
{
    private readonly IListingsApplicationService _service;
    private readonly IMapper _mapper;

    public ListingsController(IListingsApplicationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var listings = await _service.GetListingsAsync(ct);
        return Ok(_mapper.Map<IEnumerable<ListingShortResponse>>(listings));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var listing = await _service.GetListingByIdAsync(id, ct);
        if (listing is null) return NotFound();
        return Ok(_mapper.Map<ListingShortResponse>(listing));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateListingRequest request, CancellationToken ct)
    {
        var model = _mapper.Map<CreateListingModel>(request);
        var created = await _service.CreateListingAsync(model, ct);
        if (created is null) return BadRequest();
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<ListingShortResponse>(created));
    }
}