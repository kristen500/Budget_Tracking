using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Budget_Tracking.Models;
using System.IO;

namespace Budget_Tracking.Views
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
            var budgets = new List<Budget>();
            var files = Directory.EnumerateFiles(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "*.notes.txt");
            foreach (var file in files)
            {
                var budget = new Budget
                {
                    Text = File.ReadAllText(file),
                    Date = File.GetCreationTime(file),
                    FileName = file
                };
                budgets.Add(budget);
            }
            Budget_TrackingListView.ItemsSource = budgets.OrderByDescending(E => E.Date);
        }
        private async void BudgetTrackingListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new BudgetPage
            {
                BindingContext = (Budget)e.SelectedItem
            });
        }
    }
}