
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZestWeb.Models;


namespace ZestWeb.Configuration
{
    public class CartDetailsConfiguration:IEntityTypeConfiguration<CartDetails>
    {
        public void Configure(EntityTypeBuilder<CartDetails> builder)
        {
            builder.HasKey(c => new {c.IdCart,c.IdProductDetails});
            builder.Property(c => c.Quantity).HasColumnType("int").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
            builder.HasOne(c => c.ProductsdDetails).WithMany().HasForeignKey(c => c.IdProductDetails);
            builder.HasOne(c => c.Cart).WithMany().HasForeignKey(c => c.IdCart);
        }
    }
}
