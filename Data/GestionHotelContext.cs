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
            entity.HasKey(e => e.IdPersona).IsClustered(false);

            entity.ToTable("CLIENTE");

            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CORREO");
            entity.Property(e => e.Documento).HasColumnName("DOCUMENTO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Pais)
                .HasMaxLength(40)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAIS");
            entity.Property(e => e.Telefono)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("TELEFONO");
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TIPO_CLIENTE");
        });

        modelBuilder.Entity<Efectivo>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).IsClustered(false);

            entity.ToTable("EFECTIVO");

            entity.Property(e => e.IdTipoPago)
                .ValueGeneratedNever()
                .HasColumnName("ID_TIPO_PAGO");
            entity.Property(e => e.Attribute43)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("ATTRIBUTE_43");
            entity.Property(e => e.Cambio)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("CAMBIO");
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NOMBRE_TIPO");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdPersona).IsClustered(false);

            entity.ToTable("EMPLEADO");

            entity.HasIndex(e => e.IdTurno, "TIENE_FK");

            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CARGO");
            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CORREO");
            entity.Property(e => e.Documento)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("DOCUMENTO");
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CONTRATACION");
            entity.Property(e => e.IdTurno).HasColumnName("ID_TURNO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(38, 0)")
                .HasColumnName("SALARIO");
            entity.Property(e => e.Telefono)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_TIENE_TURNO");
        });

        modelBuilder.Entity<EmpleadoServicio>(entity =>
        {
            entity.HasKey(e => new { e.IdPersona, e.IdServicio }).IsClustered(false);

            entity.ToTable("EMPLEADO_SERVICIO");

            entity.HasIndex(e => e.IdServicio, "BRINDA2_FK");

            entity.HasIndex(e => e.IdPersona, "BRINDA_FK");

            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.IdServicio).HasColumnName("ID_SERVICIO");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.EmpleadoServicios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_BRINDA_EMPLEADO");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.EmpleadoServicios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_BRINDA2_SERVICIO");
        });

        modelBuilder.Entity<Estacionamiento>(entity =>
        {
            entity.HasKey(e => e.IdEstacionamiento).IsClustered(false);

            entity.ToTable("ESTACIONAMIENTO");

            entity.HasIndex(e => new { e.IdEstadia, e.IdReserva }, "ASIGNA_FK");

            entity.Property(e => e.IdEstacionamiento).HasColumnName("ID_ESTACIONAMIENTO");
            entity.Property(e => e.CostoEstacionamiento)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("COSTO_ESTACIONAMIENTO");
            entity.Property(e => e.Disponible).HasColumnName("DISPONIBLE");
            entity.Property(e => e.IdEstadia).HasColumnName("ID_ESTADIA");
            entity.Property(e => e.IdReserva).HasColumnName("ID_RESERVA");
            entity.Property(e => e.NumeroAsignado).HasColumnName("NUMERO_ASIGNADO");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Estacionamientos)
                .HasForeignKey(d => new { d.IdEstadia, d.IdReserva })
                .HasConstraintName("FK_ESTACION_ASIGNA_RESERVA");
        });

        modelBuilder.Entity<EstadiaServicio>(entity =>
        {
            entity.HasKey(e => new { e.IdEstadia, e.IdServicio }).IsClustered(false);

            entity.ToTable("ESTADIA_SERVICIO");

            entity.HasIndex(e => e.IdServicio, "USA2_FK");

            entity.HasIndex(e => e.IdEstadia, "USA_FK");

            entity.Property(e => e.IdEstadia).HasColumnName("ID_ESTADIA");
            entity.Property(e => e.IdServicio).HasColumnName("ID_SERVICIO");
            entity.Property(e => e.FechaServicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_SERVICIO");

            entity.HasOne(d => d.IdEstadiaNavigation).WithMany(p => p.EstadiaServicios)
                .HasForeignKey(d => d.IdEstadia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTADIA__USA_ESTADIA");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.EstadiaServicios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTADIA__USA2_SERVICIO");
        });

        modelBuilder.Entity<Estadium>(entity =>
        {
            entity.HasKey(e => e.IdEstadia).IsClustered(false);

            entity.ToTable("ESTADIA");

            entity.HasIndex(e => e.IdFactura, "GENERA_FK");

            entity.Property(e => e.IdEstadia)
                .ValueGeneratedNever()
                .HasColumnName("ID_ESTADIA");
            entity.Property(e => e.CantidadPersonas).HasColumnName("CANTIDAD_PERSONAS");
            entity.Property(e => e.DechaCheckOut)
                .HasColumnType("datetime")
                .HasColumnName("DECHA_CHECK_OUT");
            entity.Property(e => e.EstadoEstadia)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ESTADO_ESTADIA");
            entity.Property(e => e.FechaCheckIn)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CHECK_IN");
            entity.Property(e => e.IdFactura).HasColumnName("ID_FACTURA");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("OBSERVACIONES");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Estadia)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTADIA_GENERA_FACTURA");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).IsClustered(false);

            entity.ToTable("FACTURA");

            entity.HasIndex(e => e.IdTipoPago, "NECESITA2_FK");

            entity.HasIndex(e => e.IdPersona, "OBTIENE_FK");

            entity.Property(e => e.IdFactura).HasColumnName("ID_FACTURA");
            entity.Property(e => e.EstadoFactura)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ESTADO_FACTURA");
            entity.Property(e => e.FechaPago)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_PAGO");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.IdTipoPago).HasColumnName("ID_TIPO_PAGO");
            entity.Property(e => e.ValorTotal)
                .HasColumnType("decimal(38, 0)")
                .HasColumnName("VALOR_TOTAL");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_FACTURA_OBTIENE_CLIENTE");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("FK_FACTURA_NECESITA_EFECTIVO");

            entity.HasOne(d => d.IdTipoPago1).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("FK_FACTURA_NECESITA2_TARJETA");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).IsClustered(false);

            entity.ToTable("HABITACION");

            entity.HasIndex(e => new { e.IdEstadia, e.IdReserva }, "RESERVA_FK");

            entity.Property(e => e.IdHabitacion).HasColumnName("ID_HABITACION");
            entity.Property(e => e.Capacidad).HasColumnName("CAPACIDAD");
            entity.Property(e => e.EstadoHabitacion).HasColumnName("ESTADO_HABITACION");
            entity.Property(e => e.IdEstadia).HasColumnName("ID_ESTADIA");
            entity.Property(e => e.IdReserva).HasColumnName("ID_RESERVA");
            entity.Property(e => e.Numero).HasColumnName("NUMERO");
            entity.Property(e => e.Piso).HasColumnName("PISO");
            entity.Property(e => e.PrecioNoche)
                .HasColumnType("decimal(38, 0)")
                .HasColumnName("PRECIO_NOCHE");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TIPO_HABITACION");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Habitacions)
                .HasForeignKey(d => new { d.IdEstadia, d.IdReserva })
                .HasConstraintName("FK_HABITACI_RESERVA_RESERVA");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.IdMantenimiento).IsClustered(false);

            entity.ToTable("MANTENIMIENTO");

            entity.HasIndex(e => e.IdPersona, "REALIZA_FK");

            entity.Property(e => e.IdMantenimiento)
                .ValueGeneratedNever()
                .HasColumnName("ID_MANTENIMIENTO");
            entity.Property(e => e.CostoMantenimiento)
                .HasColumnType("decimal(38, 0)")
                .HasColumnName("COSTO_MANTENIMIENTO");
            entity.Property(e => e.FechaMantenimiento)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_MANTENIMIENTO");
            entity.Property(e => e.FechaReporte)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_REPORTE");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MOTIVO");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_MANTENIM_REALIZA_EMPLEADO");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => new { e.IdEstadia, e.IdReserva }).IsClustered(false);

            entity.ToTable("RESERVA");

            entity.HasIndex(e => e.IdPersona, "HACE_FK");

            entity.HasIndex(e => e.IdEstadia, "PUEDE_GENERAR_FK");

            entity.Property(e => e.IdEstadia).HasColumnName("ID_ESTADIA");
            entity.Property(e => e.IdReserva)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_RESERVA");
            entity.Property(e => e.Estado)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ESTADO");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_FIN");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_INICIO");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");

            entity.HasOne(d => d.IdEstadiaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstadia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESERVA_PUEDE_GEN_ESTADIA");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_RESERVA_HACE_CLIENTE");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).IsClustered(false);

            entity.ToTable("SERVICIO");

            entity.Property(e => e.IdServicio)
                .ValueGeneratedNever()
                .HasColumnName("ID_SERVICIO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(60)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NOMBRE_SERVICIO");
            entity.Property(e => e.Tarifa)
                .HasColumnType("decimal(38, 0)")
                .HasColumnName("TARIFA");
        });

        modelBuilder.Entity<Tarjetum>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).IsClustered(false);

            entity.ToTable("TARJETA");

            entity.Property(e => e.IdTipoPago)
                .ValueGeneratedNever()
                .HasColumnName("ID_TIPO_PAGO");
            entity.Property(e => e.BancoEmisor)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BANCO_EMISOR");
            entity.Property(e => e.IdTarjeta).HasColumnName("ID_TARJETA");
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NOMBRE_TIPO");
            entity.Property(e => e.NumeroTarjeta).HasColumnName("NUMERO_TARJETA");
            entity.Property(e => e.TipoTarjeta)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TIPO_TARJETA");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).IsClustered(false);

            entity.ToTable("TURNO");

            entity.Property(e => e.IdTurno).HasColumnName("ID_TURNO");
            entity.Property(e => e.HoraFin)
                .HasColumnType("datetime")
                .HasColumnName("HORA_FIN");
            entity.Property(e => e.HoraInicio)
                .HasColumnType("datetime")
                .HasColumnName("HORA_INICIO");
            entity.Property(e => e.TipoTurno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TIPO_TURNO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
