using NevikaApp.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NevikaApp.API
{
    public static class ProductProcessor
    {
        //original api implementation. this is great, but i dont have time to set up a new website, so ill use a local database instead
        public static async Task<Product> RequestProductInfo(string ean_code = "")
        {
            // Attempt to get something from the database
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(ean_code))
            {
                if (response.IsSuccessStatusCode)
                {
                    // Works as long as the properties match the database
                    // TODO: more
                    Product myProduct = JsonConvert.DeserializeObject<Product>(
                        await response.Content.ReadAsStringAsync());

                    return myProduct;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
