using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category)
                                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                                  .Where(p => p.CategoryId == categoryId)
                                  .Include(p => p.Category)
                                  .ToListAsync();
        }

        public async Task<decimal> GetTotalPriceByCategoryAsync(int categoryId)
        {
            return await _context.Products
                                  .Where(p => p.CategoryId == categoryId)
                                  .SumAsync(p => p.Price);
        }
    }
}
