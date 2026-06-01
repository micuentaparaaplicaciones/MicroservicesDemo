using ProductService.Models;

namespace ProductService.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int id);

    Task<int> CreateAsync(Product product);

    Task<bool> UpdateAsync(Product product);

    Task<bool> UpdateStockAsync(
        int productId,
        int stock);

    Task<bool> DeleteAsync(int id);
}