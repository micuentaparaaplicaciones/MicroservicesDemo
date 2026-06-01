using CustomerService.Models;
using Microsoft.Data.SqlClient;

namespace CustomerService.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;

    public CustomerRepository(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        List<Customer> customers = [];

        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            SELECT
                CustomerId,
                FirstName,
                LastName,
                Email,
                CreatedDate
            FROM Customers
            """;

        using SqlCommand command =
            new(sql, connection);

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            customers.Add(new Customer
            {
                CustomerId = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                CreatedDate = reader.GetDateTime(4)
            });
        }

        return customers;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            SELECT
                CustomerId,
                FirstName,
                LastName,
                Email,
                CreatedDate
            FROM Customers
            WHERE CustomerId = @CustomerId
            """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue(
            "@CustomerId",
            id);

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Customer
        {
            CustomerId = reader.GetInt32(0),
            FirstName = reader.GetString(1),
            LastName = reader.GetString(2),
            Email = reader.GetString(3),
            CreatedDate = reader.GetDateTime(4)
        };
    }

    public async Task<int> CreateAsync(Customer customer)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            INSERT INTO Customers
            (
                FirstName,
                LastName,
                Email,
                CreatedDate
            )
            OUTPUT INSERTED.CustomerId
            VALUES
            (
                @FirstName,
                @LastName,
                @Email,
                @CreatedDate
            )
            """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
        command.Parameters.AddWithValue("@LastName", customer.LastName);
        command.Parameters.AddWithValue("@Email", customer.Email);
        command.Parameters.AddWithValue("@CreatedDate", customer.CreatedDate);

        return (int)await command.ExecuteScalarAsync()!;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            UPDATE Customers
            SET
                FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email
            WHERE CustomerId = @CustomerId
            """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
        command.Parameters.AddWithValue("@LastName", customer.LastName);
        command.Parameters.AddWithValue("@Email", customer.Email);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using SqlConnection connection =
            new(_connectionString);

        await connection.OpenAsync();

        const string sql = """
            DELETE FROM Customers
            WHERE CustomerId = @CustomerId
            """;

        using SqlCommand command =
            new(sql, connection);

        command.Parameters.AddWithValue("@CustomerId", id);

        return await command.ExecuteNonQueryAsync() > 0;
    }
}