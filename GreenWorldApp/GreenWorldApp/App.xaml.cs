using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenWorldApp
{
    public partial class App : Application
    {

        public bool IsLoggedIn { get; set; }
        public int CurrentUserId { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainMenu());
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
