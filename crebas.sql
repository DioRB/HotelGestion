/*==============================================================*/
/* DATASET COMPLETO - CREAR TABLAS + INSERTAR DATOS             */
/*==============================================================*/

-- SOLUCIÓN SEGURA: Eliminar TODAS las FKs y tablas de una forma segura
DECLARE @sql NVARCHAR(MAX) = ''

-- 1. Generar script para eliminar TODAS las FKs
SELECT @sql = @sql + 
'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + 
QUOTENAME(OBJECT_NAME(parent_object_id)) + 
' DROP CONSTRAINT ' + QUOTENAME(name) + ';
'
FROM sys.foreign_keys
WHERE OBJECT_NAME(parent_object_id) IN (
    'FACTURA', 'EMPLEADO', 'EMPLEADO_SERVICIO', 'ESTACIONAMIENTO', 
    'ESTADIA', 'ESTADIA_SERVICIO', 'HABITACION', 'MANTENIMIENTO', 'RESERVA'
)

-- Ejecutar eliminación de FKs
EXEC sp_executesql @sql
GO

-- 2. Ahora eliminar TODAS las tablas en orden seguro
if exists (select 1 from sysobjects where id = object_id('EMPLEADO_SERVICIO') and type = 'U')
   drop table EMPLEADO_SERVICIO
go

if exists (select 1 from sysobjects where id = object_id('ESTADIA_SERVICIO') and type = 'U')
   drop table ESTADIA_SERVICIO
go

if exists (select 1 from sysobjects where id = object_id('ESTACIONAMIENTO') and type = 'U')
   drop table ESTACIONAMIENTO
go

if exists (select 1 from sysobjects where id = object_id('HABITACION') and type = 'U')
   drop table HABITACION
go

if exists (select 1 from sysobjects where id = object_id('MANTENIMIENTO') and type = 'U')
   drop table MANTENIMIENTO
go

if exists (select 1 from sysobjects where id = object_id('FACTURA') and type = 'U')
   drop table FACTURA
go

if exists (select 1 from sysobjects where id = object_id('ESTADIA') and type = 'U')
   drop table ESTADIA
go

if exists (select 1 from sysobjects where id = object_id('RESERVA') and type = 'U')
   drop table RESERVA
go

if exists (select 1 from sysobjects where id = object_id('EMPLEADO') and type = 'U')
   drop table EMPLEADO
go

if exists (select 1 from sysobjects where id = object_id('CLIENTE') and type = 'U')
   drop table CLIENTE
go

if exists (select 1 from sysobjects where id = object_id('EFECTIVO') and type = 'U')
   drop table EFECTIVO
go

if exists (select 1 from sysobjects where id = object_id('TARJETA') and type = 'U')
   drop table TARJETA
go

if exists (select 1 from sysobjects where id = object_id('SERVICIO') and type = 'U')
   drop table SERVICIO
go

if exists (select 1 from sysobjects where id = object_id('TURNO') and type = 'U')
   drop table TURNO
go
-- 1. PRIMERO VERIFICAR/CREAR LAS TABLAS NECESARIAS
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ESTADIA')
BEGIN
    CREATE TABLE ESTADIA (
        ID_ESTADIA           INT                  NOT NULL PRIMARY KEY,
        ID_RESERVA           INT                  NOT NULL,
        FECHA_CHECK_IN       DATETIME             NOT NULL,
        FECHA_CHECK_OUT      DATETIME             NULL,
        CANTIDAD_PERSONAS    INT                  NOT NULL,
        OBSERVACIONES        VARCHAR(256)         NOT NULL,
        ESTADO_ESTADIA       VARCHAR(256)         NOT NULL
    );
    PRINT 'Tabla ESTADIA creada';
END
ELSE
    PRINT 'Tabla ESTADIA ya existe';
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FACTURA')
BEGIN
    CREATE TABLE FACTURA (
        ID_FACTURA           INT                  NOT NULL PRIMARY KEY,
        ID_PERSONA           INT                  NULL,
        ID_TIPO_PAGO_EFECTIVO INT                  NULL,
        ID_TIPO_PAGO_TARJETA INT                  NULL,
        ID_ESTADIA           INT                  NULL,
        VALOR_TOTAL          DECIMAL(19,4)        NOT NULL,
        FECHA_PAGO           DATETIME             NOT NULL,
        ESTADO_FACTURA       VARCHAR(256)         NOT NULL,
        CONSTRAINT CHK_PAGO_EXCLUSIVO CHECK (
            (ID_TIPO_PAGO_EFECTIVO IS NOT NULL AND ID_TIPO_PAGO_TARJETA IS NULL) OR
            (ID_TIPO_PAGO_EFECTIVO IS NULL AND ID_TIPO_PAGO_TARJETA IS NOT NULL)
        )
    );
    PRINT 'Tabla FACTURA creada';
END
ELSE
    PRINT 'Tabla FACTURA ya existe';
GO

-- Verificar/crear las demás tablas si no existen
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TURNO')
BEGIN
    CREATE TABLE TURNO (
        ID_TURNO             INT                  NOT NULL PRIMARY KEY IDENTITY (1,1),
        TIPO_TURNO           VARCHAR(256)         NOT NULL,
        HORA_INICIO          DATETIME             NOT NULL,
        HORA_FIN             DATETIME             NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EMPLEADO')
BEGIN
    CREATE TABLE EMPLEADO (
        ID_TURNO             INT                  NOT NULL,
        ID_PERSONA           INT                  NOT NULL PRIMARY KEY IDENTITY (101,1),
        DOCUMENTO            VARCHAR(256)         NULL,
        NOMBRE               VARCHAR(256)         NULL,
        APELLIDO             VARCHAR(256)         NULL,
        TELEFONO             VARCHAR(256)         NULL,
        CORREO               VARCHAR(256)         NULL,
        CARGO                VARCHAR(256)         NOT NULL,
        SALARIO              DECIMAL(19,4)        NOT NULL,
        FECHA_CONTRATACION   DATETIME             NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CLIENTE')
BEGIN
    CREATE TABLE CLIENTE (
        ID_PERSONA           INT                  NOT NULL PRIMARY KEY,
        DOCUMENTO            VARCHAR(256)         NULL,
        NOMBRE               VARCHAR(256)         NULL,
        APELLIDO             VARCHAR(256)         NULL,
        TELEFONO             VARCHAR(256)         NULL,
        CORREO               VARCHAR(256)         NULL,
        TIPO_CLIENTE         VARCHAR(256)         NOT NULL,
        PAIS                 VARCHAR(256)         NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EFECTIVO')
BEGIN
    CREATE TABLE EFECTIVO (
        ID_TIPO_PAGO         INT                  NOT NULL PRIMARY KEY,
        ATTRIBUTE_43         DECIMAL(19,4)        NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TARJETA')
BEGIN
    CREATE TABLE TARJETA (
        ID_TIPO_PAGO         INT                  NOT NULL PRIMARY KEY,
        ID_TARJETA           INT                  NOT NULL,
        NUMERO_TARJETA       VARCHAR(256)         NOT NULL,
        TIPO_TARJETA         VARCHAR(256)         NOT NULL,
        BANCO_EMISOR         VARCHAR(256)         NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SERVICIO')
BEGIN
    CREATE TABLE SERVICIO (
        ID_SERVICIO          INT                  NOT NULL PRIMARY KEY,
        DESCRIPCION          VARCHAR(256)         NOT NULL,
        TARIFA               DECIMAL(19,4)        NOT NULL,
        NOMBRE_SERVICIO      VARCHAR(256)         NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RESERVA')
BEGIN
    CREATE TABLE RESERVA (
        ID_RESERVA           INT                  NOT NULL PRIMARY KEY,
        ID_PERSONA           INT                  NULL,
        FECHA_INICIO         DATETIME             NOT NULL,
        FECHA_FIN            DATETIME             NOT NULL,
        ESTADO               VARCHAR(256)         NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HABITACION')
BEGIN
    CREATE TABLE HABITACION (
        ID_HABITACION        INT                  NOT NULL PRIMARY KEY IDENTITY (501, 1),
        ID_RESERVA           INT                  NULL,
        NUMERO               VARCHAR(256)         NOT NULL,
        PISO                 INT                  NOT NULL,
        TIPO_HABITACION      VARCHAR(256)         NOT NULL,
        CAPACIDAD            INT                  NOT NULL,
        PRECIO_NOCHE         DECIMAL(19,4)        NOT NULL,
        ESTADO_HABITACION    VARCHAR(256)         NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ESTACIONAMIENTO')
BEGIN
    CREATE TABLE ESTACIONAMIENTO (
        ID_ESTACIONAMIENTO   INT                  NOT NULL PRIMARY KEY,
        ID_RESERVA           INT                  NULL,
        NUMERO_ASIGNADO      INT                  NOT NULL,
        DISPONIBLE           VARCHAR(256)         NOT NULL,
        COSTO_ESTACIONAMIENTO DECIMAL(19,4)        NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MANTENIMIENTO')
BEGIN
    CREATE TABLE MANTENIMIENTO (
        ID_MANTENIMIENTO     INT                  NOT NULL PRIMARY KEY,
        ID_PERSONA           INT                  NULL,
        FECHA_REPORTE        DATETIME             NOT NULL,
        FECHA_MANTENIMIENTO  DATETIME             NOT NULL,
        MOTIVO               VARCHAR(256)         NOT NULL,
        COSTO_MANTENIMIENTO  DECIMAL(19,4)        NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EMPLEADO_SERVICIO')
BEGIN
    CREATE TABLE EMPLEADO_SERVICIO (
        ID_PERSONA           INT                  NOT NULL,
        ID_SERVICIO          INT                  NOT NULL,
        FECHA                DATETIME             NOT NULL,
        PRIMARY KEY (ID_PERSONA, ID_SERVICIO)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ESTADIA_SERVICIO')
BEGIN
    CREATE TABLE ESTADIA_SERVICIO (
        ID_ESTADIA           INT                  NOT NULL,
        ID_SERVICIO          INT                  NOT NULL,
        FECHA_SERVICIO       DATETIME             NOT NULL,
        PRIMARY KEY (ID_ESTADIA, ID_SERVICIO)
    );
END
GO

/*==============================================================*/
/* 2. LIMPIAR DATOS EXISTENTES                                  */
/*==============================================================*/
PRINT 'Limpiando datos existentes...';

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'ESTADIA_SERVICIO')
    DELETE FROM ESTADIA_SERVICIO;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EMPLEADO_SERVICIO')
    DELETE FROM EMPLEADO_SERVICIO;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FACTURA')
    DELETE FROM FACTURA;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'MANTENIMIENTO')
    DELETE FROM MANTENIMIENTO;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'ESTADIA')
    DELETE FROM ESTADIA;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'HABITACION')
    DELETE FROM HABITACION;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'ESTACIONAMIENTO')
    DELETE FROM ESTACIONAMIENTO;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'RESERVA')
    DELETE FROM RESERVA;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'SERVICIO')
    DELETE FROM SERVICIO;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EMPLEADO')
    DELETE FROM EMPLEADO;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'TURNO')
    DELETE FROM TURNO;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'CLIENTE')
    DELETE FROM CLIENTE;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EFECTIVO')
    DELETE FROM EFECTIVO;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'TARJETA')
    DELETE FROM TARJETA;

PRINT 'Datos limpiados exitosamente';
GO

/*==============================================================*/
/* 3. INSERTAR DATOS EN LAS TABLAS                              */
/*==============================================================*/

-- TURNOS (6)
INSERT INTO TURNO (TIPO_TURNO, HORA_INICIO, HORA_FIN) VALUES
('MAÑANA', '2025-01-01 06:00:00', '2025-01-01 14:00:00'),
('TARDE', '2025-01-01 14:00:00', '2025-01-01 22:00:00'),
('NOCHE', '2025-01-01 22:00:00', '2025-01-02 06:00:00'),
('ADMIN', '2025-01-01 08:00:00', '2025-01-01 17:00:00'),
('24H', '2025-01-01 00:00:00', '2025-01-01 23:59:59'),
('PART-TIME', '2025-01-01 10:00:00', '2025-01-01 18:00:00');
PRINT 'TURNOS insertados';
GO

-- EMPLEADOS (15)
INSERT INTO EMPLEADO (ID_TURNO, DOCUMENTO, NOMBRE, APELLIDO, TELEFONO, CORREO, CARGO, SALARIO, FECHA_CONTRATACION) VALUES
(1, '10101010', 'Carlos', 'Gómez', '3001111111', 'carlos@hotel.com', 'RECEPCIONISTA', 2500000.00, '2023-01-15'),
(2, '20202020', 'Ana', 'Rodríguez', '3002222222', 'ana@hotel.com', 'RECEPCIONISTA', 2500000.00, '2023-03-20'),
(3, '30303030', 'Luis', 'Martínez', '3003333333', 'luis@hotel.com', 'SEGURIDAD', 1800000.00, '2023-05-10'),
(4, '40404040', 'María', 'López', '3004444444', 'maria@hotel.com', 'GERENTE', 5000000.00, '2022-08-01'),
(5, '50505050', 'Pedro', 'Sánchez', '3005555555', 'pedro@hotel.com', 'LIMPIEZA', 1500000.00, '2023-07-25'),
(6, '60606060', 'Sofía', 'Ramírez', '3006666666', 'sofia@hotel.com', 'MANTENIMIENTO', 2000000.00, '2023-09-12'),
(1, '70707070', 'Miguel', 'Torres', '3007777777', 'miguel@hotel.com', 'RECEPCIONISTA', 2500000.00, '2023-11-05'),
(2, '80808080', 'Laura', 'Díaz', '3008888888', 'laura@hotel.com', 'COCINERA', 2200000.00, '2024-01-10'),
(3, '90909090', 'Jorge', 'Hernández', '3009999999', 'jorge@hotel.com', 'SEGURIDAD', 1800000.00, '2024-02-15'),
(4, '10111111', 'Carmen', 'Gutiérrez', '3010000000', 'carmen@hotel.com', 'SUBGERENTE', 3500000.00, '2023-06-20'),
(5, '11111111', 'Ricardo', 'Castillo', '3011111111', 'ricardo@hotel.com', 'LIMPIEZA', 1500000.00, '2023-08-30'),
(6, '12121212', 'Patricia', 'Morales', '3012222222', 'patricia@hotel.com', 'RECEPCIONISTA', 2500000.00, '2024-03-01'),
(1, '13131313', 'Fernando', 'Ortega', '3013333333', 'fernando@hotel.com', 'CONSERJE', 1700000.00, '2023-12-10'),
(2, '14141414', 'Gabriela', 'Silva', '3014444444', 'gabriela@hotel.com', 'CAMARERA', 1600000.00, '2024-01-25'),
(3, '15151515', 'Roberto', 'Vargas', '3015555555', 'roberto@hotel.com', 'SEGURIDAD', 1800000.00, '2023-10-15');
PRINT 'EMPLEADOS insertados';
GO

-- CLIENTES (50)
DECLARE @cliente_id INT = 201;
DECLARE @counter_cl INT = 1;

WHILE @cliente_id <= 250
BEGIN
    INSERT INTO CLIENTE (ID_PERSONA, DOCUMENTO, NOMBRE, APELLIDO, TELEFONO, CORREO, TIPO_CLIENTE, PAIS)
    VALUES (
        @cliente_id,
        RIGHT('00000000' + CAST(@counter_cl * 1111111 AS VARCHAR(10)), 8),
        'Cliente' + CAST(@counter_cl AS VARCHAR(3)),
        'Apellido' + CAST(@counter_cl AS VARCHAR(3)),
        '31' + RIGHT('0000000' + CAST((@counter_cl * 7777777) % 10000000 AS VARCHAR(7)), 7),
        'cliente' + CAST(@counter_cl AS VARCHAR(3)) + '@email.com',
        CASE (@counter_cl % 3)
            WHEN 0 THEN 'REGULAR'
            WHEN 1 THEN 'FRECUENTE'
            ELSE 'VIP'
        END,
        CASE (@counter_cl % 5)
            WHEN 0 THEN 'COLOMBIA'
            WHEN 1 THEN 'ESTADOS UNIDOS'
            WHEN 2 THEN 'MÉXICO'
            WHEN 3 THEN 'ESPAÑA'
            ELSE 'ARGENTINA'
        END
    );
    
    SET @cliente_id = @cliente_id + 1;
    SET @counter_cl = @counter_cl + 1;
END
PRINT 'CLIENTES insertados';
GO

-- EFECTIVO (30)
DECLARE @efectivo_id INT = 1001;
DECLARE @ef_counter INT = 1;

WHILE @efectivo_id <= 1030
BEGIN
    DECLARE @monto DECIMAL(19,4) = 150000.00 + (@ef_counter * 10000.00);
    
    INSERT INTO EFECTIVO (ID_TIPO_PAGO, ATTRIBUTE_43)
    VALUES (
        @efectivo_id,
        @monto
    );
    
    SET @efectivo_id = @efectivo_id + 1;
    SET @ef_counter = @ef_counter + 1;
END
PRINT 'EFECTIVO insertado';
GO

-- TARJETA (30) - SEGURO SIN OVERFLOW
DECLARE @tarjeta_id INT = 2001;
DECLARE @tar_counter INT = 1;

WHILE @tarjeta_id <= 2030
BEGIN
    -- Generar número de tarjeta de 16 dígitos simple
    DECLARE @num_tarjeta VARCHAR(16);
    SET @num_tarjeta = '4' + 
        RIGHT('00000000000000' + CAST((@tar_counter * 1000000 + @tar_counter * 100) AS VARCHAR(15)), 15);
    
    INSERT INTO TARJETA (ID_TIPO_PAGO, ID_TARJETA, NUMERO_TARJETA, TIPO_TARJETA, BANCO_EMISOR)
    VALUES (
        @tarjeta_id,
        @tar_counter,
        @num_tarjeta,
        CASE (@tar_counter % 3)
            WHEN 0 THEN 'VISA'
            WHEN 1 THEN 'MASTERCARD'
            ELSE 'AMEX'
        END,
        CASE (@tar_counter % 4)
            WHEN 0 THEN 'BANCOLOMBIA'
            WHEN 1 THEN 'BANCO DE BOGOTÁ'
            WHEN 2 THEN 'DAVIVIENDA'
            ELSE 'BBVA'
        END
    );
    
    SET @tarjeta_id = @tarjeta_id + 1;
    SET @tar_counter = @tar_counter + 1;
END
PRINT 'TARJETAS insertadas';
GO

-- SERVICIOS (15)
INSERT INTO SERVICIO (ID_SERVICIO, DESCRIPCION, TARIFA, NOMBRE_SERVICIO) VALUES
(301, 'Desayuno buffet continental', 25000.00, 'DESAYUNO'),
(302, 'Almuerzo ejecutivo', 45000.00, 'ALMUERZO'),
(303, 'Cena gourmet', 65000.00, 'CENA'),
(304, 'Servicio a la habitación', 15000.00, 'ROOM SERVICE'),
(305, 'Lavandería express', 30000.00, 'LAVANDERIA'),
(306, 'Spa y masajes', 120000.00, 'SPA'),
(307, 'Gimnasio completo', 20000.00, 'GIMNASIO'),
(308, 'Piscina climatizada', 25000.00, 'PISCINA'),
(309, 'WiFi premium', 10000.00, 'WIFI'),
(310, 'Parqueadero cubierto', 20000.00, 'PARQUEADERO'),
(311, 'Traslado aeropuerto', 50000.00, 'TRASLADO'),
(312, 'Tour ciudad', 80000.00, 'TOUR'),
(313, 'Servicio de niñera', 40000.00, 'NIÑERA'),
(314, 'Bar privado', 35000.00, 'BAR'),
(315, 'Business center', 15000.00, 'BUSINESS');
PRINT 'SERVICIOS insertados';
GO

-- RESERVAS (80)
DECLARE @reserva_id INT = 401;
DECLARE @fecha_base DATE = '2025-01-01';

WHILE @reserva_id <= 480
BEGIN
    DECLARE @fecha_inicio_reserva DATE = DATEADD(DAY, @reserva_id % 180, @fecha_base);
    DECLARE @dias_reserva INT = 2 + (@reserva_id % 10);
    DECLARE @cliente_id_reserva INT = 201 + ((@reserva_id - 401) % 50);
    
    INSERT INTO RESERVA (ID_RESERVA, ID_PERSONA, FECHA_INICIO, FECHA_FIN, ESTADO)
    VALUES (
        @reserva_id,
        @cliente_id_reserva,
        @fecha_inicio_reserva,
        DATEADD(DAY, @dias_reserva, @fecha_inicio_reserva),
        CASE (@reserva_id % 4)
            WHEN 0 THEN 'PENDIENTE'
            WHEN 1 THEN 'CONFIRMADA'
            WHEN 2 THEN 'COMPLETADA'
            ELSE 'CANCELADA'
        END
    );
    
    SET @reserva_id = @reserva_id + 1;
END
PRINT 'RESERVAS insertadas';
GO

-- HABITACIONES (60)
DECLARE @habitacion_id INT = 501;
DECLARE @piso INT = 1;
DECLARE @numero INT = 101;

WHILE @habitacion_id <= 560
BEGIN
    DECLARE @tipo VARCHAR(20) = CASE (@habitacion_id % 5)
        WHEN 0 THEN 'INDIVIDUAL'
        WHEN 1 THEN 'DOBLE'
        WHEN 2 THEN 'TRIPLE'
        WHEN 3 THEN 'SUITE'
        ELSE 'PRESIDENCIAL'
    END;
    
    DECLARE @id_reserva_hab INT = 
        CASE WHEN (@habitacion_id % 100) < 70 
            THEN 401 + (@habitacion_id % 80) 
            ELSE NULL 
        END;
    
    INSERT INTO HABITACION ( ID_RESERVA, NUMERO, PISO, TIPO_HABITACION, CAPACIDAD, PRECIO_NOCHE, ESTADO_HABITACION)
    VALUES (
        @id_reserva_hab,
        CAST(@numero AS VARCHAR(10)),
        CAST(@piso AS VARCHAR(10)),
        @tipo,
        CASE @tipo
            WHEN 'INDIVIDUAL' THEN 1
            WHEN 'DOBLE' THEN 2
            WHEN 'TRIPLE' THEN 3
            WHEN 'SUITE' THEN 4
            ELSE 6
        END,
        CASE @tipo
            WHEN 'INDIVIDUAL' THEN 120000.00
            WHEN 'DOBLE' THEN 180000.00
            WHEN 'TRIPLE' THEN 240000.00
            WHEN 'SUITE' THEN 350000.00
            ELSE 500000.00
        END,
        CASE (@habitacion_id % 5)
            WHEN 0 THEN 'DISPONIBLE'
            WHEN 1 THEN 'RESERVADA'
            WHEN 2 THEN 'OCUPADA'
            WHEN 3 THEN 'MANTENIMIENTO'
            ELSE 'LIMPIEZA'
        END
    );
    
    SET @habitacion_id = @habitacion_id + 1;
    SET @numero = @numero + 1;

    IF @numero % 100 >= 21
    BEGIN
        SET @numero = (@piso + 1) * 100 + 1;
        SET @piso = @piso + 1;
    END
END
PRINT 'HABITACIONES insertadas';
GO

-- ESTACIONAMIENTO (40)
DECLARE @estacionamiento_id INT = 601;
DECLARE @numero_asignado INT = 1;

WHILE @estacionamiento_id <= 640
BEGIN
    DECLARE @id_reserva_est INT = 
        CASE WHEN (@estacionamiento_id % 100) < 60 
            THEN 401 + (@estacionamiento_id % 80) 
            ELSE NULL 
        END;
    
    DECLARE @costo DECIMAL(19,4) = 20000.00 + (@estacionamiento_id * 100);
    
    INSERT INTO ESTACIONAMIENTO (ID_ESTACIONAMIENTO, ID_RESERVA, NUMERO_ASIGNADO, DISPONIBLE, COSTO_ESTACIONAMIENTO)
    VALUES (
        @estacionamiento_id,
        @id_reserva_est,
        @numero_asignado,
        CASE WHEN (@estacionamiento_id % 100) < 60 THEN 'OCUPADO' ELSE 'DISPONIBLE' END,
        @costo
    );
    
    SET @estacionamiento_id = @estacionamiento_id + 1;
    SET @numero_asignado = @numero_asignado + 1;
END
PRINT 'ESTACIONAMIENTO insertado';
GO

-- ESTADÍAS (60)
DECLARE @estadia_id INT = 701;
DECLARE @reserva_counter INT = 401;
DECLARE @estadias_insertadas INT = 0;

WHILE @estadia_id <= 760 AND @reserva_counter <= 480 AND @estadias_insertadas < 60
BEGIN
    IF EXISTS (SELECT 1 FROM RESERVA WHERE ID_RESERVA = @reserva_counter AND ESTADO IN ('COMPLETADA', 'CONFIRMADA'))
    BEGIN
        DECLARE @fecha_inicio_estadia DATE = DATEADD(DAY, @reserva_counter % 180, '2025-01-01');
        DECLARE @dias_estadia INT = 1 + (@reserva_counter % 14);
        DECLARE @fecha_fin_estadia DATE = DATEADD(DAY, @dias_estadia, @fecha_inicio_estadia);
        
        INSERT INTO ESTADIA (ID_ESTADIA, ID_RESERVA, FECHA_CHECK_IN, FECHA_CHECK_OUT, CANTIDAD_PERSONAS, OBSERVACIONES, ESTADO_ESTADIA)
        VALUES (
            @estadia_id,
            @reserva_counter,
            @fecha_inicio_estadia,
            @fecha_fin_estadia,
            1 + (@reserva_counter % 6),
            CASE (@reserva_counter % 5)
                WHEN 0 THEN 'Cliente satisfecho'
                WHEN 1 THEN 'Requiere atención especial'
                WHEN 2 THEN 'Sin observaciones'
                WHEN 3 THEN 'Recomendaciones de mejora'
                ELSE 'Cliente frecuente'
            END,
            'FINALIZADA'
        );
        
        SET @estadia_id = @estadia_id + 1;
        SET @estadias_insertadas = @estadias_insertadas + 1;
    END
    
    SET @reserva_counter = @reserva_counter + 1;
END
PRINT 'ESTADÍAS insertadas';
GO

-- FACTURAS (50)
DECLARE @factura_id INT = 801;
DECLARE @estadia_factura INT = 701;
DECLARE @facturas_insertadas INT = 0;

WHILE @factura_id <= 850 AND @estadia_factura <= 760 AND @facturas_insertadas < 50
BEGIN
    IF EXISTS (SELECT 1 FROM ESTADIA WHERE ID_ESTADIA = @estadia_factura)
    BEGIN
        DECLARE @metodo_pago INT = (@factura_id % 2);
        DECLARE @valor_total DECIMAL(19,4) = 150000.00 * (1 + (@factura_id % 14));
        SET @valor_total = @valor_total + (@factura_id * 1000.00);
        
        DECLARE @fecha_check_in DATE;
        DECLARE @fecha_pago DATE;
        
        SELECT @fecha_check_in = FECHA_CHECK_IN FROM ESTADIA WHERE ID_ESTADIA = @estadia_factura;
        
        SET @fecha_pago = DATEADD(DAY, 1 + (@factura_id % 3), @fecha_check_in);
        
        DECLARE @id_efectivo INT = NULL;
        DECLARE @id_tarjeta INT = NULL;
        
        IF @metodo_pago = 0
            SET @id_efectivo = 1001 + (@factura_id % 30);
        ELSE
            SET @id_tarjeta = 2001 + (@factura_id % 30);
        
        -- USANDO LOS NOMBRES DE COLUMNA CORRECTOS
        INSERT INTO FACTURA (ID_FACTURA, ID_PERSONA, ID_TIPO_PAGO_EFECTIVO, ID_TIPO_PAGO_TARJETA, ID_ESTADIA, VALOR_TOTAL, FECHA_PAGO, ESTADO_FACTURA)
        VALUES (
            @factura_id,
            201 + ((@factura_id - 801) % 50),
            @id_efectivo,
            @id_tarjeta,
            @estadia_factura,
            @valor_total,
            @fecha_pago,
            CASE WHEN @metodo_pago = 0 THEN 'PAGADA' ELSE 'PENDIENTE' END
        );
        
        SET @factura_id = @factura_id + 1;
        SET @facturas_insertadas = @facturas_insertadas + 1;
    END
    
    SET @estadia_factura = @estadia_factura + 1;
END
PRINT 'FACTURAS insertadas';
GO

-- MANTENIMIENTO (20 registros)
DECLARE @mantenimiento_id INT = 901;
DECLARE @mant_counter INT = 1;

WHILE @mantenimiento_id <= 920
BEGIN
    DECLARE @costo_mantenimiento DECIMAL(19,4) = 50000.00 + (@mantenimiento_id * 5000);
    
    INSERT INTO MANTENIMIENTO (ID_MANTENIMIENTO, ID_PERSONA, FECHA_REPORTE, FECHA_MANTENIMIENTO, MOTIVO, COSTO_MANTENIMIENTO)
    VALUES (
        @mantenimiento_id,
        101 + (@mantenimiento_id % 15),
        DATEADD(DAY, @mantenimiento_id % 180, '2025-01-01'),
        DATEADD(DAY, 1 + (@mantenimiento_id % 7), DATEADD(DAY, @mantenimiento_id % 180, '2025-01-01')),
        CASE (@mantenimiento_id % 6)
            WHEN 0 THEN 'Reparación tuberías'
            WHEN 1 THEN 'Cambio bombillas'
            WHEN 2 THEN 'Pintura habitación'
            WHEN 3 THEN 'Reparación aire acondicionado'
            WHEN 4 THEN 'Mantenimiento eléctrico'
            ELSE 'Limpieza profunda'
        END,
        @costo_mantenimiento
    );
    
    SET @mantenimiento_id = @mantenimiento_id + 1;
    SET @mant_counter = @mant_counter + 1;
END
PRINT 'MANTENIMIENTO insertado';
GO

-- EMPLEADO_SERVICIO (50 registros ÚNICOS)
DECLARE @counter_es INT = 1;
DECLARE @empleado_servicio_ids TABLE (id_persona INT, id_servicio INT, PRIMARY KEY (id_persona, id_servicio));

WHILE @counter_es <= 50
BEGIN
    DECLARE @id_persona_es INT, @id_servicio_es INT;
    
    WHILE 1=1
    BEGIN
        SET @id_persona_es = 101 + (@counter_es % 15);
        SET @id_servicio_es = 301 + (@counter_es % 15);
        
        IF NOT EXISTS (SELECT 1 FROM @empleado_servicio_ids 
                      WHERE id_persona = @id_persona_es AND id_servicio = @id_servicio_es)
            BREAK;
    END
    
    INSERT INTO @empleado_servicio_ids VALUES (@id_persona_es, @id_servicio_es);
    
    INSERT INTO EMPLEADO_SERVICIO (ID_PERSONA, ID_SERVICIO, FECHA)
    VALUES (
        @id_persona_es,
        @id_servicio_es,
        DATEADD(DAY, @counter_es % 180, '2025-01-01 07:00:00')
    );
    
    SET @counter_es = @counter_es + 1;
END
PRINT 'EMPLEADO_SERVICIO insertado';
GO

-- ESTADIA_SERVICIO (100 registros ÚNICOS)
DECLARE @counter_ess INT = 1;
DECLARE @estadia_servicio_ids TABLE (id_estadia INT, id_servicio INT, PRIMARY KEY (id_estadia, id_servicio));

WHILE @counter_ess <= 100
BEGIN
    DECLARE @id_estadia_ess INT, @id_servicio_ess INT;
    
    WHILE 1=1
    BEGIN
        SET @id_estadia_ess = 701 + (@counter_ess % 60);
        SET @id_servicio_ess = 301 + (@counter_ess % 15);
        
        IF EXISTS (SELECT 1 FROM ESTADIA WHERE ID_ESTADIA = @id_estadia_ess)
            IF NOT EXISTS (SELECT 1 FROM @estadia_servicio_ids 
                          WHERE id_estadia = @id_estadia_ess AND id_servicio = @id_servicio_ess)
                BREAK;
    END
    
    INSERT INTO @estadia_servicio_ids VALUES (@id_estadia_ess, @id_servicio_ess);
    
    INSERT INTO ESTADIA_SERVICIO (ID_ESTADIA, ID_SERVICIO, FECHA_SERVICIO)
    VALUES (
        @id_estadia_ess,
        @id_servicio_ess,
        DATEADD(DAY, @counter_ess % 180, '2025-01-01 08:00:00')
    );
    
    SET @counter_ess = @counter_ess + 1;
END
PRINT 'ESTADIA_SERVICIO insertado';
GO

/*==============================================================*/
/* 4. VERIFICACIÓN FINAL                                        */
/*==============================================================*/
PRINT '===============================================';
PRINT 'DATASET COMPLETO GENERADO EXITOSAMENTE';
PRINT '===============================================';
PRINT 'RESUMEN DE REGISTROS:';
PRINT '-----------------------------------------------';

SELECT 'TURNO' AS Tabla, COUNT(*) AS Registros FROM TURNO
UNION ALL SELECT 'EMPLEADO', COUNT(*) FROM EMPLEADO
UNION ALL SELECT 'CLIENTE', COUNT(*) FROM CLIENTE
UNION ALL SELECT 'EFECTIVO', COUNT(*) FROM EFECTIVO
UNION ALL SELECT 'TARJETA', COUNT(*) FROM TARJETA
UNION ALL SELECT 'SERVICIO', COUNT(*) FROM SERVICIO
UNION ALL SELECT 'RESERVA', COUNT(*) FROM RESERVA
UNION ALL SELECT 'HABITACION', COUNT(*) FROM HABITACION
UNION ALL SELECT 'ESTACIONAM.', COUNT(*) FROM MANTENIMIENTO
UNION ALL SELECT 'EMP_SERVICIO', COUNT(*) FROM EMPLEADO_SERVICIO
UNION ALL SELECT 'EST_SERVICIO', COUNT(*) FROM ESTADIA_SERVICIO
ORDER BY Tabla;

PRINT '-----------------------------------------------';
PRINT 'VERIFICACIÓN DE DATOS:';
PRINT '';

-- Mostrar algunos datos de ejemplo
DECLARE @total_facturas INT, @total_reservas INT, @total_clientes INT;

SELECT @total_facturas = COUNT(*) FROM FACTURA;
SELECT @total_reservas = COUNT(*) FROM RESERVA;
SELECT @total_clientes = COUNT(*) FROM CLIENTE;

PRINT 'TOTAL FACTURAS: ' + CAST(@total_facturas AS VARCHAR(10));
PRINT 'TOTAL RESERVAS: ' + CAST(@total_reservas AS VARCHAR(10));
PRINT 'TOTAL CLIENTES: ' + CAST(@total_clientes AS VARCHAR(10));

PRINT '';
PRINT 'ESTADOS DE FACTURAS:';
SELECT ESTADO_FACTURA, COUNT(*) AS Cantidad 
FROM FACTURA 
GROUP BY ESTADO_FACTURA;

PRINT '';
PRINT 'TIPOS DE HABITACIONES:';
SELECT TIPO_HABITACION, COUNT(*) AS Cantidad, 
       AVG(PRECIO_NOCHE) AS Precio_Promedio
FROM HABITACION 
GROUP BY TIPO_HABITACION;

PRINT '===============================================';
PRINT 'PROCESO COMPLETADO EXITOSAMENTE';
PRINT '===============================================';
GO