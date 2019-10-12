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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
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
		}
	}
}
