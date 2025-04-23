using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieCalculator.Dtos
{
    [JsonObject(MemberSerialization.OptOut)]
    public class EdamamNutrientsResponseDto
    {
        [JsonProperty("totalNutrients")]
        public TotalNutrients TotalNutrients { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class TotalNutrients
    {
        [JsonProperty("ENERC_KCAL")]
        public Energy Energy { get; set; }
        [JsonProperty("FAT")]
        public Fat Fat { get; set; }
        [JsonProperty("PROCNT")]
        public Protein Protein { get; set; }
        [JsonProperty("CHOCDF")]
        public Carbs Carbs { get; set; }
        [JsonProperty("FIBTG")]
        public Fiber Fiber { get; set; }
    }

    public class Energy
    {
        private double quantity;
        [JsonProperty("quantity")]
        public double Quantity {
            get { return quantity; }
            set { quantity = Math.Round(value, 2); } 
        }
    }
    public class Protein
    {
        private double quantity;
        [JsonProperty("quantity")]
        public double Quantity
        {
            get { return quantity; }
            set { quantity = Math.Round(value, 2); }
        }
    }
    public class Fat
    {
        private double quantity;
        [JsonProperty("quantity")]
        public double Quantity
        {
            get { return quantity; }
            set { quantity = Math.Round(value, 2); }
        }
    }
    public class Carbs
    {
        private double quantity;
        [JsonProperty("quantity")]
        public double Quantity
        {
            get { return quantity; }
            set { quantity = Math.Round(value, 2); }
        }
    }
    public class Fiber
    {
        private double quantity;
        [JsonProperty("quantity")]
        public double Quantity
        {
            get { return quantity; }
            set { quantity = Math.Round(value, 2); }
        }
    }
}