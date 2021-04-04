using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace NevikaApp.Droid
{
    [Activity(Label = "NevikaApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);


            /*
            string fileName = "books_db.sqlite";
            string fileLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string full_path = Path.Combine(fileLocation, fileName);
            */

            // DATABASE
            string fileName = "nevika_db.sqlite";
            string fileLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string full_path = Path.Combine(fileLocation, fileName);


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //Xamarin.Essentials.Platform.Init(Application);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

           
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(full_path));
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}