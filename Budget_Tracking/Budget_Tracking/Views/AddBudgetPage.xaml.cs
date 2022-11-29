using Budget_Tracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;


namespace Budget_Tracking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBudgetPage : ContentPage
    {
        public AddBudgetPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var addbudget = (Addbudget)BindingContext;
            if (addbudget != null && !string.IsNullOrEmpty(addbudget.FileName))
            {
                BudgetText.Text = File.ReadAllText(addbudget.FileName);
            }

        }
        private async void BudgetSave_Clicked(object sender, EventArgs e)
        {
            var addbudget = new Addbudget();
            addbudget.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
             $"{Path.GetRandomFileName()}.budget.notes.txt");

            File.WriteAllText(addbudget.FileName, BudgetText.Text);

            if (Navigation.ModalStack.Count > 0)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                Shell.Current.CurrentItem = (Shell.Current as AppShell).MainPageContent;
            }


        }

        private async void BudgetDelete_Clicked(object sender, EventArgs e)
        {
            
                var addbudget = (Addbudget)BindingContext;
                if (File.Exists(addbudget.FileName))
                {
                    File.Delete(addbudget.FileName);
                }
                BudgetText.Text = string.Empty;
                await Navigation.PopModalAsync();

            

        }
    }
}