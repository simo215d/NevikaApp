using NevikaApp.Data;
using NevikaApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace NevikaApp
{
    public class SplashPage : ContentPage
    {
        Image splashImage;

        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();

            splashImage = new Image()
            {
                Source = "nevikaLogo.png",
                WidthRequest = 150,
                HeightRequest = 150
            };

            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);
            this.BackgroundColor = Color.FromHex("#ffffff"); // #429de3
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 2000);
            await splashImage.ScaleTo(0.9, 1500, Easing.Linear);
            //await splashImage.ScaleTo(150, 1200, Easing.Linear);
            await splashImage.FadeTo(0, 1000, Easing.Linear);
            /*if (hasAllergensBeenSelected() == true)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ActivateScan();
                });
            } else Application.Current.MainPage = new UserOptionsPage();*/
            Application.Current.MainPage = new UserOptionsPage();
        }

        private bool hasAllergensBeenSelected()
        {
            foreach (Allergen allergen in LocalDatabase.AllergensList)
            {
                if (allergen.Selected==true)
                {
                    return true;
                }
            }
            return false;
        }

        private void ActivateScan()
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
