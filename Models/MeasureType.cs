using System.ComponentModel.DataAnnotations;

namespace CalorieCalculator.Models
{
    public class MeasureType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
    }
}