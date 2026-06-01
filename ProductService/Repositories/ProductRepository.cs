using Microsoft.Data.SqlClient;
using ProductService.Models;

namespace ProductService.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        List<Product> products = [];

        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            SELECT
                ProductId,
                Name,
                Price,
                Stock,
                CreatedDate
            FROM Products
            """;

        using SqlCommand command =
            new(sql, connection);

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            products.Add(new Product
            {
                ProductId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2),
                Stock = reader.GetInt32(3),
                CreatedDate = reader.GetDateTime(4)
            });
        }

        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            SELECT
                ProductId,
                Name,
                Price,
                Stock,
                CreatedDate
            FROM Products
            WHERE ProductId = @ProductId
            """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue(
            "@ProductId",
            id);

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Product
        {
            ProductId = reader.GetInt32(0),
            Name = reader.GetString(1),
            Price = reader.GetDecimal(2),
            Stock = reader.GetInt32(3),
            CreatedDate = reader.GetDateTime(4)
        };
    }

    public async Task<int> CreateAsync(Product product)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            INSERT INTO Products
            (
                Name,
                Price,
                Stock,
                CreatedDate
            )
            OUTPUT INSERTED.ProductId
            VALUES
            (
                @Name,
                @Price,
                @Stock,
                @CreatedDate
            )
            """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Price", product.Price);
        command.Parameters.AddWithValue("@Stock", product.Stock);
        command.Parameters.AddWithValue("@CreatedDate", product.CreatedDate);

        return (int)await command.ExecuteScalarAsync()!;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            UPDATE Products
            SET
                Name = @Name,
                Price = @Price,
                Stock = @Stock
            WHERE ProductId = @ProductId
            """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue("@ProductId", product.ProductId);
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Price", product.Price);
        command.Parameters.AddWithValue("@Stock", product.Stock);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> UpdateStockAsync(
        int productId,
        int stock)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
        UPDATE Products
        SET
            Stock = @Stock
        WHERE ProductId = @ProductId
        """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue(
            "@ProductId",
            productId);

        command.Parameters.AddWithValue(
            "@Stock",
            stock);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            DELETE FROM Products
            WHERE ProductId = @ProductId
            """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue("@ProductId", id);

        return await command.ExecuteNonQueryAsync() > 0;
    }
}