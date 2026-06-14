select * from Orders;
go

insert into Orders
(
    CustomerId,
    ProductId,
    Quantity,
    UnitPrice,
    CreatedDate
)
output inserted.OrderId
values
(
    3,
    1,
    3,
	10000,
    SYSDATETIME()
);
go