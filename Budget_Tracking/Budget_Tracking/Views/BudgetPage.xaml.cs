using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Budget_Tracking;

namespace BudgetTracking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetPage : ContentPage
    {
        public BudgetPage()
        {
            InitializeComponent();
        }
        protected override void Onappearing()
        {
            var Budgetracking = (BudgetTracking)BindingContext;
            if (!string.IsNullOrEmpty(BudgetTracking.FileName))
            {
                BudgetTracking.Text = File.ReadAllText(BudgetTracking.FileName);
            }
        }
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {

            todo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                $"{Path.GetRandomFileName().notes.txt}");
            filename.WriteAllText(filename, BudgetTrackingText.Text);
            if (Navigation.ModalStack.Count > 0)
            {
                await Navigation.PopModalAsync();
            }
            else
            {

                AppShell.Current.CurrentItem = (AppShell.Current as AppShell).MainPageContent;

            }
        }
        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var BudgetTracking = (BudgetTracking)BindingContext;
            if (File.Exists(BudgetTracking.FileName))
            {
                File.Delete(BudgetTracking.FileName);
            }
            BudgetTracking.Text = string.Empty;
            await Navigation.PopAsync();
        }
    }
}