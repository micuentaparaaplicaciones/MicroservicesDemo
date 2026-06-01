using CustomerService.DTOs;
using CustomerService.Models;
using CustomerService.Repositories;

namespace CustomerService.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(
        ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CustomerReadDto>> GetAllAsync()
    {
        var customers =
            await _repository.GetAllAsync();

        return customers.Select(c => new CustomerReadDto
        {
            CustomerId = c.CustomerId,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email
        }).ToList();
    }

    public async Task<CustomerReadDto?> GetByIdAsync(int id)
    {
        var customer =
            await _repository.GetByIdAsync(id);

        if (customer is null)
        {
            return null;
        }

        return new CustomerReadDto
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email
        };
    }

    public async Task<int> CreateAsync(
        CustomerCreateDto dto)
    {
        Customer customer = new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            CreatedDate = DateTime.UtcNow
        };

        return await _repository.CreateAsync(customer);
    }

    public async Task<bool> UpdateAsync(
        int id,
        CustomerUpdateDto dto)
    {
        Customer customer = new()
        {
            CustomerId = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };

        return await _repository.UpdateAsync(customer);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}