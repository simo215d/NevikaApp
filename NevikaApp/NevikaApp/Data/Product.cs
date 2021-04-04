using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NevikaApp.Data
{
    public class Product
    {
        /// <summary>
        /// The name of the product
        /// </summary>
        public string Product_Name { get; set; }
        /// <summary>
        /// The name of the brand producing the product
        /// </summary>
        public string Product_Brand { get; set; }
        /// <summary>
        /// The full, unformatted ingredients text, as found on the product packaging
        /// </summary>
        public string Product_Ingredients_Text { get; set; }
        /// <summary>
        /// To be changed when database image implementation is complete
        /// </summary>
        public string Product_Image { get; set; }
        /// <summary>
        /// The EAN code (barcode) of the product
        /// </summary>
        public string Product_EAN_Code { get; set; }

        /// <summary>
        /// Temporarly unused
        /// </summary>
        public List<string> IngredientsList;
    }
}
