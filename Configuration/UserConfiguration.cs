
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZestWeb.Models;

namespace ZestWeb.Configuration
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(c => c.Phone).HasColumnType("nvarchar(20)").IsRequired();
            builder.Property(c => c.Password).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
            builder.Property(c => c.Address).HasColumnType("nvarchar(200)").IsRequired();
            builder.HasOne(c => c.Role).WithMany().HasForeignKey(c => c.IdRole);
        }
    }
}
