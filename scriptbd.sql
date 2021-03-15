
--CREATE DATABASE dbFactura;

--USE dbFactura;

CREATE TABLE articulos
(
	id bigint PRIMARY KEY IDENTITY,
	codigo char(5),
	descripcion varchar(125) NOT null,
	precio decimal(9,4) NOT NULL CHECK(precio > 0),
	costo decimal(9,4) NOT NULL CHECK(costo > 0 ),
	activo bit DEFAULT 1
);

CREATE TABLE facturas
(
	numero_fact bigint PRIMARY KEY IDENTITY,
	fecha_registro date DEFAULT getdate(),
	subtotal decimal(12,4),
	impuesto decimal(5,2),
	total decimal(12,4)
)

CREATE TABLE detalleFactura
(
	id bigint PRIMARY KEY IDENTITY(1,1),
	numero_fact bigint NOT NULL,
	id_articulo bigint NOT NULL,
	precio decimal(9,4),
	cantidad int,
	subtotal decimal(12,4),
	impuesto decimal(5,2),
	total decimal(12,4)
	FOREIGN KEY (numero_fact) REFERENCES facturas(numero_fact),
	FOREIGN KEY (id_articulo) REFERENCES articulos(id)
)