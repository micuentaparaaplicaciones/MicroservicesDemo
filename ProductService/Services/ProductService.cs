using ProductService.DTOs;
using ProductService.Models;
using ProductService.Repositories;

namespace ProductService.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductReadDto>> GetAllAsync()
    {
        var products =
            await _repository.GetAllAsync();

        return products.Select(p => new ProductReadDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Price = p.Price,
            Stock = p.Stock
        }).ToList();
    }

    public async Task<ProductReadDto?> GetByIdAsync(int id)
    {
        var product =
            await _repository.GetByIdAsync(id);

        if (product is null)
        {
            return null;
        }

        return new ProductReadDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public async Task<int> CreateAsync(
        ProductCreateDto dto)
    {
        Product product = new()
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            CreatedDate = DateTime.UtcNow
        };

        return await _repository.CreateAsync(product);
    }

    public async Task<bool> UpdateAsync(
        int id,
        ProductUpdateDto dto)
    {
        Product product = new()
        {
            ProductId = id,
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock
        };

        return await _repository.UpdateAsync(product);
    }

    public async Task<bool> UpdateStockAsync(
        int productId,
        int stock)
    {
        return await _repository.UpdateStockAsync(
            productId,
            stock);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}