using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Budget_Tracking.Models;

namespace Budget_Tracking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetPage : ContentPage
    {
        public BudgetPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var budget = (Budget)BindingContext;
            if (budget !=null && !string.IsNullOrEmpty(budget.FileName))
            {
                BudgetTrackingText.Text = File.ReadAllText(budget.FileName);
            }
        }
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {

            var budget = (Budget)BindingContext;
            if (budget == null || string.IsNullOrEmpty(budget.FileName))
            {
                budget = new Budget();
                budget.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                $"{Path.GetRandomFileName()}.expense.notes.txt"); 
            }
            File.WriteAllText(budget.FileName, BudgetTrackingText.Text);
            if (Navigation.ModalStack.Count > 0)
            {
                await Navigation.PopModalAsync(); 
            }
            else
            {
                Shell.Current.CurrentItem = (Shell.Current as AppShell).MainPageContent;
            }

        }
        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var budget = (Budget)BindingContext;
            if (File.Exists(budget.FileName))
            {
                File.Delete(budget.FileName);
            }
            BudgetTrackingText.Text = string.Empty;
            await Navigation.PopModalAsync();
        }
    }
}