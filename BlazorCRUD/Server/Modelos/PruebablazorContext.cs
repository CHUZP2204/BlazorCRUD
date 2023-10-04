using System;
using System.Collections.Generic;
using BlazorCRUD.Server.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BlazorCRUD.Server.Modelos;

public partial class PruebablazorContext : DbContext
{
    public PruebablazorContext()
    {
    }

    public PruebablazorContext(DbContextOptions<PruebablazorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<TblVehiculo> TblVehiculos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-MQE65RA; Initial Catalog=Pruebablazor; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; multiSubnetFailOver=False");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.CantidadProd).HasColumnName("Cantidad_Prod");
            entity.Property(e => e.NombreProd)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Nombre_Prod");
            entity.Property(e => e.PrecioProd).HasColumnName("Precio_Prod");
        });

        modelBuilder.Entity<TblVehiculo>(entity =>
        {
            entity.HasKey(e => e.IdVehiculo);

            entity.Property(e => e.IdVehiculo).HasColumnName("ID_Vehiculo");
            entity.Property(e => e.ModeloVehiculo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Modelo_Vehiculo");
            entity.Property(e => e.PlacaVehiculo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Placa_Vehiculo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("USUARIOS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaAlta).HasColumnType("datetime");
            entity.Property(e => e.FechaBaja).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
