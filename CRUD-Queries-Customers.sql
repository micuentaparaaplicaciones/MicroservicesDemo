select CustomerId, Email from Customers;
go

insert into Customers
(
	FirstName,
	LastName,
	Email,
	CreatedDate
)
output inserted.CustomerId
values
(
    'Test',
    'One',
    'testone@customer.com',
    SYSDATETIME()
);
go

update Customers 
set Email = 'testupdated@customer.com'
where CustomerId = 5;
go

delete from Customers
where CustomerId = 5;
go