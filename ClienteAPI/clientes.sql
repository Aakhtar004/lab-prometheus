CREATE DATABASE BD_CLIENTES;
GO
USE BD_CLIENTES;
GO

CREATE TABLE TiposDocumentos (
    Id INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

INSERT INTO TiposDocumentos (Nombre)
VALUES
('DNI'), ('Pasaporte'), ('Carné de Extranjería');
GO
