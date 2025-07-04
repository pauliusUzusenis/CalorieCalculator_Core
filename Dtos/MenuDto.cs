﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalorieCalculator.Dtos
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<MenuItemDto>? MenuItems { get; set; }
    }
}