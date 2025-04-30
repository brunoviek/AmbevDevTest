using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    /// <summary>
    /// Configures the <see cref="Product"/> entity mapping to the database.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Category)
                .HasMaxLength(100);

            builder.Property(p => p.Image)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.OwnsOne(p => p.Rating, r =>
            {
                r.Property(r => r.Rate)
                    .HasColumnName("Rate")
                    .HasColumnType("decimal(5,2)");

                r.Property(r => r.Count)
                    .HasColumnName("Count");
            });
        }
    }
}
