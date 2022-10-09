namespace CalorieCalculator.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public Menu Menu { get; set; }
        public int MenuId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public MeasureType MeasureType { get; set; }
        public int MeasureTypeId { get; set; }
        public double Quantity { get; set; }
        public double Energy { get; set; }
        public double Carbs { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Fiber { get; set; }
    }
}