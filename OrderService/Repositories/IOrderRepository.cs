using OrderService.Models;

namespace OrderService.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();

    Task<Order?> GetByIdAsync(int id);

    Task<int> CreateAsync(Order order);
}