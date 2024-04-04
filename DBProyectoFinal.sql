CREATE DATABASE TIENDAVIDEOJUEGOS;

USE TIENDAVIDEOJUEGOS;


-- Tabla de productos
CREATE TABLE Productos (
    IDProducto INT PRIMARY KEY,
    NombreProducto VARCHAR(255) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    FechaPublicacion DATE NOT NULL,
    DisponibilidadInventario INT NOT NULL,
    VideoJuegoURL VARCHAR(255),
    EstadoProducto VARCHAR(50) CHECK (EstadoProducto IN ('Activo', 'Inactivo')) NOT NULL
	);

-- Tabla de usuarios
CREATE TABLE Usuarios (
    IDUsuario INT PRIMARY KEY,
    NombreUsuario VARCHAR(255) NOT NULL,
    ContraseñaHash VARCHAR(255) NOT NULL,
    UltimaConexion DATETIME,
    EstadoUsuario VARCHAR(50) NOT NULL
);

-- Tabla de direcciones de usuarios
CREATE TABLE Direcciones (
    IDDireccion INT PRIMARY KEY,
    IDUsuario INT,
    Calle VARCHAR(255) NOT NULL,
    Ciudad VARCHAR(255) NOT NULL,
    Estado VARCHAR(255) NOT NULL,
    CodigoPostal VARCHAR(10) NOT NULL,
	CONSTRAINT FK_Direcciones_Usuarios FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),

);


-- Tabla de usuarios y sus métodos de pago
CREATE TABLE UsuariosMetodosPago (
    IDUsuario INT,
    NumeroTarjeta VARCHAR(16) NOT NULL,
    FechaVencimiento DATE NOT NULL, 
	PRIMARY KEY (IDUsuario),
	CONSTRAINT FK_UsuariosMetodosPago_Usuario FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),

);

-- Tabla de pedidos
CREATE TABLE Pedidos (
    IDPedido INT PRIMARY KEY,
    IDUsuario INT,
    FechaPedido DATE NOT NULL,
	CONSTRAINT FK_Pedidos_Usuarios FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),

);

-- Tabla de detalles del pedido
CREATE TABLE DetallesPedido (
    IDDetalle INT PRIMARY KEY,
    IDPedido INT,
    IDProducto INT,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_DetallesPedido_Pedidos FOREIGN KEY (IDPedido) REFERENCES Pedidos(IDPedido),
    CONSTRAINT FK_DetallesPedido_Productos FOREIGN KEY (IDProducto) REFERENCES Productos(IDProducto)
);


-- Tabla de reseñas de productos
CREATE TABLE Reseñas (
    IDReseña INT PRIMARY KEY,
    IDUsuario INT,
    IDProducto INT,
    Puntuacion INT NOT NULL,
    Comentario TEXT,
    FechaReseña DATE NOT NULL,
	CONSTRAINT FK_Reseñas_Usuarios FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    CONSTRAINT FK_Reseñas_Productos FOREIGN KEY (IDProducto) REFERENCES Productos(IDProducto)


);

-- Tabla de carrito de compras
CREATE TABLE CarritoDeCompras (
    IDCarrito INT PRIMARY KEY,
    IDUsuario INT,
    IDProducto INT,
    Cantidad INT,
    Subtotal DECIMAL(10, 2), 
	CONSTRAINT FK_CarritoDeCompras_Usuarios FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    CONSTRAINT FK_CarritoDeCompras_Productos FOREIGN KEY (IDProducto) REFERENCES Productos(IDProducto)

);


