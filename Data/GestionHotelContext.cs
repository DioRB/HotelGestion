using System;
using System.Collections.Generic;
using HotelGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelGestion.Data;

public partial class GestionHotelContext : DbContext
{
    public GestionHotelContext()
    {
    }

    public GestionHotelContext(DbContextOptions<GestionHotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Efectivo> Efectivos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EmpleadoServicio> EmpleadoServicios { get; set; }

    public virtual DbSet<Estacionamiento> Estacionamientos { get; set; }

    public virtual DbSet<EstadiaServicio> EstadiaServicios { get; set; }

    public virtual DbSet<Estadium> Estadia { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Habitacion> Habitacions { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Tarjetum> Tarjeta { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=GestionHotel;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__CLIENTE__7824414984A8000D");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.IdPersona)
                .ValueGeneratedNever()
                .HasColumnName("ID_PERSONA");
            entity.Property(e => e.Apellido)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Correo)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("CORREO");
            entity.Property(e => e.Documento)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Pais)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("PAIS");
            entity.Property(e => e.Telefono)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("TIPO_CLIENTE");
        });

        modelBuilder.Entity<Efectivo>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__EFECTIVO__5A5E9B59BCEAF11D");

            entity.ToTable("EFECTIVO");

            entity.Property(e => e.IdTipoPago)
                .ValueGeneratedNever()
                .HasColumnName("ID_TIPO_PAGO");
            entity.Property(e => e.Attribute43)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("ATTRIBUTE_43");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__EMPLEADO__78244149C91574B3");

            entity.ToTable("EMPLEADO");

            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.Apellido)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Cargo)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("CARGO");
            entity.Property(e => e.Correo)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("CORREO");
            entity.Property(e => e.Documento)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO");
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CONTRATACION");
            entity.Property(e => e.IdTurno).HasColumnName("ID_TURNO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("SALARIO");
            entity.Property(e => e.Telefono)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
        });

        modelBuilder.Entity<EmpleadoServicio>(entity =>
        {
            entity.HasKey(e => new { e.IdPersona, e.IdServicio }).HasName("PK__EMPLEADO__D4AF9F4767E4CAA5");

            entity.ToTable("EMPLEADO_SERVICIO");

            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.IdServicio).HasColumnName("ID_SERVICIO");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
        });

        modelBuilder.Entity<Estacionamiento>(entity =>
        {
            entity.HasKey(e => e.IdEstacionamiento).HasName("PK__ESTACION__7DD9E776D4045AB6");

            entity.ToTable("ESTACIONAMIENTO");

            entity.Property(e => e.IdEstacionamiento)
                .ValueGeneratedNever()
                .HasColumnName("ID_ESTACIONAMIENTO");
            entity.Property(e => e.CostoEstacionamiento)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("COSTO_ESTACIONAMIENTO");
            entity.Property(e => e.Disponible)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("DISPONIBLE");
            entity.Property(e => e.IdReserva).HasColumnName("ID_RESERVA");
            entity.Property(e => e.NumeroAsignado).HasColumnName("NUMERO_ASIGNADO");
        });

        modelBuilder.Entity<EstadiaServicio>(entity =>
        {
            entity.HasKey(e => new { e.IdEstadia, e.IdServicio }).HasName("PK__ESTADIA___F6ABEF1233C63BFC");

            entity.ToTable("ESTADIA_SERVICIO");

            entity.Property(e => e.IdEstadia).HasColumnName("ID_ESTADIA");
            entity.Property(e => e.IdServicio).HasColumnName("ID_SERVICIO");
            entity.Property(e => e.FechaServicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_SERVICIO");
        });

        modelBuilder.Entity<Estadium>(entity =>
        {
            entity.HasKey(e => e.IdEstadia).HasName("PK__ESTADIA__5A20311C2691DBA6");

            entity.ToTable("ESTADIA");

            entity.Property(e => e.IdEstadia)
                .ValueGeneratedNever()
                .HasColumnName("ID_ESTADIA");
            entity.Property(e => e.CantidadPersonas).HasColumnName("CANTIDAD_PERSONAS");
            entity.Property(e => e.EstadoEstadia)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("ESTADO_ESTADIA");
            entity.Property(e => e.FechaCheckIn)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CHECK_IN");
            entity.Property(e => e.FechaCheckOut)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CHECK_OUT");
            entity.Property(e => e.IdReserva).HasColumnName("ID_RESERVA");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("OBSERVACIONES");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__FACTURA__4A921BED6FEF5EFE");

            entity.ToTable("FACTURA");

            entity.Property(e => e.IdFactura)
                .ValueGeneratedNever()
                .HasColumnName("ID_FACTURA");
            entity.Property(e => e.EstadoFactura)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("ESTADO_FACTURA");
            entity.Property(e => e.FechaPago)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_PAGO");
            entity.Property(e => e.IdEstadia).HasColumnName("ID_ESTADIA");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.IdTipoPagoEfectivo).HasColumnName("ID_TIPO_PAGO_EFECTIVO");
            entity.Property(e => e.IdTipoPagoTarjeta).HasColumnName("ID_TIPO_PAGO_TARJETA");
            entity.Property(e => e.ValorTotal)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("VALOR_TOTAL");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__HABITACI__BBD249503410FD2E");

            entity.ToTable("HABITACION");

            entity.Property(e => e.IdHabitacion).HasColumnName("ID_HABITACION");
            entity.Property(e => e.Capacidad).HasColumnName("CAPACIDAD");
            entity.Property(e => e.EstadoHabitacion)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("ESTADO_HABITACION");
            entity.Property(e => e.IdReserva).HasColumnName("ID_RESERVA");
            entity.Property(e => e.Numero)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("NUMERO");
            entity.Property(e => e.Piso).HasColumnName("PISO");
            entity.Property(e => e.PrecioNoche)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("PRECIO_NOCHE");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("TIPO_HABITACION");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.IdMantenimiento).HasName("PK__MANTENIM__5B1EDD46A2A0AC00");

            entity.ToTable("MANTENIMIENTO");

            entity.Property(e => e.IdMantenimiento)
                .ValueGeneratedNever()
                .HasColumnName("ID_MANTENIMIENTO");
            entity.Property(e => e.CostoMantenimiento)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("COSTO_MANTENIMIENTO");
            entity.Property(e => e.FechaMantenimiento)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_MANTENIMIENTO");
            entity.Property(e => e.FechaReporte)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_REPORTE");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.Motivo)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("MOTIVO");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__RESERVA__1ED54AE384A5A5EB");

            entity.ToTable("RESERVA");

            entity.Property(e => e.IdReserva)
                .ValueGeneratedNever()
                .HasColumnName("ID_RESERVA");
            entity.Property(e => e.Estado)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("ESTADO");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_FIN");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_INICIO");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__SERVICIO__C8BDE0EB1F12AD55");

            entity.ToTable("SERVICIO");

            entity.Property(e => e.IdServicio)
                .ValueGeneratedNever()
                .HasColumnName("ID_SERVICIO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_SERVICIO");
            entity.Property(e => e.Tarifa)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("TARIFA");
        });

        modelBuilder.Entity<Tarjetum>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__TARJETA__5A5E9B5905CFC871");

            entity.ToTable("TARJETA");

            entity.Property(e => e.IdTipoPago)
                .ValueGeneratedNever()
                .HasColumnName("ID_TIPO_PAGO");
            entity.Property(e => e.BancoEmisor)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("BANCO_EMISOR");
            entity.Property(e => e.IdTarjeta).HasColumnName("ID_TARJETA");
            entity.Property(e => e.NumeroTarjeta)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("NUMERO_TARJETA");
            entity.Property(e => e.TipoTarjeta)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("TIPO_TARJETA");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PK__TURNO__F1C3D8735B6A4EB0");

            entity.ToTable("TURNO");

            entity.Property(e => e.IdTurno).HasColumnName("ID_TURNO");
            entity.Property(e => e.HoraFin)
                .HasColumnType("datetime")
                .HasColumnName("HORA_FIN");
            entity.Property(e => e.HoraInicio)
                .HasColumnType("datetime")
                .HasColumnName("HORA_INICIO");
            entity.Property(e => e.TipoTurno)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("TIPO_TURNO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
