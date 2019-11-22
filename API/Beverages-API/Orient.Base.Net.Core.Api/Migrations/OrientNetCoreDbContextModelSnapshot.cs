﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orient.Base.Net.Core.Api.Core.DataAccess;

namespace Orient.Base.Net.Core.Api.Migrations
{
    [DbContext(typeof(OrientNetCoreDbContext))]
    partial class OrientNetCoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid?>("DeletedBy");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("Quantity_Product");

                    b.Property<bool>("RecordActive");

                    b.Property<bool>("RecordDeleted");

                    b.Property<int>("RecordOrder");

                    b.Property<string>("Total")
                        .HasMaxLength(255);

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid?>("DeletedBy");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("RecordActive");

                    b.Property<bool>("RecordDeleted");

                    b.Property<int>("RecordOrder");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasMaxLength(255);

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid?>("DeletedBy");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid?>("ProductId")
                        .IsRequired();

                    b.Property<bool>("RecordActive");

                    b.Property<bool>("RecordDeleted");

                    b.Property<int>("RecordOrder");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<Guid?>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CartId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid?>("DeletedBy");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<string>("Image")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.Property<string>("Price")
                        .HasMaxLength(255);

                    b.Property<int>("ProductStatus");

                    b.Property<bool>("RecordActive");

                    b.Property<bool>("RecordDeleted");

                    b.Property<int>("RecordOrder");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.ProductInCategory", b =>
                {
                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("CategoryId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid?>("DeletedBy");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid>("Id");

                    b.Property<bool>("RecordActive");

                    b.Property<bool>("RecordDeleted");

                    b.Property<int>("RecordOrder");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductInCategory");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.ProductInShop", b =>
                {
                    b.Property<Guid>("ShopId");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid?>("DeletedBy");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid>("Id");

                    b.Property<bool>("RecordActive");

                    b.Property<bool>("RecordDeleted");

                    b.Property<int>("RecordOrder");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ShopId", "ProductId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInShop");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid?>("DeletedBy");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<bool>("RecordActive");

                    b.Property<bool>("RecordDeleted");

                    b.Property<int>("RecordOrder");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.Shop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("AvatarUrl");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid?>("DeletedBy");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description");

                    b.Property<string>("FeeShip");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<bool>("RecordActive");

                    b.Property<bool>("RecordDeleted");

                    b.Property<int>("RecordOrder");

                    b.Property<int>("Status");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Shop");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<string>("Address")
                        .HasMaxLength(1024);

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(512);

                    b.Property<string>("Color");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<Guid?>("DeletedBy");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<string>("Facebook")
                        .HasMaxLength(512);

                    b.Property<int>("Gender");

                    b.Property<string>("Google")
                        .HasMaxLength(512);

                    b.Property<string>("LinkedIn")
                        .HasMaxLength(512);

                    b.Property<string>("Mobile")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<bool>("RecordActive");

                    b.Property<bool>("RecordDeleted");

                    b.Property<int>("RecordOrder");

                    b.Property<string>("ResetPasswordCode");

                    b.Property<DateTime?>("ResetPasswordExpiryDate");

                    b.Property<Guid?>("ShopId");

                    b.Property<string>("Twitter")
                        .HasMaxLength(512);

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.UserInRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid?>("DeletedBy");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid>("Id");

                    b.Property<bool>("RecordActive");

                    b.Property<bool>("RecordDeleted");

                    b.Property<int>("RecordOrder");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("UserId", "RoleId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("UserInRole");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.Comment", b =>
                {
                    b.HasOne("Orient.Base.Net.Core.Api.Core.Entities.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Orient.Base.Net.Core.Api.Core.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.Product", b =>
                {
                    b.HasOne("Orient.Base.Net.Core.Api.Core.Entities.Cart")
                        .WithMany("Products")
                        .HasForeignKey("CartId");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.ProductInCategory", b =>
                {
                    b.HasOne("Orient.Base.Net.Core.Api.Core.Entities.Category", "Category")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Orient.Base.Net.Core.Api.Core.Entities.Product", "Product")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.ProductInShop", b =>
                {
                    b.HasOne("Orient.Base.Net.Core.Api.Core.Entities.Product", "Product")
                        .WithMany("ProductInShops")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Orient.Base.Net.Core.Api.Core.Entities.Shop", "Shop")
                        .WithMany("ProductInShops")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.User", b =>
                {
                    b.HasOne("Orient.Base.Net.Core.Api.Core.Entities.Shop")
                        .WithMany("Users")
                        .HasForeignKey("ShopId");
                });

            modelBuilder.Entity("Orient.Base.Net.Core.Api.Core.Entities.UserInRole", b =>
                {
                    b.HasOne("Orient.Base.Net.Core.Api.Core.Entities.Role", "Role")
                        .WithMany("UserInRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Orient.Base.Net.Core.Api.Core.Entities.User", "User")
                        .WithMany("UserInRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
