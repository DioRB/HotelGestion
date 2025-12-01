using System;
using System.Collections.Generic;
using HotelGestion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

    public virtual DbSet<Estacionamiento> Estacionamientos { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Habitacion> Habitacions { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Tarjetum> Tarjeta { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=GestionHotel;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__CLIENTE__7824414964969E67");

            entity.ToTable("CLIENTE");

            entity.HasIndex(e => e.IdPersona, "CLIENTE_PK_IDX").IsUnique();

            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CORREO");
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAIS");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIPO_CLIENTE");

            entity.HasMany(d => d.IdServicios).WithMany(p => p.IdPersonas)
                .UsingEntity<Dictionary<string, object>>(
                    "ClienteServicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("IdServicio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CLIENTE__USA2_SERVICIO"),
                    l => l.HasOne<Cliente>().WithMany()
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CLIENTE__USA_CLIENTE"),
                    j =>
                    {
                        j.HasKey("IdPersona", "IdServicio");
                        j.ToTable("CLIENTE_SERVICIO");
                        j.HasIndex(new[] { "IdPersona" }, "IX_CLIENTE_SERVICIO_ID_PERSONA");
                        j.HasIndex(new[] { "IdServicio" }, "IX_CLIENTE_SERVICIO_ID_SERVICIO");
                        j.IndexerProperty<int>("IdPersona").HasColumnName("ID_PERSONA");
                        j.IndexerProperty<int>("IdServicio").HasColumnName("ID_SERVICIO");
                    });
        });

        modelBuilder.Entity<Efectivo>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__EFECTIVO__5A5E9B59FF7D9124");

            entity.ToTable("EFECTIVO");

            entity.HasIndex(e => e.IdTipoPago, "EFECTIVO_PK_IDX").IsUnique();

            entity.Property(e => e.IdTipoPago).HasColumnName("ID_TIPO_PAGO");
            entity.Property(e => e.Attribute43)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("ATTRIBUTE_43");
            entity.Property(e => e.Cambio)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("CAMBIO");
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_TIPO");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__EMPLEADO__782441496D469990");

            entity.ToTable("EMPLEADO");

            entity.HasIndex(e => e.IdPersona, "EMPLEADO_PK_IDX").IsUnique();

            entity.HasIndex(e => e.IdTurno, "IX_EMPLEADO_ID_TURNO");

            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Cargo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CARGO");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CORREO");
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO");
            entity.Property(e => e.FechaContratacion).HasColumnName("FECHA_CONTRATACION");
            entity.Property(e => e.IdTurno).HasColumnName("ID_TURNO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("SALARIO");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_TIENE_TURNO");

            entity.HasMany(d => d.IdServicios).WithMany(p => p.IdPersonasNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "EmpleadoServicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("IdServicio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EMPLEADO_BRINDA2_SERVICIO"),
                    l => l.HasOne<Empleado>().WithMany()
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EMPLEADO_BRINDA_EMPLEADO"),
                    j =>
                    {
                        j.HasKey("IdPersona", "IdServicio");
                        j.ToTable("EMPLEADO_SERVICIO");
                        j.HasIndex(new[] { "IdPersona" }, "IX_EMPLEADO_SERVICIO_ID_PERSONA");
                        j.HasIndex(new[] { "IdServicio" }, "IX_EMPLEADO_SERVICIO_ID_SERVICIO");
                        j.IndexerProperty<int>("IdPersona").HasColumnName("ID_PERSONA");
                        j.IndexerProperty<int>("IdServicio").HasColumnName("ID_SERVICIO");
                    });
        });

        modelBuilder.Entity<Estacionamiento>(entity =>
        {
            entity.HasKey(e => e.IdEstacionamiento).HasName("PK__ESTACION__7DD9E7765A6588A1");

            entity.ToTable("ESTACIONAMIENTO");

            entity.HasIndex(e => e.IdReserva, "IX_ESTACIONAMIENTO_ID_RESERVA");

            entity.Property(e => e.IdEstacionamiento)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_ESTACIONAMIENTO");
            entity.Property(e => e.CostoEstacionamiento)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("COSTO_ESTACIONAMIENTO");
            entity.Property(e => e.Disponible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DISPONIBLE");
            entity.Property(e => e.IdReserva).HasColumnName("ID_RESERVA");
            entity.Property(e => e.NumeroAsignado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NUMERO_ASIGNADO");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Estacionamientos)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK_ESTACION_SE_ASIGNA_RESERVA");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__FACTURA__4A921BED6AF2E2B3");

            entity.ToTable("FACTURA");

            entity.HasIndex(e => e.IdFactura, "FACTURA_PK_IDX").IsUnique();

            entity.HasIndex(e => e.IdPersona, "IX_FACTURA_ID_PERSONA");

            entity.HasIndex(e => e.IdTipoPago, "IX_FACTURA_ID_TIPO_PAGO");

            entity.Property(e => e.IdFactura).HasColumnName("ID_FACTURA");
            entity.Property(e => e.EstadoFactura)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ESTADO_FACTURA");
            entity.Property(e => e.FechaPago).HasColumnName("FECHA_PAGO");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.IdTipoPago).HasColumnName("ID_TIPO_PAGO");
            entity.Property(e => e.ValorTotal)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("VALOR_TOTAL");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_FACTURA_RECIBE_CLIENTE");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("FK_FACTURA_NECESITA_EFECTIVO");

            entity.HasOne(d => d.IdTipoPago1).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("FK_FACTURA_NECESITA2_TARJETA");

            entity.HasMany(d => d.IdServicios).WithMany(p => p.IdFacturas)
                .UsingEntity<Dictionary<string, object>>(
                    "FacturaServicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("IdServicio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FACTURA__INCLUYE2_SERVICIO"),
                    l => l.HasOne<Factura>().WithMany()
                        .HasForeignKey("IdFactura")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FACTURA__INCLUYE_FACTURA"),
                    j =>
                    {
                        j.HasKey("IdFactura", "IdServicio");
                        j.ToTable("FACTURA_SERVICIO");
                        j.HasIndex(new[] { "IdFactura" }, "IX_FACTURA_SERVICIO_ID_FACTURA");
                        j.HasIndex(new[] { "IdServicio" }, "IX_FACTURA_SERVICIO_ID_SERVICIO");
                        j.IndexerProperty<int>("IdFactura").HasColumnName("ID_FACTURA");
                        j.IndexerProperty<int>("IdServicio").HasColumnName("ID_SERVICIO");
                    });
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__HABITACI__BBD249501086F995");

            entity.ToTable("HABITACION");

            entity.HasIndex(e => e.IdHabitacion, "HABITACION_PK_IDX").IsUnique();

            entity.Property(e => e.IdHabitacion).HasColumnName("ID_HABITACION");
            entity.Property(e => e.Capacidad).HasColumnName("CAPACIDAD");
            entity.Property(e => e.EstadoHabitacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ESTADO_HABITACION");
            entity.Property(e => e.Numero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO");
            entity.Property(e => e.Piso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PISO");
            entity.Property(e => e.PrecioNoche)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PRECIO_NOCHE");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIPO_HABITACION");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.IdMantenimiento).HasName("PK__MANTENIM__5B1EDD46B0BA068E");

            entity.ToTable("MANTENIMIENTO");

            entity.HasIndex(e => e.IdHabitacion, "IX_MANTENIMIENTO_ID_HABITACION");

            entity.HasIndex(e => e.IdPersona, "IX_MANTENIMIENTO_ID_PERSONA");

            entity.Property(e => e.IdMantenimiento).HasColumnName("ID_MANTENIMIENTO");
            entity.Property(e => e.CostoMantenimiento)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("COSTO_MANTENIMIENTO");
            entity.Property(e => e.FechaMantenimiento).HasColumnName("FECHA_MANTENIMIENTO");
            entity.Property(e => e.FechaReporte).HasColumnName("FECHA_REPORTE");
            entity.Property(e => e.IdHabitacion).HasColumnName("ID_HABITACION");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.Motivo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MOTIVO");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdHabitacion)
                .HasConstraintName("FK_MANTENIM_SE_LE_HAC_HABITACI");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_MANTENIM_REALIZA_EMPLEADO");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__RESERVA__1ED54AE33DA77E36");

            entity.ToTable("RESERVA");

            entity.HasIndex(e => e.IdPersona, "IX_RESERVA_ID_PERSONA");

            entity.Property(e => e.IdReserva).HasColumnName("ID_RESERVA");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ESTADO");
            entity.Property(e => e.FechaFin).HasColumnName("FECHA_FIN");
            entity.Property(e => e.FechaInicio).HasColumnName("FECHA_INICIO");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_RESERVA_HACE_CLIENTE");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__SERVICIO__C8BDE0EBCF36FB77");

            entity.ToTable("SERVICIO");

            entity.HasIndex(e => e.IdServicio, "SERVICIO_PK_IDX").IsUnique();

            entity.Property(e => e.IdServicio).HasColumnName("ID_SERVICIO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_SERVICIO");
            entity.Property(e => e.Tarifa)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TARIFA");

            entity.HasMany(d => d.IdHabitacions).WithMany(p => p.IdServicios)
                .UsingEntity<Dictionary<string, object>>(
                    "HabitacionServicio",
                    r => r.HasOne<Habitacion>().WithMany()
                        .HasForeignKey("IdHabitacion")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_HABITACI_ASIGNA2_HABITACI"),
                    l => l.HasOne<Servicio>().WithMany()
                        .HasForeignKey("IdServicio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_HABITACI_ASIGNA_SERVICIO"),
                    j =>
                    {
                        j.HasKey("IdServicio", "IdHabitacion");
                        j.ToTable("HABITACION_SERVICIO");
                        j.HasIndex(new[] { "IdHabitacion" }, "IX_HABITACION_SERVICIO_ID_HABITACION");
                        j.HasIndex(new[] { "IdServicio" }, "IX_HABITACION_SERVICIO_ID_SERVICIO");
                        j.IndexerProperty<int>("IdServicio").HasColumnName("ID_SERVICIO");
                        j.IndexerProperty<int>("IdHabitacion").HasColumnName("ID_HABITACION");
                    });
        });

        modelBuilder.Entity<Tarjetum>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__TARJETA__5A5E9B59ECCF0A75");

            entity.ToTable("TARJETA");

            entity.HasIndex(e => e.IdTipoPago, "TARJETA_PK_IDX").IsUnique();

            entity.Property(e => e.IdTipoPago).HasColumnName("ID_TIPO_PAGO");
            entity.Property(e => e.BancoEmisor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("BANCO_EMISOR");
            entity.Property(e => e.IdTarjeta).HasColumnName("ID_TARJETA");
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_TIPO");
            entity.Property(e => e.NumeroTarjeta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO_TARJETA");
            entity.Property(e => e.TipoTarjeta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIPO_TARJETA");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PK__TURNO__F1C3D873F298DDD6");

            entity.ToTable("TURNO");

            entity.HasIndex(e => e.IdTurno, "TURNO_PK_IDX").IsUnique();

            entity.Property(e => e.IdTurno).HasColumnName("ID_TURNO");
            entity.Property(e => e.HoraFin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HORA_FIN");
            entity.Property(e => e.HoraInicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HORA_INICIO");
            entity.Property(e => e.TipoTurno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIPO_TURNO");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC0701161F1E");

            entity.ToTable("Usuario");

            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
