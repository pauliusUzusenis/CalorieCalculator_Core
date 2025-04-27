using CalorieCalculator.Models;

namespace CalorieCalculatorCore.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int productID);
        Product GetByFoodId(string foodID);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int productID);
        void Save();
    }
}
