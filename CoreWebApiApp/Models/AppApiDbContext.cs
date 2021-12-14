using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiApp.Models
{
    public class AppApiDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppApiDbContext(DbContextOptions<AppApiDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().
                HasOne(c => c.Category).
                WithMany(x => x.Products).
                HasForeignKey(f=>f.CategoryRowId);
        }
    }
}
