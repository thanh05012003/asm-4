
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZestWeb.Models;


namespace ZestWeb.Configuration
{
    public class BillDetailsConfiguration:IEntityTypeConfiguration<BillDetails>
    {
        public void Configure(EntityTypeBuilder<BillDetails> builder)
        {
            builder.HasKey(c =>new {c.IdBill,c.IdProductDetails});
            builder.Property(c => c.Quantity).HasColumnType("int").IsRequired();
            builder.Property(c => c.Price).HasColumnType("int").IsRequired();
            builder.HasOne(c => c.ProductsdDetails).WithMany().HasForeignKey(c => c.IdProductDetails);
            builder.HasOne(c => c.Bill).WithMany().HasForeignKey(c => c.IdBill);
        }
    }
}
