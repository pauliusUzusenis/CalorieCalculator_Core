﻿using System.ComponentModel.DataAnnotations;

namespace CalorieCalculator.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FoodId { get; set; }
        public double Energy { get; set; }
        public double Carbs { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Fiber { get; set; }
    }
}