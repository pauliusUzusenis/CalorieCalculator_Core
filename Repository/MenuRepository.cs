using CalorieCalculator.Models;
using CalorieCalculatorCore.Data;
using Microsoft.EntityFrameworkCore;

namespace CalorieCalculatorCore.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;
        public MenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //TODO extract include logic to method parameters
        public IEnumerable<Menu> GetAll(string userId)
        {
            return _context.Menus
                            .Where(x => x.UserId == userId)
                            .Include(x => x.MenuItems)
                            .AsEnumerable();
        }
        //TODO extract include logic to method parameters
        public Menu GetById(int menuID)
        {
            return _context.Menus
                            .Include(m => m.MenuItems)
                            .ThenInclude(m => m.Product)
                            .SingleOrDefault(m => m.Id == menuID);
        }
        public void Insert(Menu menu)
        {
            _context.Menus.Add(menu);
        }
        public void Update(Menu menu)
        {
            _context.Entry(menu).State = EntityState.Modified;
        }
        public void Delete(int menuID)
        {
            Menu menu = _context.Menus.Find(menuID);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
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