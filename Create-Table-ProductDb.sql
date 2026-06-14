USE ProductsDB;
GO

CREATE TABLE Products
(
    ProductId     INT           IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(200) NOT NULL,		
    Price         DECIMAL(18,2) NOT NULL,
    Stock         INT           NOT NULL,
    CreatedDate   DATETIME2     NOT NULL
);
GO