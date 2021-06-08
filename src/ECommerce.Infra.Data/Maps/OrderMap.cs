using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infra.Data.Maps
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnName("Id");

            builder.Property(x => x.ProductCode)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
    public class CampaignMap : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnName("Id");

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ProductCode)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PriceManipulationLimit).IsRequired();
            builder.Property(x => x.TargetSalesCount).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
