using AutoHub.Application.Models.Customer;
using AutoHub.Application.Services.Abstractions;
using AutoHub.WebHost.Requests.Customer;
using AutoHub.WebHost.Responses.Customer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.WebHost.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomersApplicationService _service;
    private readonly IMapper _mapper;

    public CustomersController(ICustomersApplicationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var customers = await _service.GetCustomersAsync(ct);
        return Ok(_mapper.Map<IEnumerable<CustomerShortResponse>>(customers));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request, CancellationToken ct)
    {
        var model = _mapper.Map<CreateCustomerModel>(request);
        var created = await _service.CreateCustomerAsync(model, ct);
        if (created is null) return BadRequest();
        return CreatedAtAction(nameof(GetAll), null, _mapper.Map<CustomerShortResponse>(created));
    }
}