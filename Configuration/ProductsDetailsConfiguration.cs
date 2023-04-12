
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZestWeb.Models;

namespace ZestWeb.Configuration
{
    public class ProductsDetailsConfiguration:IEntityTypeConfiguration<ProductDetails>
    {
        public void Configure(EntityTypeBuilder<ProductDetails> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Price).HasColumnType("int").IsRequired();
            builder.Property(c => c.AvailableQuantity).HasColumnType("int").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
            builder.Property(c => c.ImageUrl).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(c => c.Color).HasColumnType("nvarchar(200)").IsRequired();
            builder.HasOne(c => c.Products).WithMany().HasForeignKey(c => c.IdProduct);
            
        }
    }
}
