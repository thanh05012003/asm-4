using System.Reflection;

using Microsoft.EntityFrameworkCore;
using ZestWeb.Models;


namespace ZestWeb.Context
{
    public class AsmDbContext:Microsoft.EntityFrameworkCore.DbContext
    {
        public AsmDbContext()
        {
            
        }

        public AsmDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<TypeProduct> TypeProducts { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=MSI;Initial Catalog=Thanhnxph20424_CSharp4_ASM;Persist Security Info=True;User ID=thanhnxph20424;Password=05012003");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
