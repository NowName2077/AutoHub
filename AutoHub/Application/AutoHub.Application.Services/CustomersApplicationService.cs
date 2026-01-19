using AutoMapper;
using AutoHub.Application.Models.Customer;
using AutoHub.Application.Services.Abstractions;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions;
using AutoHub.ValueObjects;

namespace AutoHub.Application.Services;

    public class CustomersApplicationService(ICustomersRepository repository, IMapper mapper) : ICustomersApplicationService
    {
        public async Task<IEnumerable<CustomerModel>> GetCustomersAsync(CancellationToken cancellationToken = default)
        {
            var all = await repository.GetAllAsync(cancellationToken = default, true);
            return all.Select(mapper.Map<CustomerModel>);
        }

        public async Task<CustomerModel?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var customer = await repository.GetByIdAsync(id, cancellationToken);
            return customer is null ? null : mapper.Map<CustomerModel>(customer);
        }

        public async Task<CustomerModel?> GetCustomerByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var customer = await repository.GetCustomerByUsernameAsync(username, cancellationToken);
            return customer is null ? null : mapper.Map<CustomerModel>(customer);
        }
        public async Task<CustomerModel?> CreateCustomerAsync(CreateCustomerModel customerInformation, CancellationToken cancellationToken = default)
        {
            if (await repository.GetByIdAsync(customerInformation.Id, cancellationToken) is not null)
                return null;

            Customer customer = new(customerInformation.Id, new Username(customerInformation.Username));
            var createdCustomer = await repository.AddAsync(customer, cancellationToken);
            return createdCustomer is null ? null : mapper.Map<CustomerModel>(createdCustomer);
        }

        public async Task<bool> UpdateCustomerAsync(CustomerModel customer, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(customer.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<Customer>(customer);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var customer = await repository.GetByIdAsync(id, cancellationToken);
            return customer is null ? false : await repository.DeleteAsync(customer, cancellationToken);
        }
    }

