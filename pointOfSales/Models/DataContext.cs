using pointOfSales.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace point_of_sales.Models
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        //public DbSet<Report> Reports { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Re_typePassword)
                .HasColumnName("RetypePassword");

        }

        public System.Data.Entity.DbSet<pointOfSales.Models.ProductsSold> ProductsSolds { get; set; }
    }
}