using CalorieCalculator.Models;
using CalorieCalculatorCore.Data;
using Microsoft.EntityFrameworkCore;

namespace CalorieCalculatorCore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.AsEnumerable();
        }
        public Product GetById(int productID)
        {
            return _context.Products.SingleOrDefault(m => m.Id == productID);
        }
        public Product GetByFoodId(string foodID)
        {
            return _context.Products.SingleOrDefault(m => m.FoodId == foodID);
        }
        public void Insert(Product product)
        {
            _context.Products.Add(product);
        }
        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }
        public void Delete(int productID)
        {
            Product product = _context.Products.Find(productID);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}