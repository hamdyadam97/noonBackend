using AliExpress.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Context
{
    public class AliExpressContext: IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        //public DbSet<AppUser> AppUsers { get; set; }
        public AliExpressContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        //public AppContext(DbContextOptions<AppContext> options) : base(options)
        //{

        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<AppUser>()
             .HasOne(u => u.Cart)
             .WithOne(c => c.User)
             .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);





            base.OnModelCreating(modelBuilder);

            //Ayed 
            // Configure soft delete behavior for Product entity
            modelBuilder.Entity<Category>()
                .Property<bool>("IsDeleted");

            // Apply global query filter to exclude deleted products
            modelBuilder.Entity<Category>().HasQueryFilter(p => !EF.Property<bool>(p, "IsDeleted"));

            // Configure soft delete behavior for Product entity
            modelBuilder.Entity<Product>()
                .Property<bool>("IsDeleted");

            // Apply global query filter to exclude deleted products
            modelBuilder.Entity<Product>().HasQueryFilter(p => !EF.Property<bool>(p, "IsDeleted"));
        }

    }
}
