using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace seguros_restapi.Models;

public partial class SegurosContext : DbContext
{
    public SegurosContext()
    {
    }

    public SegurosContext(DbContextOptions<SegurosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<EstadosSolicitud> EstadosSolicituds { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    public virtual DbSet<Poliza> Polizas { get; set; }

    public virtual DbSet<ProductosPermitido> ProductosPermitidos { get; set; }

    public virtual DbSet<Seguro> Seguros { get; set; }

    public virtual DbSet<Solicitude> Solicitudes { get; set; }

    public virtual DbSet<TipoCuenta> TipoCuentas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Seguros;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__clientes__40F9A2075B2540DA");

            entity.ToTable("clientes");

            entity.Property(e => e.Codigo)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.FechaNac)
                .HasColumnType("date")
                .HasColumnName("fecha_nac");
            entity.Property(e => e.IdTipoCuenta).HasColumnName("id_tipo_cuenta");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroCuenta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_cuenta");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("segundo_apellido");

            entity.HasOne(d => d.IdTipoCuentaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdTipoCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__clientes__id_tip__3B75D760");
        });

        modelBuilder.Entity<EstadosSolicitud>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__estados___3213E83FA31BF9D9");

            entity.ToTable("estados_solicitud");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__planes__40F9A2076A9EC809");

            entity.ToTable("planes");

            entity.Property(e => e.Codigo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.CodigoSeguro)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("codigo_seguro");
            entity.Property(e => e.Cuota)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cuota");
            entity.Property(e => e.MaxEdad).HasColumnName("max_edad");
            entity.Property(e => e.MinEdad).HasColumnName("min_edad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.CodigoSeguroNavigation).WithMany(p => p.Planes)
                .HasForeignKey(d => d.CodigoSeguro)
                .HasConstraintName("FK__planes__codigo_s__4E88ABD4");
        });

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__polizas__40F9A207E70C8FC2");

            entity.ToTable("polizas");

            entity.Property(e => e.Codigo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("date")
                .HasColumnName("fecha_venta");
            entity.Property(e => e.IdSolicitud).HasColumnName("id_solicitud");

            entity.HasOne(d => d.IdSolicitudNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdSolicitud)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__polizas__id_soli__49C3F6B7");
        });

        modelBuilder.Entity<ProductosPermitido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3213E83F1315C03F");

            entity.ToTable("productos_permitidos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodigoSeguro)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("codigo_seguro");
            entity.Property(e => e.IdTipoCuenta).HasColumnName("id_tipo_cuenta");

            entity.HasOne(d => d.CodigoSeguroNavigation).WithMany(p => p.ProductosPermitidos)
                .HasForeignKey(d => d.CodigoSeguro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productos__codig__4CA06362");

            entity.HasOne(d => d.IdTipoCuentaNavigation).WithMany(p => p.ProductosPermitidos)
                .HasForeignKey(d => d.IdTipoCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productos__id_ti__4D94879B");
        });

        modelBuilder.Entity<Seguro>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__seguros__40F9A2070F7D8DE1");

            entity.ToTable("seguros");

            entity.Property(e => e.Codigo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Solicitude>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__solicitu__3213E83F03137E35");

            entity.ToTable("solicitudes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodigoCliente)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("codigo_cliente");
            entity.Property(e => e.CodigoEstado).HasColumnName("codigo_estado");
            entity.Property(e => e.CodigoPlan)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("codigo_plan");
            entity.Property(e => e.CodigoSeguro)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("codigo_seguro");

            entity.HasOne(d => d.CodigoClienteNavigation).WithMany(p => p.Solicitudes)
                .HasForeignKey(d => d.CodigoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__solicitud__codig__440B1D61");

            entity.HasOne(d => d.CodigoEstadoNavigation).WithMany(p => p.Solicitudes)
                .HasForeignKey(d => d.CodigoEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__solicitud__codig__46E78A0C");

            entity.HasOne(d => d.CodigoPlanNavigation).WithMany(p => p.Solicitudes)
                .HasForeignKey(d => d.CodigoPlan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__solicitud__codig__45F365D3");

            entity.HasOne(d => d.CodigoSeguroNavigation).WithMany(p => p.Solicitudes)
                .HasForeignKey(d => d.CodigoSeguro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__solicitud__codig__44FF419A");
        });

        modelBuilder.Entity<TipoCuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tipo_cue__3213E83F103B45B9");

            entity.ToTable("tipo_cuentas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
