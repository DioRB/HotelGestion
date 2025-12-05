/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     4/12/2025 4:42:01 p. m.                      */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EMPLEADO') and o.name = 'FK_EMPLEADO_TIENE_TURNO')
alter table EMPLEADO
   drop constraint FK_EMPLEADO_TIENE_TURNO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EMPLEADO_SERVICIO') and o.name = 'FK_EMPLEADO_BRINDA_EMPLEADO')
alter table EMPLEADO_SERVICIO
   drop constraint FK_EMPLEADO_BRINDA_EMPLEADO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EMPLEADO_SERVICIO') and o.name = 'FK_EMPLEADO_BRINDA2_SERVICIO')
alter table EMPLEADO_SERVICIO
   drop constraint FK_EMPLEADO_BRINDA2_SERVICIO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ESTACIONAMIENTO') and o.name = 'FK_ESTACION_ASIGNA_RESERVA')
alter table ESTACIONAMIENTO
   drop constraint FK_ESTACION_ASIGNA_RESERVA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ESTADIA') and o.name = 'FK_ESTADIA_GENERA_FACTURA')
alter table ESTADIA
   drop constraint FK_ESTADIA_GENERA_FACTURA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ESTADIA_SERVICIO') and o.name = 'FK_ESTADIA__USA_ESTADIA')
alter table ESTADIA_SERVICIO
   drop constraint FK_ESTADIA__USA_ESTADIA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ESTADIA_SERVICIO') and o.name = 'FK_ESTADIA__USA2_SERVICIO')
alter table ESTADIA_SERVICIO
   drop constraint FK_ESTADIA__USA2_SERVICIO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FACTURA') and o.name = 'FK_FACTURA_NECESITA_EFECTIVO')
alter table FACTURA
   drop constraint FK_FACTURA_NECESITA_EFECTIVO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FACTURA') and o.name = 'FK_FACTURA_NECESITA2_TARJETA')
alter table FACTURA
   drop constraint FK_FACTURA_NECESITA2_TARJETA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FACTURA') and o.name = 'FK_FACTURA_OBTIENE_CLIENTE')
alter table FACTURA
   drop constraint FK_FACTURA_OBTIENE_CLIENTE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HABITACION') and o.name = 'FK_HABITACI_RESERVA_RESERVA')
alter table HABITACION
   drop constraint FK_HABITACI_RESERVA_RESERVA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MANTENIMIENTO') and o.name = 'FK_MANTENIM_REALIZA_EMPLEADO')
alter table MANTENIMIENTO
   drop constraint FK_MANTENIM_REALIZA_EMPLEADO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RESERVA') and o.name = 'FK_RESERVA_HACE_CLIENTE')
alter table RESERVA
   drop constraint FK_RESERVA_HACE_CLIENTE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RESERVA') and o.name = 'FK_RESERVA_PUEDE_GEN_ESTADIA')
alter table RESERVA
   drop constraint FK_RESERVA_PUEDE_GEN_ESTADIA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLIENTE')
            and   type = 'U')
   drop table CLIENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EFECTIVO')
            and   type = 'U')
   drop table EFECTIVO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EMPLEADO')
            and   name  = 'TIENE_FK'
            and   indid > 0
            and   indid < 255)
   drop index EMPLEADO.TIENE_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EMPLEADO')
            and   type = 'U')
   drop table EMPLEADO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EMPLEADO_SERVICIO')
            and   name  = 'BRINDA_FK'
            and   indid > 0
            and   indid < 255)
   drop index EMPLEADO_SERVICIO.BRINDA_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EMPLEADO_SERVICIO')
            and   name  = 'BRINDA2_FK'
            and   indid > 0
            and   indid < 255)
   drop index EMPLEADO_SERVICIO.BRINDA2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EMPLEADO_SERVICIO')
            and   type = 'U')
   drop table EMPLEADO_SERVICIO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ESTACIONAMIENTO')
            and   name  = 'ASIGNA_FK'
            and   indid > 0
            and   indid < 255)
   drop index ESTACIONAMIENTO.ASIGNA_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ESTACIONAMIENTO')
            and   type = 'U')
   drop table ESTACIONAMIENTO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ESTADIA')
            and   name  = 'GENERA_FK'
            and   indid > 0
            and   indid < 255)
   drop index ESTADIA.GENERA_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ESTADIA')
            and   type = 'U')
   drop table ESTADIA
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ESTADIA_SERVICIO')
            and   name  = 'USA_FK'
            and   indid > 0
            and   indid < 255)
   drop index ESTADIA_SERVICIO.USA_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ESTADIA_SERVICIO')
            and   name  = 'USA2_FK'
            and   indid > 0
            and   indid < 255)
   drop index ESTADIA_SERVICIO.USA2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ESTADIA_SERVICIO')
            and   type = 'U')
   drop table ESTADIA_SERVICIO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('FACTURA')
            and   name  = 'NECESITA2_FK'
            and   indid > 0
            and   indid < 255)
   drop index FACTURA.NECESITA2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('FACTURA')
            and   name  = 'OBTIENE_FK'
            and   indid > 0
            and   indid < 255)
   drop index FACTURA.OBTIENE_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FACTURA')
            and   type = 'U')
   drop table FACTURA
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HABITACION')
            and   name  = 'RESERVA_FK'
            and   indid > 0
            and   indid < 255)
   drop index HABITACION.RESERVA_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HABITACION')
            and   type = 'U')
   drop table HABITACION
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MANTENIMIENTO')
            and   name  = 'REALIZA_FK'
            and   indid > 0
            and   indid < 255)
   drop index MANTENIMIENTO.REALIZA_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MANTENIMIENTO')
            and   type = 'U')
   drop table MANTENIMIENTO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RESERVA')
            and   name  = 'PUEDE_GENERAR_FK'
            and   indid > 0
            and   indid < 255)
   drop index RESERVA.PUEDE_GENERAR_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RESERVA')
            and   name  = 'HACE_FK'
            and   indid > 0
            and   indid < 255)
   drop index RESERVA.HACE_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RESERVA')
            and   type = 'U')
   drop table RESERVA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SERVICIO')
            and   type = 'U')
   drop table SERVICIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TARJETA')
            and   type = 'U')
   drop table TARJETA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TURNO')
            and   type = 'U')
   drop table TURNO
go

/*==============================================================*/
/* Table: CLIENTE                                               */
/*==============================================================*/
create table CLIENTE (
   ID_PERSONA           int                  identity (1,1)not null,
   DOCUMENTO            int                  null,
   NOMBRE               char(50)             null,
   APELLIDO             char(50)             null,
   TELEFONO             numeric(10)          null,
   CORREO               char(60)             null,
   TIPO_CLIENTE         char(256)            not null,
   PAIS                 char(40)             not null,
   constraint PK_CLIENTE primary key nonclustered (ID_PERSONA)
)
go

/*==============================================================*/
/* Table: EFECTIVO                                              */
/*==============================================================*/
create table EFECTIVO (
   ID_TIPO_PAGO         int                  not null,
   NOMBRE_TIPO          char(256)            null,
   ATTRIBUTE_43         numeric(8,2)         not null,
   CAMBIO               numeric(8,2)         null,
   constraint PK_EFECTIVO primary key nonclustered (ID_TIPO_PAGO)
)
go

/*==============================================================*/
/* Table: EMPLEADO                                              */
/*==============================================================*/
create table EMPLEADO (
   ID_PERSONA           int                  identity (1,1) not null,
   ID_TURNO             int                  not null,
   DOCUMENTO            numeric(10)          null,
   NOMBRE               char(50)             null,
   APELLIDO             char(50)             null,
   TELEFONO             numeric(10)          null,
   CORREO               char(60)             null,
   CARGO                char(50)             not null,
   SALARIO              decimal(38)          not null,
   FECHA_CONTRATACION   datetime             not null,
   constraint PK_EMPLEADO primary key nonclustered (ID_PERSONA)
)
go

/*==============================================================*/
/* Index: TIENE_FK                                              */
/*==============================================================*/
create index TIENE_FK on EMPLEADO (
ID_TURNO ASC
)
go

/*==============================================================*/
/* Table: EMPLEADO_SERVICIO                                     */
/*==============================================================*/
create table EMPLEADO_SERVICIO (
   ID_PERSONA           int                  not null,
   ID_SERVICIO          int                  not null,
   FECHA                datetime             not null,
   constraint PK_EMPLEADO_SERVICIO primary key nonclustered (ID_PERSONA, ID_SERVICIO)
)
go

/*==============================================================*/
/* Index: BRINDA2_FK                                            */
/*==============================================================*/
create index BRINDA2_FK on EMPLEADO_SERVICIO (
ID_SERVICIO ASC
)
go

/*==============================================================*/
/* Index: BRINDA_FK                                             */
/*==============================================================*/
create index BRINDA_FK on EMPLEADO_SERVICIO (
ID_PERSONA ASC
)
go

/*==============================================================*/
/* Table: ESTACIONAMIENTO                                       */
/*==============================================================*/
create table ESTACIONAMIENTO (
   ID_ESTACIONAMIENTO   int                  identity (1,1) not null,
   ID_ESTADIA           int                  null,
   ID_RESERVA           int                  null,
   NUMERO_ASIGNADO      int                  not null,
   DISPONIBLE           smallint             not null,
   COSTO_ESTACIONAMIENTO decimal              not null,
   constraint PK_ESTACIONAMIENTO primary key nonclustered (ID_ESTACIONAMIENTO)
)
go

/*==============================================================*/
/* Index: ASIGNA_FK                                             */
/*==============================================================*/
create index ASIGNA_FK on ESTACIONAMIENTO (
ID_ESTADIA ASC,
ID_RESERVA ASC
)
go

/*==============================================================*/
/* Table: ESTADIA                                               */
/*==============================================================*/
create table ESTADIA (
   ID_ESTADIA           int                  not null,
   ID_FACTURA           int                  not null,
   FECHA_CHECK_IN       datetime             not null,
   DECHA_CHECK_OUT      datetime             not null,
   CANTIDAD_PERSONAS    smallint             not null,
   OBSERVACIONES        char(256)            not null,
   ESTADO_ESTADIA       char(256)            not null,
   constraint PK_ESTADIA primary key nonclustered (ID_ESTADIA)
)
go

/*==============================================================*/
/* Index: GENERA_FK                                             */
/*==============================================================*/
create index GENERA_FK on ESTADIA (
ID_FACTURA ASC
)
go

/*==============================================================*/
/* Table: ESTADIA_SERVICIO                                      */
/*==============================================================*/
create table ESTADIA_SERVICIO (
   ID_ESTADIA           int                  not null,
   ID_SERVICIO          int                  not null,
   FECHA_SERVICIO       datetime             null,
   constraint PK_ESTADIA_SERVICIO primary key nonclustered (ID_ESTADIA, ID_SERVICIO)
)
go

/*==============================================================*/
/* Index: USA2_FK                                               */
/*==============================================================*/
create index USA2_FK on ESTADIA_SERVICIO (
ID_SERVICIO ASC
)
go

/*==============================================================*/
/* Index: USA_FK                                                */
/*==============================================================*/
create index USA_FK on ESTADIA_SERVICIO (
ID_ESTADIA ASC
)
go

/*==============================================================*/
/* Table: FACTURA                                               */
/*==============================================================*/
create table FACTURA (
   ID_FACTURA           int                  identity (1,1)not null,
   ID_PERSONA           int                  null,
   ID_TIPO_PAGO         int                  null,
   VALOR_TOTAL          decimal(38)          not null,
   FECHA_PAGO           datetime             not null,
   ESTADO_FACTURA       char(256)            not null,
   constraint PK_FACTURA primary key nonclustered (ID_FACTURA)
)
go

/*==============================================================*/
/* Index: OBTIENE_FK                                            */
/*==============================================================*/
create index OBTIENE_FK on FACTURA (
ID_PERSONA ASC
)
go

/*==============================================================*/
/* Index: NECESITA2_FK                                          */
/*==============================================================*/
create index NECESITA2_FK on FACTURA (
ID_TIPO_PAGO ASC
)
go

/*==============================================================*/
/* Table: HABITACION                                            */
/*==============================================================*/
create table HABITACION (
   ID_HABITACION        int                  identity (1,1)not null,
   ID_ESTADIA           int                  null,
   ID_RESERVA           int                  null,
   NUMERO               int             not null,
   PISO                 int             not null,
   TIPO_HABITACION      char(256)            not null,
   CAPACIDAD            int                  not null,
   PRECIO_NOCHE         decimal(38)         not null,
   ESTADO_HABITACION    bit            not null,
   constraint PK_HABITACION primary key nonclustered (ID_HABITACION)
)
go

/*==============================================================*/
/* Index: RESERVA_FK                                            */
/*==============================================================*/
create index RESERVA_FK on HABITACION (
ID_ESTADIA ASC,
ID_RESERVA ASC
)
go

/*==============================================================*/
/* Table: MANTENIMIENTO                                         */
/*==============================================================*/
create table MANTENIMIENTO (
   ID_MANTENIMIENTO     int                  not null,
   ID_PERSONA           int                  null,
   FECHA_REPORTE        datetime             not null,
   FECHA_MANTENIMIENTO  datetime             not null,
   MOTIVO               char(200)            not null,
   COSTO_MANTENIMIENTO  decimal(38)          not null,
   constraint PK_MANTENIMIENTO primary key nonclustered (ID_MANTENIMIENTO)
)
go

/*==============================================================*/
/* Index: REALIZA_FK                                            */
/*==============================================================*/
create index REALIZA_FK on MANTENIMIENTO (
ID_PERSONA ASC
)
go

/*==============================================================*/
/* Table: RESERVA                                               */
/*==============================================================*/
create table RESERVA (
   ID_ESTADIA           int                  not null,
   ID_RESERVA           int                  identity (1,1)not null,
   ID_PERSONA           int                  null,
   FECHA_INICIO         datetime             not null,
   FECHA_FIN            datetime             not null,
   ESTADO               char(256)            not null,
   constraint PK_RESERVA primary key nonclustered (ID_ESTADIA, ID_RESERVA)
)
go

/*==============================================================*/
/* Index: HACE_FK                                               */
/*==============================================================*/
create index HACE_FK on RESERVA (
ID_PERSONA ASC
)
go

/*==============================================================*/
/* Index: PUEDE_GENERAR_FK                                      */
/*==============================================================*/
create index PUEDE_GENERAR_FK on RESERVA (
ID_ESTADIA ASC
)
go

/*==============================================================*/
/* Table: SERVICIO                                              */
/*==============================================================*/
create table SERVICIO (
   ID_SERVICIO          int                  not null,
   DESCRIPCION          char(256)            not null,
   TARIFA               decimal(38)          not null,
   NOMBRE_SERVICIO      char(60)             not null,
   constraint PK_SERVICIO primary key nonclustered (ID_SERVICIO)
)
go

/*==============================================================*/
/* Table: TARJETA                                               */
/*==============================================================*/
create table TARJETA (
   ID_TIPO_PAGO         int                  not null,
   NOMBRE_TIPO          char(256)            null,
   ID_TARJETA           int                  not null,
   NUMERO_TARJETA       int                  not null,
   TIPO_TARJETA         char(256)            not null,
   BANCO_EMISOR         char(256)            not null,
   constraint PK_TARJETA primary key nonclustered (ID_TIPO_PAGO)
)
go

/*==============================================================*/
/* Table: TURNO                                                 */
/*==============================================================*/
create table TURNO (
   ID_TURNO             int                  identity (1,1) not null,
   TIPO_TURNO           char(50)             not null,
   HORA_INICIO          datetime             not null,
   HORA_FIN             datetime             not null,
   constraint PK_TURNO primary key nonclustered (ID_TURNO)
)
go

alter table EMPLEADO
   add constraint FK_EMPLEADO_TIENE_TURNO foreign key (ID_TURNO)
      references TURNO (ID_TURNO)
go

alter table EMPLEADO_SERVICIO
   add constraint FK_EMPLEADO_BRINDA_EMPLEADO foreign key (ID_PERSONA)
      references EMPLEADO (ID_PERSONA)
go

alter table EMPLEADO_SERVICIO
   add constraint FK_EMPLEADO_BRINDA2_SERVICIO foreign key (ID_SERVICIO)
      references SERVICIO (ID_SERVICIO)
go

alter table ESTACIONAMIENTO
   add constraint FK_ESTACION_ASIGNA_RESERVA foreign key (ID_ESTADIA, ID_RESERVA)
      references RESERVA (ID_ESTADIA, ID_RESERVA)
go

alter table ESTADIA
   add constraint FK_ESTADIA_GENERA_FACTURA foreign key (ID_FACTURA)
      references FACTURA (ID_FACTURA)
go

alter table ESTADIA_SERVICIO
   add constraint FK_ESTADIA__USA_ESTADIA foreign key (ID_ESTADIA)
      references ESTADIA (ID_ESTADIA)
go

alter table ESTADIA_SERVICIO
   add constraint FK_ESTADIA__USA2_SERVICIO foreign key (ID_SERVICIO)
      references SERVICIO (ID_SERVICIO)
go

alter table FACTURA
   add constraint FK_FACTURA_NECESITA_EFECTIVO foreign key (ID_TIPO_PAGO)
      references EFECTIVO (ID_TIPO_PAGO)
go

alter table FACTURA
   add constraint FK_FACTURA_NECESITA2_TARJETA foreign key (ID_TIPO_PAGO)
      references TARJETA (ID_TIPO_PAGO)
go

alter table FACTURA
   add constraint FK_FACTURA_OBTIENE_CLIENTE foreign key (ID_PERSONA)
      references CLIENTE (ID_PERSONA)
go

alter table HABITACION
   add constraint FK_HABITACI_RESERVA_RESERVA foreign key (ID_ESTADIA, ID_RESERVA)
      references RESERVA (ID_ESTADIA, ID_RESERVA)
go

alter table MANTENIMIENTO
   add constraint FK_MANTENIM_REALIZA_EMPLEADO foreign key (ID_PERSONA)
      references EMPLEADO (ID_PERSONA)
go

alter table RESERVA
   add constraint FK_RESERVA_HACE_CLIENTE foreign key (ID_PERSONA)
      references CLIENTE (ID_PERSONA)
go

alter table RESERVA
   add constraint FK_RESERVA_PUEDE_GEN_ESTADIA foreign key (ID_ESTADIA)
      references ESTADIA (ID_ESTADIA)
go

