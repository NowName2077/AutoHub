using AutoHub.Application.Models.Seller;
using AutoHub.Application.Services.Abstractions;
using AutoHub.WebHost.Requests;
using AutoHub.WebHost.Requests.Seller;
using AutoHub.WebHost.Responses;
using AutoHub.WebHost.Responses.Seller;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.WebHost.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SellersController : ControllerBase
{
    private readonly ISellersApplicationService _service;
    private readonly IMapper _mapper;

    public SellersController(ISellersApplicationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var sellers = await _service.GetSellersAsync(ct);
        return Ok(_mapper.Map<IEnumerable<SellerShortResponse>>(sellers));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var seller = await _service.GetSellerByIdAsync(id, ct);
        if (seller is null) return NotFound();
        return Ok(_mapper.Map<SellerShortResponse>(seller));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSellerRequest request, CancellationToken ct)
    {
        var model = _mapper.Map<CreateSellerModel>(request);
        var created = await _service.CreateSellerAsync(model, ct);
        if (created is null) return BadRequest();
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<SellerShortResponse>(created));
    }
}