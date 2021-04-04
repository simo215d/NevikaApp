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
    public partial class FavoritesPage : ContentPage
    {
        public FavoritesPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            productsListView.ItemsSource = LocalDatabase.GetFavoritedProducts();
        }

        private void OnSearchBarChange(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            SearchList(searchBar.Text);
        }

        private void SearchList(String SearchWord)
        {
            //recreate the list with allergens that contian the word
            List<FavoritedProduct> favoritedProducts = new List<FavoritedProduct>();
            foreach (FavoritedProduct item in LocalDatabase.GetFavoritedProducts())
            {
                if (item.Product_Brand.ToLower().Contains(SearchWord) || item.Product_Ingredients_Text.ToLower().Contains(SearchWord) || item.Product_Name.ToLower().Contains(SearchWord))
                {
                    favoritedProducts.Add(item);
                }
            }
            productsListView.ItemsSource = favoritedProducts;
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
        private void ButtonFjern_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var item = (FavoritedProduct)button.CommandParameter;
            LocalDatabase.RemoveFavoritedProduct(item.Product_EAN_Code);
            productsListView.ItemsSource = null;
            productsListView.ItemsSource = LocalDatabase.GetFavoritedProducts();
        }
    }
}