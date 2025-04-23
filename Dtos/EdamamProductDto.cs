using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieCalculator.Dtos
{
    [JsonObject(MemberSerialization.OptOut)]
    public class EdamamProductDto
    {
        [JsonProperty("parsed")]
        public List<Parsed> Parsed { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class Parsed
    {
        [JsonProperty("food")]
        public Food Food { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class Food
    {
        [JsonProperty("foodId")]
        public string FoodId { get; set; }
        [JsonProperty("label")]
        public string Name { get; set; }
        [JsonProperty("nutrients")]
        public Nutrients Nutrients { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class Nutrients
    {
        [JsonProperty("ENERC_KCAL")]
        public double Energy { get; set; }
        [JsonProperty("PROCNT")]
        public double Protein { get; set; }
        [JsonProperty("FAT")]
        public double Fat { get; set; }
        [JsonProperty("CHOCDF")]
        public double Carbs { get; set; }
        [JsonProperty("FIBTG")]
        public double Fiber { get; set; }
    }
}