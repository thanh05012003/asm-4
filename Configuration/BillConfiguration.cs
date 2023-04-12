
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZestWeb.Models;


namespace ZestWeb.Configuration
{
    public class BillConfiguration:IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CreateDate).HasColumnType("DateTime").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
            builder.HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId);
        }
    }
}
