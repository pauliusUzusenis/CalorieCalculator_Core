using CalorieCalculator.Models;

namespace CalorieCalculatorCore.Repository
{
    public interface IMenuRepository
    {
        IEnumerable<Menu> GetAll(string userId);
        Menu GetById(int menuID);
        void Insert(Menu menu);
        void Update(Menu menu);
        void Delete(int menuID);
        void Save();
    }
}
