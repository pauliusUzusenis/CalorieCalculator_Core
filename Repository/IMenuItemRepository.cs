using CalorieCalculator.Models;

namespace CalorieCalculatorCore.Repository
{
    public interface IMenuItemRepository
    {
        IEnumerable<MenuItem> GetAll();
        MenuItem GetById(int menuItemID);
        void Insert(MenuItem menuItem);
        void Update(MenuItem menuItem);
        void Delete(int menuItemID);
        void Delete(MenuItem menuItem);
        void Save();
    }
}
