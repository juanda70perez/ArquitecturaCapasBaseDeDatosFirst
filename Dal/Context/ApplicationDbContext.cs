using System;
using System.Collections.Generic;
using Dal;
using Microsoft.EntityFrameworkCore;

namespace Dal.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ciudad> Ciudades { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Pais> Paises { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => new { e.IdPais, e.IdDepartamento, e.IdCiudad }).HasName("PK__ciudades__ABB9484AC665FA30");

            entity.ToTable("ciudades");

            entity.Property(e => e.IdPais).HasColumnName("id_pais");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");
            entity.Property(e => e.CodigoCiudad)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_ciudad");
            entity.Property(e => e.NombreCiudad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_ciudad");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Ciudades)
                .HasForeignKey(d => new { d.IdPais, d.IdDepartamento })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ciudades__5629CD9C");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => new { e.IdPais, e.IdDepartamento }).HasName("PK__departam__7F0E9406030681C9");

            entity.ToTable("departamentos");

            entity.Property(e => e.IdPais).HasColumnName("id_pais");
            entity.Property(e => e.IdDepartamento)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_departamento");
            entity.Property(e => e.CodigoDepartamento)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_departamento");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_departamento");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__departame__id_pa__4CA06362");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__paises__3213E83F08BCAD58");

            entity.ToTable("paises");

            entity.HasIndex(e => e.CodigoPais, "UQ__paises__8B3210980C4517CE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodigoPais)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo_pais");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
