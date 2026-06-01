using System.Net.Http.Json;
using OrderService.DTOs;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly HttpClient _httpClient;

    public OrderService(
        IOrderRepository repository,
        HttpClient httpClient)
    {
        _repository = repository;
        _httpClient = httpClient;
    }

    public async Task<List<OrderReadDto>> GetAllAsync()
    {
        var orders =
            await _repository.GetAllAsync();

        return orders.Select(o => new OrderReadDto
        {
            OrderId = o.OrderId,
            CustomerId = o.CustomerId,
            ProductId = o.ProductId,
            Quantity = o.Quantity,
            UnitPrice = o.UnitPrice,
            TotalAmount = o.TotalAmount
        }).ToList();
    }

    public async Task<OrderReadDto?> GetByIdAsync(int id)
    {
        var order =
            await _repository.GetByIdAsync(id);

        if (order is null)
        {
            return null;
        }

        return new OrderReadDto
        {
            OrderId = order.OrderId,
            CustomerId = order.CustomerId,
            ProductId = order.ProductId,
            Quantity = order.Quantity,
            UnitPrice = order.UnitPrice,
            TotalAmount = order.TotalAmount
        };
    }

    public async Task<int> CreateAsync(
        OrderCreateDto dto)
    {
        CustomerDto? customer =
            await _httpClient.GetFromJsonAsync<CustomerDto>(
                $"https://localhost:7257/api/customers/{dto.CustomerId}");

        if (customer is null)
        {
            throw new Exception(
                "Customer not found.");
        }

        ProductDto? product =
            await _httpClient.GetFromJsonAsync<ProductDto>(
                $"https://localhost:7214/api/products/{dto.ProductId}");

        if (product is null)
        {
            throw new Exception(
                "Product not found.");
        }

        if (product.Stock < dto.Quantity)
        {
            throw new Exception(
                "Insufficient stock.");
        }

        int newStock =
            product.Stock - dto.Quantity;

        HttpResponseMessage stockResponse =
            await _httpClient.PutAsJsonAsync(
                $"https://localhost:7214/api/products/{dto.ProductId}/stock",
                new UpdateStockDto
                {
                    Stock = newStock
                });

        if (!stockResponse.IsSuccessStatusCode)
        {
            throw new Exception(
                "Stock update failed.");
        }

        Order order = new()
        {
            CustomerId = dto.CustomerId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            UnitPrice = product.Price,
            TotalAmount = product.Price * dto.Quantity,
            CreatedDate = DateTime.UtcNow
        };

        return await _repository.CreateAsync(order);
    }
}