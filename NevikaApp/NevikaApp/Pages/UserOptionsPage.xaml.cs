using NevikaApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace NevikaApp.Pages
{
    public partial class UserOptionsPage : ContentPage
    {
        private ObservableCollection<GroupedAllergen> groupedAll { get; set; }
        private ObservableCollection<GroupedAllergen> groupedExpanded { get; set; }
        public UserOptionsPage()
        {
            InitializeComponent();
            SetAllergens();
            updateList();
        }

        protected override void OnAppearing()
        {
            //base.OnAppearing();
            //allergensListView.ItemsSource = LocalDatabase.AllergensList;
        }

        private void SetAllergens()
        {
            List<Allergen> allergens = LocalDatabase.AllergensList;
            groupedAll = new ObservableCollection<GroupedAllergen>();
            foreach (Allergen allergen in allergens)
            {
                if (allergen.IsCategory)
                {
                    GroupedAllergen group = new GroupedAllergen();
                    group.Expanded = true;
                    group.Selected = allergen.Selected;
                    group.GroupName = allergen.DanishName;
                    foreach (Allergen allergen2 in allergens)
                    {
                        if (allergen2.Category == group.GroupName && !allergen2.IsCategory)
                        {
                            group.Add(allergen2);
                        }
                    }
                    if (group.Count > 0)
                    {
                        group.HasChildren = true;
                    }
                    groupedAll.Add(group);
                }
            }
        }

        private void SyncAllergensWithDatabase()
        {
            //handle sub allergens
            foreach (GroupedAllergen list in groupedAll)
            {
                foreach (Allergen item in list)
                {
                    foreach (Allergen dbItem in LocalDatabase.AllergensList)
                    {
                        if (dbItem.Category == list.GroupName && dbItem.DanishName==item.DanishName && !dbItem.IsCategory)
                        {
                            item.Selected = dbItem.Selected;
                        }
                    }
                }
            }
            //handle categories
            foreach (GroupedAllergen list in groupedAll)
            {
                foreach (Allergen dbItem in LocalDatabase.AllergensList)
                {
                    if (dbItem.IsCategory && dbItem.DanishName == list.GroupName)
                    {
                        list.Selected = dbItem.Selected;
                    }
                }
            }
        }

        private void HeaderClicked(object sender, EventArgs e)
        {
            //marking the clicked to change between hidden and shown
            GroupedAllergen group = (GroupedAllergen)((Button)sender).CommandParameter;
            int selectedIndex = groupedExpanded.IndexOf(group);
            groupedAll[selectedIndex].Expanded = !groupedAll[selectedIndex].Expanded;
            //updated ui 
            updateList();
        }

        private void updateList()
        {
            groupedExpanded = new ObservableCollection<GroupedAllergen>();
            foreach (GroupedAllergen headerGroup in groupedAll)
            {
                GroupedAllergen newGroup = new GroupedAllergen { GroupName = headerGroup.GroupName, Expanded = headerGroup.Expanded, HasChildren = headerGroup.HasChildren, Selected = headerGroup.Selected };
                if (headerGroup.Expanded)
                {
                    foreach (Allergen item in headerGroup)
                    {
                        newGroup.Add(item);
                    }
                }
                groupedExpanded.Add(newGroup);
            }
            allergensListView.ItemsSource = groupedExpanded;
        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            bool Selected = false;
            String AllergenName = null;
            bool IsGroup = false;
            if (((Button)sender).BindingContext is Allergen)
            {
                Selected = ((Allergen)((Button)sender).BindingContext).Selected;
                AllergenName = ((Allergen)((Button)sender).BindingContext).DanishName;
            }
            if (((Button)sender).BindingContext is GroupedAllergen)
            {
                Selected = ((GroupedAllergen)((Button)sender).BindingContext).Selected;
                AllergenName = ((GroupedAllergen)((Button)sender).BindingContext).GroupName;
                IsGroup = true;
            }
            if(AllergenName != null && !IsGroup)
            {
                int index = LocalDatabase.AllergensList.FindIndex(al => al.DanishName == AllergenName);
                LocalDatabase.AllergensList[index].Selected = !Selected;

                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LocalDatabase.LOCAL_DB_PATH))
                {
                    conn.Update(LocalDatabase.AllergensList[index]);
                }
                //since database was manipulated we need to refresh our allergens list
                SyncAllergensWithDatabase();
                updateList();
            }
            if (AllergenName != null && IsGroup)
            {
                int index = LocalDatabase.AllergensList.FindIndex(al => al.DanishName == AllergenName);
                LocalDatabase.AllergensList[index].Selected = !Selected;
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LocalDatabase.LOCAL_DB_PATH))
                {
                    conn.Update(LocalDatabase.AllergensList[index]);
                }
                foreach (Allergen item in LocalDatabase.AllergensList)
                {
                    if (item.Category==AllergenName)
                    {
                        int index2 = LocalDatabase.AllergensList.FindIndex(al => al.DanishName == item.DanishName && al.Category==AllergenName);
                        LocalDatabase.AllergensList[index2].Selected = !Selected;
                        using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(LocalDatabase.LOCAL_DB_PATH))
                        {
                            conn.Update(LocalDatabase.AllergensList[index2]);
                        }
                    }
                }
                //since database was manipulated we need to refresh our allergens list
                SyncAllergensWithDatabase();
                updateList();
            }
        }

        private void OnSearchBarChange(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            SearchList(searchBar.Text);
        }

        private void SearchList(String SearchWord)
        {
            //recreate the list with allergens that contian the word
            groupedExpanded = new ObservableCollection<GroupedAllergen>();
            foreach (GroupedAllergen headerGroup in groupedAll)
            {
                GroupedAllergen newGroup = new GroupedAllergen { GroupName = headerGroup.GroupName, Expanded = headerGroup.Expanded, HasChildren = headerGroup.HasChildren, Selected = headerGroup.Selected };
                foreach (Allergen item in headerGroup)
                {
                    if (item.DanishName.ToLower().Contains(SearchWord.ToLower()))
                    {
                        newGroup.Add(item);
                    }
                }
                if (newGroup.Count>0 || newGroup.GroupName.ToLower().Contains(SearchWord.ToLower()))
                {
                    groupedExpanded.Add(newGroup);
                }
            }
            allergensListView.ItemsSource = groupedExpanded;
        }

        private void buttonVidere_Clicked(object sender, EventArgs e)
        {
            ActivateScan();
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
