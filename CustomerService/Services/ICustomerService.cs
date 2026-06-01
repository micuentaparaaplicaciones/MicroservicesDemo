using CustomerService.DTOs;

namespace CustomerService.Services;

public interface ICustomerService
{
    Task<List<CustomerReadDto>> GetAllAsync();

    Task<CustomerReadDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CustomerCreateDto dto);

    Task<bool> UpdateAsync(
        int id,
        CustomerUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}