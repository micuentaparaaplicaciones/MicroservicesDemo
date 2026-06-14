-- SELECT: consultar productos con todas sus propiedades
select * from Products;
go

-- SELECT: consultar productos con algunas propiedades
SELECT ProductId, Name, Price, Stock
FROM Products;
GO

-- INSERT: agregar un producto nuevo y devolver el ProductId generado
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
    'Laptop Gamer',
    500000.00,
    10,
    SYSDATETIME()
);
GO

-- UPDATE: actualizar el precio y stock de un producto específico
UPDATE Products
SET Price = 750000.00,
    Stock = 8
WHERE ProductId = 3;
GO

-- DELETE: eliminar un producto por su ProductId
DELETE FROM Products
WHERE ProductId = 3;
GO
