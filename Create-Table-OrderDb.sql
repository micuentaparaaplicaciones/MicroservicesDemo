USE OrdersDB;
GO

-- Nota: FK no aplica, Customers y Products residen en bases de datos distintas.
-- La integridad referencial se maneja en la capa de aplicación.
CREATE TABLE Orders
(
    OrderId       INT           IDENTITY(1,1) PRIMARY KEY,
    CustomerId    INT           NOT NULL,
    ProductId     INT           NOT NULL,
    Quantity      INT           NOT NULL,
    UnitPrice     DECIMAL(18,2) NOT NULL,
    TotalAmount   AS (Quantity * UnitPrice) PERSISTED,
    CreatedDate   DATETIME2     NOT NULL--,
    --CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    --CONSTRAINT FK_Orders_Products  FOREIGN KEY (ProductId)  REFERENCES Products(ProductId)
);
GO