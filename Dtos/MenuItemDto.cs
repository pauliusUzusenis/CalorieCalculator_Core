using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalorieCalculator.Dtos
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public MeasureTypeDto? MeasureType { get; set; }
        public int MeasureTypeId { get; set; }
        public MenuDto? Menu { get; set; }
        public int MenuId { get; set; }
        public ProductDto? Product { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double Energy { get; set; }
        public double Carbs { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Fiber { get; set; }
    }
}