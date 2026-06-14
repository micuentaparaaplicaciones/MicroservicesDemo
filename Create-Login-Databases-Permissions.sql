-- 1. Habilitar modo mixto en SQL Server
-- -> click derecho en el servidor -> Properties 
-- -> Security -> SQL Server and Windows Authentication mode -> OK
-- -> Reiniciar el servicio de SQL Server

-- 2. Crear el Login con tu usuario de Windows ya conectado en SSMS
CREATE LOGIN AppUser
WITH PASSWORD = 'AppUser123456789!';
GO

-- 3. Crear las bases de datos
CREATE DATABASE CustomersDB;
GO

CREATE DATABASE ProductsDB;
GO

CREATE DATABASE OrdersDB;
GO

-- 4. Crear User y permisos en cada DB
USE CustomersDB;
CREATE USER AppUser FOR LOGIN AppUser;
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO AppUser;
GO

USE ProductsDB;
CREATE USER AppUser FOR LOGIN AppUser;
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO AppUser;
GO

USE OrdersDB;
CREATE USER AppUser FOR LOGIN AppUser;
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO AppUser;
GO