using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NevikaApp.Data
{
    public static class LocalDatabase
    {
        public static string LOCAL_DB_PATH { get; set; }
        public static List<Allergen> AllergensList;

        public static void InitializeLocalDatabase(string db_path)
        {
            LOCAL_DB_PATH = db_path;

            // Temporary, because the allergens are not on the database yet. The idea is to keep
            // a local database with the allergens instead of having to query everytime we open the app,
            // and check every now and then that it's synchronized
            InsertTempAllergens();
            PopulateAllergenList();
            //Products now also in the local database because i dont wanna setup an api on my site
            FillDatabaseWithProducts();
            CreateFavoritesTable();
        }

        public static void InsertTempAllergens()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LOCAL_DB_PATH))
            {
                //dont refill it if it already exists
                if (conn.GetTableInfo("Allergen").Count > 0)
                {
                    //conn.DropTable<Allergen>();
                    //return;
                }
                if (conn.GetTableInfo("FavoritedProduct").Count > 0)
                {
                    return;
                }
                conn.DropTable<Allergen>();
                conn.CreateTable<Allergen>();
                List<Allergen> tempList = new List<Allergen>();
                tempList.Add(new Allergen()
                {
                    DanishName = "Gluten",
                    EnglishName = "Gluten",
                    IsCategory = true,
                    Category = "Gluten",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Æg (protein)",
                    EnglishName = "Egg (protein)",
                    IsCategory = true,
                    Category = "Æg",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Selleri",
                    IsCategory = true,
                    Category = "Selleri",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Sukker (arter)",
                    IsCategory = true,
                    Category = "Sukker (arter)",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Soja",
                    IsCategory = true,
                    Category = "Soja",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Svovldioxid",
                    IsCategory = true,
                    Category = "Svovldioxid",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Sennep",
                    IsCategory = true,
                    Category = "Sennep",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Sennepskorn",
                    IsCategory = false,
                    Category = "Sennep",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Krebsdyr",
                    IsCategory = true,
                    Category = "Krebsdyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Rejer",
                    IsCategory = false,
                    Category = "Krebsdyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Hummer",
                    IsCategory = false,
                    Category = "Krebsdyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Jomfruhummer",
                    IsCategory = false,
                    Category = "Krebsdyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Krabbe",
                    IsCategory = false,
                    Category = "Krebsdyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Krebs",
                    IsCategory = false,
                    Category = "Krebsdyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "fiskesauce",
                    IsCategory = false,
                    Category = "Krebsdyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Mejeriprodukter",
                    IsCategory = true,
                    Category = "Mejeriprodukter",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Mælk",
                    IsCategory = false,
                    Category = "Mejeriprodukter",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Smør",
                    IsCategory = false,
                    Category = "Mejeriprodukter",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Syrnede mejeriprodukter (yoghurt, skyr osv.)",
                    IsCategory = false,
                    Category = "Mejeriprodukter",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Fløde",
                    IsCategory = false,
                    Category = "Mejeriprodukter",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Ost",
                    IsCategory = false,
                    Category = "Mejeriprodukter",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Fisk",
                    IsCategory = true,
                    Category = "Fisk",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Tun",
                    IsCategory = false,
                    Category = "Fisk",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Andre fiskearter",
                    IsCategory = false,
                    Category = "Fisk",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Fiskesauce",
                    IsCategory = false,
                    Category = "Fisk",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Skaldyr",
                    IsCategory = true,
                    Category = "Skaldyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Blåmuslinger",
                    IsCategory = false,
                    Category = "Skaldyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Knivmuslinger",
                    IsCategory = false,
                    Category = "Skaldyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Kammuslinger",
                    IsCategory = false,
                    Category = "Skaldyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Hjertemuslinger",
                    IsCategory = false,
                    Category = "Skaldyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Østers",
                    IsCategory = false,
                    Category = "Skaldyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Blæksprutte",
                    IsCategory = false,
                    Category = "Skaldyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Snegle",
                    IsCategory = false,
                    Category = "Skaldyr",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Frø/kerner",
                    IsCategory = true,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Sesamfrø",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Solsikkekerner",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Birkes",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Græskarkerner",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Hørfrø",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Chiafrø",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Quinoa",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Nigellafrø",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Lucernefrø",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Boghvede",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Hampefrø",
                    IsCategory = false,
                    Category = "Frø/kerner",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Nødder",
                    IsCategory = true,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Peanuts",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Hasselnødder",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Valnødder",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Pinjekerner",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Mandler",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Cashewnødder",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Pekannødder",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Macadamianødder",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Paranødder",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Pistacienødder",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Kastanje",
                    IsCategory = false,
                    Category = "Nødder",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Svampe",
                    IsCategory = true,
                    Category = "Svampe",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Kantarel",
                    IsCategory = false,
                    Category = "Svampe",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Stor trompetsvamp",
                    IsCategory = false,
                    Category = "Svampe",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Morkler",
                    IsCategory = false,
                    Category = "Svampe",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Champignon",
                    IsCategory = false,
                    Category = "Svampe",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Markchampignon",
                    IsCategory = false,
                    Category = "Svampe",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Lille blødchampignon",
                    IsCategory = false,
                    Category = "Svampe",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Karl Johan",
                    IsCategory = false,
                    Category = "Svampe",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Brunstokket rørhat",
                    IsCategory = false,
                    Category = "Svampe",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Frugt/bær",
                    IsCategory = true,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Jordbær",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Banan",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Æble",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Honningmelon",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Vandmelon",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Cantaloup",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Galiamelon",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Charentais melon",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Appelsin",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Ananas",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Pære",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Kiwi",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Mango",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Tomat",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Abrikos",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Vindruer",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Kokosnødder",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Chili",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Peberfrugt",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Blåbær",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Hindbær",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Skovbær",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Solbær",
                    IsCategory = false,
                    Category = "Frugt/bær",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Grøntsager",
                    IsCategory = true,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Ærter",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Gulerødder",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Kartofler",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Jordskok",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Blomkål",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Broccoli",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Spinat",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Pastinak",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Persillerod",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Rødbede",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Gulbede",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Bolsjebede",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Zittauerløg",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Rødløg",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Hvidløg",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Perleløg",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Skallotteløg",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Bananskallotteløg",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Forårsløg",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Hvid asparges",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Grøn asparges",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Agurk",
                    IsCategory = false,
                    Category = "Grøntsager",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Krydderi",
                    IsCategory = true,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Karry",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Gurkemeje",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Ingefær",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Spidskommen",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Kanel",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Kardemomme",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Nelliker",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Muskatnød",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Sort feber",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Laurbærblade",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                tempList.Add(new Allergen()
                {
                    DanishName = "Bukkehorn",
                    IsCategory = false,
                    Category = "Krydderi",
                    Selected = false
                });
                conn.InsertAll(tempList);
            }
        }

        public static void PopulateAllergenList()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LOCAL_DB_PATH))
            {
                //conn.CreateTable<Allergen>();
                AllergensList = conn.Table<Allergen>().ToList();
            }
        }

        private static void FillDatabaseWithProducts()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LOCAL_DB_PATH))
            {
                //dont refill it if it already exists
                if (conn.GetTableInfo("Product").Count > 0)
                {
                    //return;
                    conn.DropTable<Product>();
                }
                conn.CreateTable<Product>();
                List<Product> products = new List<Product>();
                products.Add(new Product()
                {
                    Product_Name = "Minimælk 1 liter",
                    Product_Brand = "Arla",
                    Product_Ingredients_Text = "Mælk 0,4% fedt.",
                    Product_Image = "Milk.jpg",
                    Product_EAN_Code = "5700426291383",
                });
                products.Add(new Product()
                {
                    Product_Name = "BODYLAB WEIGHT GAINER ULTIMATE CHOCOLATE",
                    Product_Brand = "Bodylab",
                    Product_Ingredients_Text = "Ultra- og mikrofiltreret valleprotein-koncentrat (40%, fra MÆLK), maltodextrin, dextrose, fedtreduceret kakaopulver (5%), emulgator (SOJA- og solsikkelecithin), aroma, konsistensmiddel (xanthangummi), sødestof (sukralose).",
                    Product_Image = "protienpulver.jpg",
                    Product_EAN_Code = "5711657015457",
                });
                products.Add(new Product()
                {
                    Product_Name = "Stryhn’s Fransk postej",
                    Product_Brand = "Stryhn’s",
                    Product_Ingredients_Text = "Griselever 34%, grisespæk, vand, hvedemel, skummetmælkspulver, løg, salt, ansjoser, sukker, krydderier, konserveringsmiddel: sorbinsyre, stabilisator E451, antioxidant: natriumascorbat.",
                    Product_Image = "franskpostej.jpg",
                    Product_EAN_Code = "5704000433480",
                });
                products.Add(new Product()
                {
                    Product_Name = "Kogt skinke - Rema 1000",
                    Product_Brand = "Rema 1000",
                    Product_Ingredients_Text = "87% skinkekød, 9% vand, salt, salterstatter (kaliumchlorid), dextrose, stabilisator (E 451), antioxidant (E 301), konserveringsmiddel (E 250).",
                    Product_Image = "kogtskinke.jpg",
                    Product_EAN_Code = "5705830604606",
                });
                products.Add(new Product()
                {
                    Product_Name = "Hamburgerryg - kogt. røget.",
                    Product_Brand = "First Price",
                    Product_Ingredients_Text = "97% grisekam, salt, stabilisator (E 451, E 450, E 262, E 327), dextrose, antioxidant (E 301), krydderiekstrakter, konserveringsmiddel (E 250). - Spor af: hvede, soja, mælk, selleri og sennep.",
                    Product_Image = "hamburgerryg.jpg",
                    Product_EAN_Code = "7311041080375",
                });
                products.Add(new Product()
                {
                    Product_Name = "Den klassiske røget medister",
                    Product_Brand = "3-stjernet",
                    Product_Ingredients_Text = "Grisekød 71%, vand, løg, soyaprotein, salt, surhedsregulerende midler (E 261, E 326), sukker, stabilisator (E 451), røgaroma, antioxidant (E 301, rosmarinekstrakt), krydderier og krydderiekstrakter, konserveringsmiddel (E 250).",
                    Product_Image = "rogetmedister.jpg",
                    Product_EAN_Code = "5704080955858",
                });
                products.Add(new Product()
                {
                    Product_Name = "Skinkeost, blød smelteost med skinke",
                    Product_Brand = "BUKO",
                    Product_Ingredients_Text = "Ost, vand, kogt skinke 7% (skinke, salt dextrose), smør, smeltesalt (E 339), vallepermeatpulver (mælk), skummetmælkspulver, surhedsregulerende middel (citronsyre). - Anvendt pasteuriseret mælk til fremstillingen af osten.",
                    Product_Image = "skinkeost.jpg",
                    Product_EAN_Code = "5760466304214",
                });
                products.Add(new Product()
                {
                    Product_Name = "Smør",
                    Product_Brand = "Kærgården",
                    Product_Ingredients_Text = "Smør, rapsolie, vand, mælkesyrekultur og salt",
                    Product_Image = "smor.jpg",
                    Product_EAN_Code = "5760466904919",
                });
                products.Add(new Product()
                {
                    Product_Name = "HP sauce, The original",
                    Product_Brand = "HP sauce",
                    Product_Ingredients_Text = "tomatpuré, bygmalteddike, melasse, glukosefruktosesirup, eddike, sukker, dadler, modificeret majsstivelse, rugmel, salt, krydderier, naturlig aroma, tamarind",
                    Product_Image = "hpsauce.jpg",
                    Product_EAN_Code = "5000111018005",
                });
                products.Add(new Product()
                {
                    Product_Name = "Økologisk Tomat ketchup",
                    Product_Brand = "Svansø",
                    Product_Ingredients_Text = "tomatpuré, rørsukker, eddike, vand, majsstivelse, havsalt, fortykningsmiddel (E440), krydderier",
                    Product_Image = "tomatkethup.jpg",
                    Product_EAN_Code = "5701116011403",
                });
                products.Add(new Product()
                {
                    Product_Name = "Økologisk mayonnaise",
                    Product_Brand = "Rema 1000",
                    Product_Ingredients_Text = "økologisk rapsolie, vand, økologisk rekonstitueret æggeblomme, økologisk eddike, økologisk rørsukker, havsalt, stabilisator, surhedsregulerende middel",
                    Product_Image = "okologiskmayo.jpg",
                    Product_EAN_Code = "5705830007711",
                });
                products.Add(new Product()
                {
                    Product_Name = "Sød fransk sennep",
                    Product_Brand = "Bähncke",
                    Product_Ingredients_Text = "Vand, eddike, glukose-fruktosesirup,sukker, hvedemel, 6% sennepsmel, salt, modificeret stivelse, krydderier, surhedsregulerende middel : (E330), konserveringsmiddel : (E202)",
                    Product_Image = "sodfransksennep.jpg",
                    Product_EAN_Code = "5702040711070",
                });
                products.Add(new Product()
                {
                    Product_Name = "Økologisk sød chilisauce",
                    Product_Brand = "Oriental chef",
                    Product_Ingredients_Text = "Sukker, vand,8% rød chili, ris-eddike, salt, hvidløg, tapiokastivelse, citronsyre, fortykningsmiddel :(E 415)",
                    Product_Image = "okologisksodchilisauce.jpg",
                    Product_EAN_Code = "5711988002133",
                });
                products.Add(new Product()
                {
                    Product_Name = "Snickers",
                    Product_Brand = "Mars",
                    Product_Ingredients_Text = "Mælkechokolade med hvid nougat (14%), blød karamel (27%) og ristede peanuts. Indeholder mindst 25% kakaotørstof.",
                    Product_Image = "snickers.jpg",
                    Product_EAN_Code = "5000159461122",
                });
                conn.InsertAll(products);
            }
        }

        public static Product GetProduct(string ean_code = "")
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LOCAL_DB_PATH))
            {
                return conn.FindWithQuery<Product>("select * from Product where Product_EAN_Code = ?", ean_code);
            }
        }
        public static void CreateFavoritesTable(string ean_code = "")
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LOCAL_DB_PATH))
            {
                //dont refill it if it already exists
                if (conn.GetTableInfo("FavoritedProduct").Count > 0)
                {
                    return;
                }
                conn.CreateTable<FavoritedProduct>();
            }
        }
        public static void AddFavoritedProduct(Product product)
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LOCAL_DB_PATH))
            {
                conn.Insert(new FavoritedProduct()
                {
                    Product_Name = product.Product_Name,
                    Product_Brand = product.Product_Brand,
                    Product_Ingredients_Text = product.Product_Ingredients_Text,
                    Product_Image = product.Product_Image,
                    Product_EAN_Code = product.Product_EAN_Code,
                });
            }
        }
        public static void RemoveFavoritedProduct(string ean_code)
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LOCAL_DB_PATH))
            {
                conn.Execute("delete from FavoritedProduct where Product_EAN_Code=?", ean_code);
            }
        }
        public static List<FavoritedProduct> GetFavoritedProducts()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LOCAL_DB_PATH))
            {
                return conn.CreateCommand("select * from FavoritedProduct").ExecuteDeferredQuery<FavoritedProduct>().ToList();
            }
        }
    }
}
