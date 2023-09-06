using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App6.Pages;

namespace App6
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new GScrollPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
