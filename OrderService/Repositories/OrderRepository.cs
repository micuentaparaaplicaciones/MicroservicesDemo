using Microsoft.Data.SqlClient;
using OrderService.Models;

namespace OrderService.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(
        IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString(
                "DefaultConnection")!;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        List<Order> orders = [];

        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            SELECT
                OrderId,
                CustomerId,
                ProductId,
                Quantity,
                UnitPrice,
                TotalAmount,
                CreatedDate
            FROM Orders
            """;

        using SqlCommand command =
            new(sql, connection);

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            orders.Add(new Order
            {
                OrderId = reader.GetInt32(0),
                CustomerId = reader.GetInt32(1),
                ProductId = reader.GetInt32(2),
                Quantity = reader.GetInt32(3),
                UnitPrice = reader.GetDecimal(4),
                TotalAmount = reader.GetDecimal(5),
                CreatedDate = reader.GetDateTime(6)
            });
        }

        return orders;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            SELECT
                OrderId,
                CustomerId,
                ProductId,
                Quantity,
                UnitPrice,
                TotalAmount,
                CreatedDate
            FROM Orders
            WHERE OrderId = @OrderId
            """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue(
            "@OrderId",
            id);

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Order
        {
            OrderId = reader.GetInt32(0),
            CustomerId = reader.GetInt32(1),
            ProductId = reader.GetInt32(2),
            Quantity = reader.GetInt32(3),
            UnitPrice = reader.GetDecimal(4),
            TotalAmount = reader.GetDecimal(5),
            CreatedDate = reader.GetDateTime(6)
        };
    }

    public async Task<int> CreateAsync(Order order)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            INSERT INTO Orders
            (
                CustomerId,
                ProductId,
                Quantity,
                UnitPrice,
                TotalAmount,
                CreatedDate
            )
            OUTPUT INSERTED.OrderId
            VALUES
            (
                @CustomerId,
                @ProductId,
                @Quantity,
                @UnitPrice,
                @TotalAmount,
                @CreatedDate
            )
            """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue(
            "@CustomerId",
            order.CustomerId);

        command.Parameters.AddWithValue(
            "@ProductId",
            order.ProductId);

        command.Parameters.AddWithValue(
            "@Quantity",
            order.Quantity);

        command.Parameters.AddWithValue(
            "@UnitPrice",
            order.UnitPrice);

        command.Parameters.AddWithValue(
            "@TotalAmount",
            order.TotalAmount);

        command.Parameters.AddWithValue(
            "@CreatedDate",
            order.CreatedDate);

        return (int)await command.ExecuteScalarAsync()!;
    }
}