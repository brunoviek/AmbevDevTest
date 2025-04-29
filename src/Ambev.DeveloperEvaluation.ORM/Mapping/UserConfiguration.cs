using Ambev.DeveloperEvaluation.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "auth");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).HasMaxLength(20);

        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.Firstname)
                .HasMaxLength(100)
                .HasColumnName("Firstname");

            name.Property(n => n.Lastname)
                .HasMaxLength(100)
                .HasColumnName("Lastname");
        });

        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.Street)
                .HasMaxLength(150)
                .HasColumnName("Street");

            address.Property(a => a.Number)
                .HasColumnName("Number");

            address.Property(a => a.City)
                .HasMaxLength(100)
                .HasColumnName("City");

            address.Property(a => a.Zipcode)
                .HasMaxLength(20)
                .HasColumnName("Zipcode");

            address.OwnsOne(a => a.Geolocation, geo =>
            {
                geo.Property(g => g.Latitude)
                    .HasMaxLength(20)
                    .HasColumnName("Latitude");

                geo.Property(g => g.Longitude)
                    .HasMaxLength(20)
                    .HasColumnName("Longitude");
            });
        });

    }
}
