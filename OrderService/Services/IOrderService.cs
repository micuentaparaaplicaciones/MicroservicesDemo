using OrderService.DTOs;

namespace OrderService.Services;

public interface IOrderService
{
    Task<List<OrderReadDto>> GetAllAsync();

    Task<OrderReadDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(OrderCreateDto dto);
}