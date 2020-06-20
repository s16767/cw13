using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw13.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<OrderConfectionery> OrderConfectioneries { get; set; }
        public DbSet<Confectionery> Confectioneries { get; set; }

        public MyContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne<Client>(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.IdClient);

            modelBuilder.Entity<Order>()
                .HasOne<Employee>(o => o.Employee)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.IdEmployee);

            modelBuilder.Entity<OrderConfectionery>()
                .HasKey(x => new { x.IdConfectionery, x.IdOrder });

            modelBuilder.Entity<OrderConfectionery>()
                .HasOne<Confectionery>(o => o.Confectionery)
                .WithMany(c => c.OrderConfectioneries)
                .HasForeignKey(o => o.IdConfectionery);

            modelBuilder.Entity<OrderConfectionery>()
                .HasOne<Order>(o => o.Order)
                .WithMany(o => o.OrderConfectioneries)
                .HasForeignKey(o => o.IdOrder);
        }
    }
}
