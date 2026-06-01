namespace OrderService.DTOs;

public class OrderCreateDto
{
    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}