
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZestWeb.Models;

namespace ZestWeb.Configuration
{
    public class RoleConfiguration:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(c => c.Description).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
        }
    }
}
