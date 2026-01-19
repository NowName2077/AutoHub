using AutoHub.Application.Models.Transaction;
using AutoHub.Application.Services.Abstractions;
using AutoHub.WebHost.Requests.Transaction;
using AutoHub.WebHost.Responses.Transaction;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.WebHost.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsApplicationService _service;
    private readonly IMapper _mapper;

    public TransactionsController(ITransactionsApplicationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var tx = await _service.GetTransactionByIdAsync(id, ct);
        if (tx is null) return NotFound();
        return Ok(_mapper.Map<TransactionResponse>(tx));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request, CancellationToken ct)
    {
        var createModel = _mapper.Map<CreateTransactionModel>(request);
        var created = await _service.CreateTransactionAsync(createModel, ct);
        if (created is null) return BadRequest("Transaction cannot be created");
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<TransactionResponse>(created));
    }
}