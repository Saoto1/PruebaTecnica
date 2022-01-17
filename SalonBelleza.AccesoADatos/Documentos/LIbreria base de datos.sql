



USE [master]
GO

CREATE DATABASE [Libreria]
GO

USE [Libreria]
Go

CREATE TABLE [dbo].[Cliente](
[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Dui] [varchar](50) NOT NULL,
[Nombre] [varchar](50) NOT NULL,
[Apellido] [varchar](50) NOT NULL,
[Numero] [int] NOT NULL
)
GO

CREATE TABLE [dbo].[Libros](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](50) NOT NULL,
	[Autor] [varchar](50) NOT NULL,
	[FechaPublicacion] [datetime] NOT NULL,
	[Genero] [varchar](50) NOT NULL,	
	[Precio] [decimal] (12, 2) NOT NULL,
	[Otros] [varchar](50)  NULL,
 )
 GO

 CREATE TABLE [dbo].[AlquiladosYVendidos](
[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
[IdCliente] [int] NOT NULL,
[IdLibro] [int] NOT NULL,
[Estado] [TinyInt] NOT NULL,
[Desde] [datetime] NOT NULL,
[Hasta] [datetime],
CONSTRAINT FK1_Cliente_AlquiladosYVendidos FOREIGN KEY (IdCliente) REFERENCES Cliente (Id),
CONSTRAINT FK2_Libros_AlquiladosYVendidos FOREIGN KEY (IdLibro) REFERENCES Libros (Id)
)
GO

select * from AlquiladosYVendidos




