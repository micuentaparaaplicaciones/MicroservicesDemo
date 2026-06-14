USE CustomersDB;
GO

CREATE TABLE Customers
(
    CustomerId    INT           IDENTITY(1,1) PRIMARY KEY,
    FirstName     NVARCHAR(100) NOT NULL,
    LastName      NVARCHAR(100) NOT NULL,
    Email         NVARCHAR(255) NOT NULL UNIQUE,
    CreatedDate   DATETIME2     NOT NULL
);
GO