using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieCalculator.Dtos
{
    [JsonObject(MemberSerialization.OptOut)]
    public class EdamamNutrientsReqDto
    {
        [JsonProperty("ingredients")]
        public List<Ingredient> Ingredients { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class Ingredient
    {
        [JsonProperty("quantity")]
        public double Quantity { get; set; }
        [JsonProperty("measureURI")]
        public string MeasureURI { get; set; }
        [JsonProperty("foodId")]
        public string FoodId { get; set; }
    }
}