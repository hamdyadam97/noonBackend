using AliExpress.Models;
using AliExpress.Models.Orders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Context
{
    public class AliExpressContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        //public DbSet<AppUser> AppUsers { get; set; }
        public AliExpressContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        //public AppContext(DbContextOptions<AppContext> options) : base(options)
        //{

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });
            //cartItem
            //modelBuilder.Entity<CartItem>()
            //    .HasKey(ci =>new {ci.ProductId,ci.CartId});
            //cartItem-cart
            modelBuilder.Entity<CartItem>()
              .HasOne(ci => ci.Cart)
              .WithMany(c => c.CartItems)
              .HasForeignKey(ci => ci.CartId);

            //cartitem-product
            modelBuilder.Entity<CartItem>()
              .HasOne(ci => ci.Product)
              .WithMany(p => p.CartItems)
              .HasForeignKey(ci => ci.ProductId);


            //user-cart
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.AppUser)
                .HasForeignKey<Cart>(C => C.AppUserId);

            modelBuilder.Entity<Cart>()
       .HasKey(c => c.CartId);

            modelBuilder.Entity<Cart>()
       .HasMany(c => c.CartItems)
       .WithOne(ci => ci.Cart)
       .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.AppUser)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.AppUserId);

            // one-to-many , AppUser and Order
            modelBuilder.Entity<Order>()
          .HasOne(o => o.AppUser)
          .WithMany(u => u.Orders)
         .HasForeignKey(o => o.AppUserId);

            // one-to-many DeliveryMethod and Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId);

            // one-to-many relationship Order and OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            // one-to-many  Product and OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);



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