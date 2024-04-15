CREATE DATABASE TIENDAVIDEOJUEGOS;

USE TIENDAVIDEOJUEGOS;


-- Tabla de productos
CREATE TABLE Productos (
    IDProducto INT IDENTITY(1,1) PRIMARY KEY,
    NombreProducto VARCHAR(255) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    FechaPublicacion DATE NOT NULL,
    DisponibilidadInventario INT NOT NULL,
    VideoJuegoURL VARCHAR(255),
    EstadoProducto VARCHAR(50) NOT NULL DEFAULT 'Activo'
);

-- Tabla de usuarios
CREATE TABLE usuario (
    IDUsuario INT IDENTITY(1,1)  PRIMARY KEY,
    NombreUsuario VARCHAR(255) NOT NULL,
    Clave VARCHAR(255) NOT NULL,
    Correo varchar (255),
    UltimaConexion DATETIME DEFAULT GETDATE(),
    EstadoUsuario VARCHAR(50) NOT NULL DEFAULT 'Activo',
    ImagenPerfil VARBINARY(MAX) NULL
);

-- Tabla de direcciones de usuarios
CREATE TABLE Direcciones (
    IDDireccion INT PRIMARY KEY IDENTITY(1,1),
    IDUsuario INT,
    Calle VARCHAR(255) NOT NULL,
    Ciudad VARCHAR(255) NOT NULL,
    Estado VARCHAR(255) NOT NULL DEFAULT 'Activo',
    CodigoPostal VARCHAR(10) NOT NULL,
	CONSTRAINT FK_Direcciones_Usuarios FOREIGN KEY (IDUsuario) REFERENCES usuario(IDUsuario),

);


-- Tabla de usuarios y sus métodos de pago
CREATE TABLE UsuariosMetodosPago (
    IDUsuario INT,
    NumeroTarjeta VARCHAR(16) NOT NULL,
    FechaVencimiento DATE NOT NULL, 
	PRIMARY KEY (IDUsuario),
	CONSTRAINT FK_UsuariosMetodosPago_Usuario FOREIGN KEY (IDUsuario) REFERENCES usuario(IDUsuario),

);

-- Tabla de pedidos
CREATE TABLE Pedidos (
    IDPedido INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    FechaPedido DATE NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_Pedidos_Usuarios FOREIGN KEY (IDUsuario) REFERENCES usuario(IDUsuario),

);

-- Tabla de detalles del pedido
CREATE TABLE DetallesPedido (
    IDDetalle INT PRIMARY KEY IDENTITY(1,1) ,
    IDPedido INT,
    IDProducto INT,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_DetallesPedido_Pedidos FOREIGN KEY (IDPedido) REFERENCES Pedidos(IDPedido),
    CONSTRAINT FK_DetallesPedido_Productos FOREIGN KEY (IDProducto) REFERENCES Productos(IDProducto)
);


-- Tabla de reseñas de productos
CREATE TABLE Reseñas (
    IDReseña INT PRIMARY KEY IDENTITY(1,1),
    IDUsuario INT,
    IDProducto INT,
    Puntuacion INT NOT NULL,
    Comentario TEXT,
    FechaReseña DATE NOT NULL,
	CONSTRAINT FK_Reseñas_Usuarios FOREIGN KEY (IDUsuario) REFERENCES usuario(IDUsuario),
    CONSTRAINT FK_Reseñas_Productos FOREIGN KEY (IDProducto) REFERENCES Productos(IDProducto)


);

-- Tabla de carrito de compras
CREATE TABLE CarritoDeCompras (
    IDCarrito INT PRIMARY KEY IDENTITY(1,1),
    IDUsuario INT,
    IDProducto INT,
    Cantidad INT,
    Subtotal DECIMAL(10, 2), 
	CONSTRAINT FK_CarritoDeCompras_Usuarios FOREIGN KEY (IDUsuario) REFERENCES usuario(IDUsuario),
    CONSTRAINT FK_CarritoDeCompras_Productos FOREIGN KEY (IDProducto) REFERENCES Productos(IDProducto)

);


