using CalorieCalculator.Dtos;
using CalorieCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieCalculator.ViewModels
{
    public class DetailsViewModel
    {
        public MenuItemDto MenuItem { get; set; }
        public IEnumerable<MeasureTypeDto> MeasureTypes { get; set; }
        public MenuDto Menu { get; set; }
    }
}