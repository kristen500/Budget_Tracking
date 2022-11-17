using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Budget_Tracking.Models;
using System.IO;

namespace BudgetTracking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var BudgetTrackings = new List<Budget_Tracking>();
            var files = Directory.EnumerateFiles(Enviornment.GetFolderPath(Enviornment.SpecialFolder.LocalApplicationData), "*.notes.txt");
            foreach (var file in files)
            {
                var BudgetTracking = new BudgetTracking
                {
                    Text = file.ReadAllText(file),
                    Date = file.GetCreationTime(file),
                    Amount = file.GetAmount(file),
                    Expense = file.ReadAllText(file),
                    FileName = file
                };
                BudgetTracking.Add(BudgetTracking);
            }
            BudgetTrackingListView.ItemsSource = BudgetTracking.OrderByDescending(E => E.Date);
        }
        private async void BudgetTrackingListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new BudgetTrackingPage
            {
                BindingContext = (BudgetTracking)e.SelectedItem
            });
        }
    }
}