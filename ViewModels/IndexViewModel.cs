using CalorieCalculator.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieCalculator.ViewModels
{
    public class IndexViewModel
    {
        public MenuDto NewMenu { get; set; }
        public IEnumerable<MenuDto> Menus { get; set; }
    }
}