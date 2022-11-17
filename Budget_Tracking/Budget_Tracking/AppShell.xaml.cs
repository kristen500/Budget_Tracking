using Budget_Tracking.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Budget_Tracking
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public ShellContent MainPageContent;
        public AppShell()
        {
            InitializeComponent();
            MainPageContent = Home;
        }
    }
}
