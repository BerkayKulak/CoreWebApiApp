using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiApp.Services
{
    public class CategoryService:IService<Category,int>
    {
        private readonly AppApiDbContext _context;

        public CategoryService(AppApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            var cats = await _context.Categories.ToListAsync();
            return cats;
        }

        public async Task<Category> GetAsync(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            return cat;
        }

        public async Task<Category> CreateAsync(Category entity)
        {
            var res  = await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat != null)
            {
                cat.CategoryId = entity.CategoryId;
                cat.CategoryName = entity.CategoryName;
                cat.BasePrice = entity.BasePrice;
                await _context.SaveChangesAsync();
                return cat;
            }

            return cat;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // search record based on Pk
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return false;
            }

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
