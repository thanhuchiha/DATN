using Microsoft.EntityFrameworkCore;
using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.DataAccess
{
    public class OrientNetCoreDbContext : DbContext
    {
        public OrientNetCoreDbContext(DbContextOptions<OrientNetCoreDbContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInRole> UserInRoles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductInCategory> ProductInCategories { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<ProductInShop> ProductInShops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1User -> nComment
            modelBuilder.Entity<Comment>()
                .HasOne(u => u.User).WithMany(u => u.Comments).IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(u => u.Product).WithMany(u => u.Comments).IsRequired().OnDelete(DeleteBehavior.Restrict);

            ////1Shop - nUser
            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.Shop).WithMany(u => u.Users).IsRequired().OnDelete(DeleteBehavior.Restrict);

            // User In Role
            modelBuilder.Entity<UserInRole>()
                .HasOne(u => u.User).WithMany(u => u.UserInRoles).IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserInRole>().HasKey(t => new { t.UserId, t.RoleId });

            modelBuilder.Entity<UserInRole>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserInRoles)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserInRole>()
                .HasOne(pt => pt.Role)
                .WithMany(p => p.UserInRoles)
                .HasForeignKey(pt => pt.RoleId);

            // Product In Category
            modelBuilder.Entity<ProductInCategory>()
                .HasOne(u => u.Product).WithMany(u => u.ProductInCategories).IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductInCategory>().HasKey(t => new { t.ProductId, t.CategoryId });

            modelBuilder.Entity<ProductInCategory>()
                .HasOne(pt => pt.Category)
                .WithMany(p => p.ProductInCategories)
                .HasForeignKey(pt => pt.CategoryId);

            modelBuilder.Entity<ProductInCategory>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductInCategories)
                .HasForeignKey(pt => pt.ProductId);

            // Product In Shop
            modelBuilder.Entity<ProductInShop>()
                .HasOne(u => u.Product).WithMany(u => u.ProductInShops).IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductInShop>().HasKey(t => new { t.ShopId, t.ProductId });

            modelBuilder.Entity<ProductInShop>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductInShops)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductInShop>()
                .HasOne(pt => pt.Shop)
                .WithMany(p => p.ProductInShops)
                .HasForeignKey(pt => pt.ShopId);
        }
    }
}
