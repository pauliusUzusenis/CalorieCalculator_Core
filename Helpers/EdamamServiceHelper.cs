using CalorieCalculator.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CalorieCalculator.Helpers
{
    public static class EdamamServiceHelper
    {
        private const string baseUri = "https://api.edamam.com";
        private const string appId = "877a16fa";
        private const string appKey = "401a4cdcc4c369c2f74a92289d629d57";
        public static async Task<EdamamProductDto> GetProduct(string productName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(String.Format("/api/food-database/v2/parser?app_id={0}&app_key={1}&ingr={2}", appId, appKey, productName));
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();
                    EdamamProductDto result = JsonConvert.DeserializeObject<EdamamProductDto>(responseString);
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public static async Task<EdamamNutrientsResponseDto> GetProductNutrients(double quantity, string measureUri, string foodId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var obj = new EdamamNutrientsRequestDto
                    {
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient
                            {
                                Quantity = quantity,
                                MeasureURI = measureUri,
                                FoodId = foodId
                            }
                         }
                    };
                    var dataJson = JsonConvert.SerializeObject(obj);
                    var data = new StringContent(dataJson, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(String.Format("/api/food-database/v2/nutrients?app_id={0}&app_key={1}", appId, appKey), data);
                    response.EnsureSuccessStatusCode();
                    var resultString = await response.Content.ReadAsStringAsync();

                    EdamamNutrientsResponseDto result = JsonConvert.DeserializeObject<EdamamNutrientsResponseDto>(resultString);
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}