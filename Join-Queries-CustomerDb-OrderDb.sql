-- INNER JOIN
-- Devuelve solo los registros que tienen coincidencia en ambas tablas.
SELECT 
    c.CustomerId,
    c.FirstName,
    c.LastName,
    o.OrderId,
    o.TotalAmount
FROM CustomersDb.dbo.Customers c
INNER JOIN OrdersDb.dbo.Orders o
    ON c.CustomerId = o.CustomerId;
go

-- LEFT JOIN
-- Devuelve todos los registros de la tabla izquierda y los que coinciden en la derecha (los que no, aparecen como NULL).
SELECT 
    c.CustomerId,
    c.FirstName,
    c.LastName,
    o.OrderId,
    o.TotalAmount
FROM CustomersDb.dbo.Customers c
LEFT JOIN OrdersDb.dbo.Orders o
    ON c.CustomerId = o.CustomerId;
go

-- RIGHT JOIN
-- Lo contrario del LEFT.
SELECT 
    c.CustomerId,
    c.FirstName,
    c.LastName,
    o.OrderId,
    o.TotalAmount
FROM CustomersDb.dbo.Customers c
RIGHT JOIN OrdersDb.dbo.Orders o
    ON c.CustomerId = o.CustomerId;
go

-- FULL OUTER JOIN
-- Devuelve todos los registros de ambas tablas, coincidan o no.
SELECT 
    c.CustomerId,
    c.FirstName,
    c.LastName,
    o.OrderId,
    o.TotalAmount
FROM CustomersDb.dbo.Customers c
FULL OUTER JOIN OrdersDb.dbo.Orders o
    ON c.CustomerId = o.CustomerId;
go