using GymCheckin.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GymCheckin
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Delay(0);
        }
    }
}
