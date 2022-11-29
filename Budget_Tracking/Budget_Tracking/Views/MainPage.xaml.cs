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
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "*.expense.notes.txt");
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
            Expense_TrackingListView.ItemsSource = budgets.OrderByDescending(E => E.Date);

            var addbudgets = new List<Addbudget>();
            var budgetfiles = Directory.EnumerateFiles(
                Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData),
               "*.budget.notes.txt");
            foreach (var budgetfile in budgetfiles)
            {
                var addbudget = new Addbudget
                { Text = File.ReadAllText(budgetfile),
                  Date = File.GetCreationTime(budgetfile),
                  FileName = budgetfile,
                  Icon = "Resources/drawable/budgeticon.png"
                };
                addbudgets.Add(addbudget);
            }
            BudgetListView.ItemsSource = addbudgets;
            if (budgetfiles.Count() > 0)
            {
                BudgetButton.IsVisible = false;
                BudgetListView.IsVisible = true;
            }
            else
            {
                BudgetButton.IsVisible = true;
                BudgetListView.IsVisible = false;
            }
        }
        private async void Expense_TrackingListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new BudgetPage
            {
                BindingContext = (Budget)e.SelectedItem
            });
        }

        private async void BudgetListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new AddBudgetPage
            {
                BindingContext = (Addbudget)e.SelectedItem 
            });
        }

        private async void BudgetButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddBudgetPage());
        }
    }
}