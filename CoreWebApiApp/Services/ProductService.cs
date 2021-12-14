using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiApp.Services
{
    public class ProductService:IService<Product,int>
    {
        private readonly AppApiDbContext _context;

        public ProductService(AppApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            var prd = await _context.Products.ToListAsync();
            return prd;
        }

        public async Task<Product> GetAsync(int id)
        {
            var prd = await _context.Products.FindAsync(id);
            return prd;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            var res  = await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Product> UpdateAsync(int id, Product entity)
        {
            var prd = await _context.Products.FindAsync(id);
            if (prd != null)
            {
                prd.ProductId = entity.ProductId;
                prd.ProductName = entity.ProductName;
                prd.ProductName = entity.ProductName;
                prd.Manufacturer = entity.Manufacturer;
                prd.Price = entity.Price;
                prd.CategoryRowId = entity.CategoryRowId;
          
                await _context.SaveChangesAsync();
                return prd;
            }

            return prd;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // search record based on Pk
            var prd = await _context.Products.FindAsync(id);
            if (prd == null)
            {
                return false;
            }

            _context.Products.Remove(prd);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
