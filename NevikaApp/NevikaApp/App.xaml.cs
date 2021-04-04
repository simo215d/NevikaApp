using NevikaApp.API;
using NevikaApp.Data;
using NevikaApp.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NevikaApp
{
    public partial class App : Application
    {
        public static Product ScannedProduct;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SplashPage());
        }

        public App(string _db_path)
        {
            InitializeComponent();

            // Initializations
            ApiHelper.InitializeClient();


            LocalDatabase.InitializeLocalDatabase(_db_path);

            MainPage = new NavigationPage(new SplashPage());
        }


       
       
        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
