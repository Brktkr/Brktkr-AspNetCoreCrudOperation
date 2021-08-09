using Microsoft.EntityFrameworkCore;
using MvcCoreProductOperationApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProductOperationApplication.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .HasOne<ProductCategory>(s => s.Category)
            .WithMany(g => g.Products)
            .HasForeignKey(s => s.CategoryId);
        }
    }
}
