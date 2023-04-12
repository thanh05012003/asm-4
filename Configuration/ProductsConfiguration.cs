
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZestWeb.Models;

namespace ZestWeb.Configuration
{
    public class ProductsConfiguration:IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(c => c.Description).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
            builder.Property(c => c.Supplier).HasColumnType("nvarchar(200)").IsRequired();
            builder.HasOne(c => c.TypeProduct).WithMany().HasForeignKey(c => c.IdTypeProduct);
        }
    }
}
