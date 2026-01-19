using AutoMapper;
using AutoHub.Application.Models.Customer;
using AutoHub.Application.Services.Abstractions;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions;
using AutoHub.ValueObjects;

namespace AutoHub.Application.Services;

    public class CustomersApplicationService : ICustomersApplicationService
    {
    private readonly ICustomersRepository _repository;
    private readonly IMapper _mapper;

    public CustomersApplicationService(ICustomersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerModel>> GetCustomersAsync(CancellationToken cancellationToken = default)
    {
        var all = await _repository.GetAllAsync(cancellationToken, true);
        return all.Select(c => _mapper.Map<CustomerModel>(c));
    }

    public async Task<CustomerModel?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(id, cancellationToken);
        return customer is null ? null : _mapper.Map<CustomerModel>(customer);
    }

    public async Task<CustomerModel?> GetCustomerByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetCustomerByUsernameAsync(username, cancellationToken);
        return customer is null ? null : _mapper.Map<CustomerModel>(customer);
    }

    public async Task<CustomerModel?> CreateCustomerAsync(CreateCustomerModel customerInformation, CancellationToken cancellationToken)
    {
        if (await _repository.GetByIdAsync(customerInformation.Id, cancellationToken) is not null)
            return null;

        var customer = new Customer(customerInformation.Id, new Username(customerInformation.Username));
        var createdCustomer = await _repository.AddAsync(customer, cancellationToken);
        return createdCustomer is null ? null : _mapper.Map<CustomerModel>(createdCustomer);
    }

    public async Task<bool> UpdateCustomerAsync(CustomerModel customer, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(customer.Id, cancellationToken);
        if (entity is null) return false;
        
        var mapped = _mapper.Map<Customer>(customer);
        return await _repository.UpdateAsync(mapped, cancellationToken);
    }

    public async Task<bool> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(id, cancellationToken);
        return customer is null ? false : await _repository.DeleteAsync(customer, cancellationToken);
    }
    }

