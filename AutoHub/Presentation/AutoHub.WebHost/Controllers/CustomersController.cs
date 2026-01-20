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
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var customers = await _service.GetCustomersAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<CustomerShortResponse>>(customers));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _service.GetCustomerByIdAsync(id, cancellationToken);
        if (customer is null)
            return NotFound($"Customer with id:{id} not found");
        return Ok(_mapper.Map<CustomerDetailedResponse>(customer));
    }
    
    [HttpGet("{username}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetCustomerByUsernameId(string username, CancellationToken cancellationToken)
    {
        var customer = await _service.GetCustomerByUsernameAsync(username, cancellationToken);
        if (customer is null)
            return NotFound($"Customer with username:{username} not found");
        return Ok(_mapper.Map<CustomerDetailedResponse>(customer));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create( CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<CreateCustomerModel>(request);
        var createdCustomer = await _service.CreateCustomerAsync(customer, cancellationToken);
        if (createdCustomer is null) return BadRequest("Customer can not be created");
        
        var customerResponse = _mapper.Map<CustomerShortResponse>(createdCustomer);
        return CreatedAtAction(nameof(GetCustomerById), new { customerResponse.Id }, customerResponse);
    }
}