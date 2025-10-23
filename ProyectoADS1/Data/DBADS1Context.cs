using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoADS1.Models;

namespace ProyectoADS1.Data;

public partial class DBADS1Context : DbContext
{
    public DBADS1Context()
    {
    }

    public DBADS1Context(DbContextOptions<DBADS1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ActaInspeccion> ActaInspeccions { get; set; }

    public virtual DbSet<Concesionario> Concesionarios { get; set; }

    public virtual DbSet<FichaInspeccion> FichaInspeccions { get; set; }

    public virtual DbSet<InformeInspeccion> InformeInspeccions { get; set; }

    public virtual DbSet<MemorandumInspeccion> MemorandumInspeccions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActaInspeccion>(entity =>
        {
            entity.HasKey(e => e.IdInspeccion).HasName("PK__ActaInsp__2929253F64C3558B");

            entity.Property(e => e.IdInspeccion).ValueGeneratedNever();

            entity.HasOne(d => d.IdInspeccionNavigation).WithOne(p => p.ActaInspeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActaInspe__IdIns__04E4BC85");
        });

        modelBuilder.Entity<Concesionario>(entity =>
        {
            entity.HasKey(e => e.IdConcesionario).HasName("PK__Concesio__49EF4257A6FA8DDD");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ruc).IsFixedLength();
        });

        modelBuilder.Entity<FichaInspeccion>(entity =>
        {
            entity.HasKey(e => e.IdInspeccion).HasName("PK__FichaIns__2929253F94084335");

            entity.Property(e => e.Estado).HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaActualizada).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ruc).IsFixedLength();

            entity.HasOne(d => d.IdConcesionarioNavigation).WithMany(p => p.FichaInspeccions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FichaInsp__IdCon__5EBF139D");
        });

        modelBuilder.Entity<InformeInspeccion>(entity =>
        {
            entity.HasKey(e => e.IdInspeccion).HasName("PK__InformeI__2929253F400F5D12");

            entity.Property(e => e.IdInspeccion).ValueGeneratedNever();
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FirmaCoordinador).HasDefaultValue(false);
            entity.Property(e => e.FirmaSupervisor).HasDefaultValue(false);

            entity.HasOne(d => d.IdInspeccionNavigation).WithOne(p => p.InformeInspeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InformeIn__IdIns__2CF2ADDF");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.InformeInspeccions).HasConstraintName("FK__InformeIn__IdUsu__2DE6D218");
        });

        modelBuilder.Entity<MemorandumInspeccion>(entity =>
        {
            entity.HasKey(e => e.IdMemo).HasName("PK__Memorand__4D7D50869C42DFBC");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FirmaCoordinador).HasDefaultValue(false);
            entity.Property(e => e.FirmaCoordinadorGeneral).HasDefaultValue(false);
            entity.Property(e => e.FirmaDirectorGeneral).HasDefaultValue(false);

            entity.HasOne(d => d.IdInspeccionNavigation).WithOne(p => p.MemorandumInspeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Memorandu__IdIns__498EEC8D");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6DDB296B6");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rol).HasDefaultValue("usuario");
        });

        OnModelCreatingPartial(modelBuilder);

        // 🔹 Datos iniciales de Concesionarios
        modelBuilder.Entity<Concesionario>().HasData(
            new Concesionario { IdConcesionario = 1, NombreComercial = "Postal Express", RazonSocial = "Concesionaria Postal Express S.A.C.", Ruc = "20451234567", Direccion = "Av. Arequipa 1234", Departamento = "Lima", Provincia = "Lima", Distrito = "Miraflores", Telefono = "012345678", Email = "contacto@postalexpress.com" },
            new Concesionario { IdConcesionario = 2, NombreComercial = "Red Courier", RazonSocial = "Red Courier Perú E.I.R.L.", Ruc = "20567891234", Direccion = "Calle Comercio 456", Departamento = "Arequipa", Provincia = "Arequipa", Distrito = "Cercado", Telefono = "054987654", Email = "info@redcourier.pe" },
            new Concesionario { IdConcesionario = 3, NombreComercial = "Andes Post", RazonSocial = "Servicios Postales Andes S.R.L.", Ruc = "20678912345", Direccion = "Jr. Cusco 321", Departamento = "Cusco", Provincia = "Cusco", Distrito = "Wanchaq", Telefono = "084567890", Email = "contacto@andespost.com" }
        );

        // 🔹 Usuario inicial (Admin)
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                IdUsuario = 1,
                Dni = 73613466,
                NombreUsuario = "Anderson Villegas Cruz",
                Correo = "admincsp@empresa.com",
                Password = "admin123",
                Rol = "CoordinadorCSP",
                Activo = true
            }
        );

        // 🔹 Fichas iniciales
        modelBuilder.Entity<FichaInspeccion>().HasData(
            new FichaInspeccion
            {
                IdInspeccion = 1,
                IdConcesionario = 1,
                Ruc = "20481234567",
                NombreComercial = "Correo Norte SAC",
                RazonSocial = "Correo Norte S.A.C.",
                Direccion = "Av. Central 123",
                Departamento = "Lambayeque",
                Provincia = "Chiclayo",
                Telefono = "074-123456",
                Email = "norte@correo.pe",
                AreaSupervisada = "Almacén Principal",
                FechaProgramada = new DateOnly(2025, 5, 10),
                MotivoInspeccion = "Supervisión rutinaria del local"
            }
        );
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
