using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieCalculator.Dtos
{
    public class MeasureTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Symbol { get; set; }
    }
}