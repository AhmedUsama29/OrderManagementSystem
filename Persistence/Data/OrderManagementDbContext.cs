using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>().HasMany(c => c.Orders)
                                    .WithOne(x => x.Customer)
                                    .HasForeignKey(x => x.CustomerId)
                                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>().HasMany(o => o.OrderItems)
                                        .WithOne(x => x.Order)
                                        .HasForeignKey(x => x.OrderId)
                                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>().HasOne(oi => oi.Product)
                                            .WithMany()
                                            .HasForeignKey(x => x.ProductId)
                                            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Order)
                .WithOne()
                .HasForeignKey<Invoice>(i => i.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoice>().Property(x => x.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(x => x.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().Property(x => x.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().Property(x => x.Discount).HasPrecision(18, 2);
            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);

            modelBuilder.Entity<Order>().Property(o => o.PaymentMethod)
                .HasConversion<string>()
                .HasMaxLength(15);

            modelBuilder.Entity<Order>().Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(10);

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
