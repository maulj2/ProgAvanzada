using System;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

public partial class _dbContext : DbContext
{
    public _dbContext()
    {
    }

    public _dbContext(DbContextOptions<_dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
     optionsBuilder.UseSqlServer("Server=(local)\\SQLEXPRESS01; DataBase=TIENDAVIDEOJUEGOS; Trusted_Connection=True; TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuario__5B65BF9794F6A7D1");

            entity.ToTable("usuario");

            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Productos__5B65BF9794F6A7D1");

            entity.ToTable("Productos");

            entity.Property(e => e.NombreProducto)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            entity.Property(e => e.FechaPublicacion)
                .HasColumnType("date")
                .IsRequired();

            entity.Property(e => e.DisponibilidadInventario)
                .IsRequired();

            entity.Property(e => e.VideoJuegoURL)
                .HasMaxLength(255);

            entity.Property(e => e.EstadoProducto)
                .HasMaxLength(50)
                .IsRequired();
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
