using ProductService.DTOs;

namespace ProductService.Services;

public interface IProductService
{
    Task<List<ProductReadDto>> GetAllAsync();

    Task<ProductReadDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(ProductCreateDto dto);

    Task<bool> UpdateAsync(
        int id,
        ProductUpdateDto dto);

    Task<bool> UpdateStockAsync(
        int productId,
        int stock);

    Task<bool> DeleteAsync(int id);
}