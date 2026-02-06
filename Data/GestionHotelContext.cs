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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=GestionHotel;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdPersona).IsClustered(false);

            entity.ToTable("CLIENTE");

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
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Pais)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PAIS");
            entity.Property(e => e.Sexo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SEXO");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TIPO_CLIENTE");
        });

        modelBuilder.Entity<Efectivo>(entity =>
        {
            entity.HasKey(e => e.IdEfectivo).IsClustered(false);

            entity.ToTable("EFECTIVO");

            entity.Property(e => e.IdEfectivo).HasColumnName("ID_EFECTIVO");
            entity.Property(e => e.Cambio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("CAMBIO");
            entity.Property(e => e.MontoEntregado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("MONTO_ENTREGADO");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdPersona).IsClustered(false);

            entity.ToTable("EMPLEADO");

            entity.HasIndex(e => e.IdTurno, "TIENE_FK");

            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Cargo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CARGO");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CORREO");
            entity.Property(e => e.Documento)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO");
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CONTRATACION");
            entity.Property(e => e.IdTurno).HasColumnName("ID_TURNO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("SALARIO");
            entity.Property(e => e.Sexo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SEXO");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_TIENE_TURNO");
        });

        modelBuilder.Entity<EmpleadoServicio>(entity =>
        {
            entity.HasKey(e => new { e.IdServicio, e.IdPersona }).IsClustered(false);

            entity.ToTable("EMPLEADO_SERVICIO");

            entity.HasIndex(e => e.IdPersona, "BRINDA2_FK");

            entity.Property(e => e.IdServicio).HasColumnName("ID_SERVICIO");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.EmpleadoServicios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_BRINDA2_EMPLEADO");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.EmpleadoServicios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_BRINDA_SERVICIO");
        });

        modelBuilder.Entity<Estacionamiento>(entity =>
        {
            entity.HasKey(e => e.IdEstacionamiento).IsClustered(false);

            entity.ToTable("ESTACIONAMIENTO");

            entity.HasIndex(e => e.IdReserva, "ASIGNA_FK");

            entity.Property(e => e.IdEstacionamiento).HasColumnName("ID_ESTACIONAMIENTO");
            entity.Property(e => e.CostoEstacionamiento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("COSTO_ESTACIONAMIENTO");
            entity.Property(e => e.Disponibilidad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DISPONIBILIDAD");
            entity.Property(e => e.IdReserva).HasColumnName("ID_RESERVA");
            entity.Property(e => e.NumeroAsignado).HasColumnName("NUMERO_ASIGNADO");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Estacionamientos)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK_ESTACION_ASIGNA_RESERVA");
        });

        modelBuilder.Entity<EstadiaServicio>(entity =>
        {
            entity.HasKey(e => new { e.IdServicio, e.IdEstadia }).IsClustered(false);

            entity.ToTable("ESTADIA_SERVICIO");

            entity.HasIndex(e => e.IdEstadia, "USA2_FK");

            entity.Property(e => e.IdServicio).HasColumnName("ID_SERVICIO");
            entity.Property(e => e.IdEstadia).HasColumnName("ID_ESTADIA");
            entity.Property(e => e.FechaServicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_SERVICIO");

            entity.HasOne(d => d.IdEstadiaNavigation).WithMany(p => p.EstadiaServicios)
                .HasForeignKey(d => d.IdEstadia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTADIA__USA2_ESTADIA");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.EstadiaServicios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTADIA__USA_SERVICIO");
        });

        modelBuilder.Entity<Estadium>(entity =>
        {
            entity.HasKey(e => e.IdEstadia).IsClustered(false);

            entity.ToTable("ESTADIA");

            entity.HasIndex(e => e.IdReserva, "PUEDE_GENERAR_FK");

            entity.Property(e => e.IdEstadia).HasColumnName("ID_ESTADIA");
            entity.Property(e => e.CantidadPersonas).HasColumnName("CANTIDAD_PERSONAS");
            entity.Property(e => e.EstadoEstadia)
                .HasMaxLength(255)
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
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("OBSERVACIONES");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Estadia)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK_ESTADIA_PUEDE_GEN_RESERVA");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).IsClustered(false);

            entity.ToTable("FACTURA");

            entity.HasIndex(e => e.IdEstadia, "GENERA_FK");

            entity.HasIndex(e => e.IdPersona, "OBTIENE_FK");

            entity.HasIndex(e => e.IdTarjeta, "SE_PAGA_CON_FK");

            entity.HasIndex(e => e.IdEfectivo, "SE_PAGA_EN_FK");

            entity.Property(e => e.IdFactura).HasColumnName("ID_FACTURA");
            entity.Property(e => e.EstadoFactura)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ESTADO_FACTURA");
            entity.Property(e => e.FechaPago)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_PAGO");
            entity.Property(e => e.IdEfectivo).HasColumnName("ID_EFECTIVO");
            entity.Property(e => e.IdEstadia).HasColumnName("ID_ESTADIA");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");
            entity.Property(e => e.IdTarjeta).HasColumnName("ID_TARJETA");
            entity.Property(e => e.ValorTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("VALOR_TOTAL");

            entity.HasOne(d => d.IdEfectivoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdEfectivo)
                .HasConstraintName("FK_FACTURA_SE_PAGA_E_EFECTIVO");

            entity.HasOne(d => d.IdEstadiaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdEstadia)
                .HasConstraintName("FK_FACTURA_GENERA_ESTADIA");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_FACTURA_OBTIENE_CLIENTE");

            entity.HasOne(d => d.IdTarjetaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdTarjeta)
                .HasConstraintName("FK_FACTURA_SE_PAGA_C_TARJETA");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).IsClustered(false);

            entity.ToTable("HABITACION");

            entity.HasIndex(e => e.IdReserva, "RESERVA_FK");

            entity.Property(e => e.IdHabitacion).HasColumnName("ID_HABITACION");
            entity.Property(e => e.Capacidad).HasColumnName("CAPACIDAD");
            entity.Property(e => e.EstadoHabitacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ESTADO_HABITACION");
            entity.Property(e => e.IdReserva).HasColumnName("ID_RESERVA");
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NUMERO");
            entity.Property(e => e.Piso)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PISO");
            entity.Property(e => e.PrecioNoche)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRECIO_NOCHE");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TIPO_HABITACION");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Habitacions)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK_HABITACI_RESERVA_RESERVA");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.IdMantenimiento).IsClustered(false);

            entity.ToTable("MANTENIMIENTO");

            entity.HasIndex(e => e.IdPersona, "REALIZA_FK");

            entity.HasIndex(e => e.IdHabitacion, "SE_LE_HACE_FK");

            entity.Property(e => e.IdMantenimiento).HasColumnName("ID_MANTENIMIENTO");
            entity.Property(e => e.CostoMantenimiento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("COSTO_MANTENIMIENTO");
            entity.Property(e => e.FechaMantenimiento)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_MANTENIMIENTO");
            entity.Property(e => e.FechaReporte)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_REPORTE");
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
            entity.HasKey(e => e.IdReserva).IsClustered(false);

            entity.ToTable("RESERVA");

            entity.HasIndex(e => e.IdPersona, "HACE_FK");

            entity.Property(e => e.IdReserva).HasColumnName("ID_RESERVA");
            entity.Property(e => e.EstadoReserva)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ESTADO_RESERVA");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_FIN");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_INICIO");
            entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_RESERVA_HACE_CLIENTE");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).IsClustered(false);

            entity.ToTable("SERVICIO");

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
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TARIFA");
        });

        modelBuilder.Entity<Tarjetum>(entity =>
        {
            entity.HasKey(e => e.IdTarjeta).IsClustered(false);

            entity.ToTable("TARJETA");

            entity.Property(e => e.IdTarjeta).HasColumnName("ID_TARJETA");
            entity.Property(e => e.BancoEmisor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("BANCO_EMISOR");
            entity.Property(e => e.NumeroTarjeta)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NUMERO_TARJETA");
            entity.Property(e => e.TipoTarjeta)
                .HasMaxLength(255)
                .IsUnicode(false)
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
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TIPO_TURNO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
