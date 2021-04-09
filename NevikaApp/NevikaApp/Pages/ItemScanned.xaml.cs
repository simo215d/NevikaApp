using NevikaApp.API;
using NevikaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace NevikaApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemScanned : ContentPage
    {
        
        private string Scanned_EAN { get; set; }
        public string sourceImage;
        public Product product;

        public ItemScanned()
        {
            InitializeComponent();
        }

        private void ButtonSettingsMenu_Clicked(object sender, EventArgs e)
        {
            LocalDatabase.AddFavoritedProduct(product);
            Application.Current.MainPage = new FavoritesPage();
        }

        public ItemScanned(string ean_code)
        {
            Scanned_EAN = ean_code;

            // Next in line is OnAppearing()
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // First thing is hide these controls, so that we are only showing
            // the loading icon
            ProductIconSlot.IsVisible = false;
            ProductNameSlot.IsVisible = false;

            // Let's try and find the product
            GetProductInfo(Scanned_EAN);
        }

        /// <summary>
        /// Queries the API on nevika.taotek.dk for a product
        /// </summary>
        /// <param name="ean_code"></param>
        private void GetProductInfo(string ean_code)
        {
            // Try and get a product
            //Product product = await ProductProcessor.RequestProductInfo(ean_code);
            //my new local database method
            product = LocalDatabase.GetProduct(ean_code);

            if (product != null)
            {
                // If the product is found, stop the loading icon and activate the other slots.
                // All of this is temporary

                ProductIconSlot.IsVisible = true;
                ProductNameSlot.IsVisible = true;
                LoadingCircle.IsRunning = false;

                // ONLY FOR TESTING, need to implement the icons in the database first
                switch (ean_code)
                {
                    case "8710398169280":
                        ProductIconSlot.Source = "Cruesli.png";
                        break;

                    case "5000159461122":
                        ProductIconSlot.Source = "snickers.jpg";
                        break;

                    case "25065244":
                        ProductIconSlot.Source = "Milk.jpg";
                        break;

                    default:
                        ProductIconSlot.IsVisible = false;
                        break;
                }
                if (product.Product_Image != "")
                {
                    ProductIconSlot.IsVisible = true;
                    ProductIconSlot.Source = product.Product_Image;
                }

                // If successful, apply the product information on the page
                ApplyProduct(product);
            }
            else
            {
                // Else nope.
                // All of the frontend for this page is very temporary
                ProductIconSlot.IsVisible = true;
                ProductNameSlot.IsVisible = true;
                LoadingCircle.IsRunning = false;
                LoadingCircle.IsVisible = false;
                FavoritButton.IsVisible = false;
                Label_Allergens.IsVisible = false;
                ProductFoundAllergensSlot.IsVisible = false;
                Label_Allergens_Title.IsVisible = false;

                ChangeName("Varen findes ikke i databasen");
            }
        }

        private void ApplyProduct(Product product)
        {
            ChangeName(product.Product_Name);

            Console.WriteLine(product.Product_Ingredients_Text);

            if(product.Product_Ingredients_Text == "NULL" || product.Product_Ingredients_Text.Length <= 0)
            {
                ProductIngredientsSlot.Text = "";
                Label_Ingredients.Text = "Ingen ingredienser fundet";
            }
            else
            {
                Label_Ingredients.Text = "Yderligere ingredienser:";
                ProductIngredientsSlot.Text = product.Product_Ingredients_Text;
                
            }

            string allergensText = "";

            // Again temporary until we decide exactly how to do this. For now it just looks for a match in the 
            // ingredients text.
            foreach (Allergen al in LocalDatabase.AllergensList)
            {
                if(product.Product_Ingredients_Text.ToLower().Contains(al.DanishName.ToLower()) && al.Selected)
                {
                    if(allergensText != "")
                    {
                        allergensText += ", ";
                    }

                    allergensText += al.DanishName;
                }
            }

            // If it found something, the string is gonna be > 0
            if(allergensText.Length > 0)
            {
                //Label_Allergens.Text = "Allergener i varen:";
                ProductFoundAllergensSlot.Text = allergensText;
            }
            else
            {
                Label_Allergens.IsVisible = true;
                ProductFoundAllergensSlot.IsVisible = false;
                Label_Allergens.Text = "Ingen allergener fundet";
            }

            
        }

        private void ChangeName(string name)
        {
            ProductNameSlot.Text = name;
        }

        private void ButtonBack_Clicked(object sender, EventArgs e)
        {
            // Open a new page with the scanner. using ZXingNetMobile.
            var overlay = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            Frame frame = new Frame
            {
                BorderColor = Color.FromHex("#007AFF"),
                HasShadow = true,
                WidthRequest = 250,
                HeightRequest = 175,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
                //Content = new Label { Text = "Example" }
            };
            var top = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 60,
                BackgroundColor = Color.White
            };
            var topIcon = new Image
            {
                VerticalOptions = LayoutOptions.Center,
                Source = "nevikaLogo_small"
            };
            var buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 60,
                BackgroundColor = Color.White
            };
            var settingsBtn = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ImageSource = "profileicon",
                BackgroundColor = Color.White
            };
            var ideBtn = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ImageSource = "lightbulbicon",
                BackgroundColor = Color.White
            };
            var favoritesBtn = new Button
            {
                ImageSource = "star",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
            };
            //functions for buttons
            favoritesBtn.Clicked += favoritesBtn_Clicked;
            settingsBtn.Clicked += settingsBtn_Clicked;
            top.Children.Add(topIcon);
            buttons.Children.Add(favoritesBtn);
            buttons.Children.Add(ideBtn);
            buttons.Children.Add(settingsBtn);
            overlay.Children.Add(frame);
            overlay.Children.Add(buttons);
            overlay.Children.Add(top);
            ZXingScannerPage scanPage = new ZXingScannerPage(customOverlay: overlay);
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    // Once it finds a barcode, we're gonna open the ItemScanned page and give it
                    // the barcode it scanned. All processing is done there.
                    Application.Current.MainPage = new ItemScanned(result.Text);
                });
            };
            Application.Current.MainPage = scanPage;
        }
        private void favoritesBtn_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FavoritesPage();
        }

        private void settingsBtn_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new UserOptionsPage();
        }
    }
}