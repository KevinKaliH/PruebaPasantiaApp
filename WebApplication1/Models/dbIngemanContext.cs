using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication1.Models
{
    public partial class dbIngemanContext : DbContext
    {
        public dbIngemanContext()
        {
        }

        public dbIngemanContext(DbContextOptions<dbIngemanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.ToTable("articulos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("codigo")
                    .IsFixedLength(true);

                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(9, 4)")
                    .HasColumnName("costo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(125)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(9, 4)")
                    .HasColumnName("precio");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.ToTable("detalleFactura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");

                entity.Property(e => e.Impuesto)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("impuesto");

                entity.Property(e => e.NumeroFact).HasColumnName("numero_fact");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(9, 4)")
                    .HasColumnName("precio");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("decimal(12, 4)")
                    .HasColumnName("subtotal");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(12, 4)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__detalleFa__id_ar__4D94879B");

                entity.HasOne(d => d.NumeroFactNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.NumeroFact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__detalleFa__total__4CA06362");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.NumeroFact)
                    .HasName("PK__facturas__7DC27F2086A52C20");

                entity.ToTable("facturas");

                entity.Property(e => e.NumeroFact).HasColumnName("numero_fact");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("date")
                    .HasColumnName("fecha_registro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Impuesto)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("impuesto");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("decimal(12, 4)")
                    .HasColumnName("subtotal");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(12, 4)")
                    .HasColumnName("total");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
