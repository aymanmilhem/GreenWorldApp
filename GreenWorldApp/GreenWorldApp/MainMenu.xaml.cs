using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenWorldApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        public MainMenu()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void MainPageLoginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void MainPageProfileButton_Clicked(object sender, EventArgs e)
        {
            App app = (App)Application.Current;
            var currentUserId = app.CurrentUserId;

            if (app.IsLoggedIn)
            {
                await Navigation.PushAsync(new ProfilePage(currentUserId: currentUserId));
            }
            else
            {
                await Navigation.PushAsync(new LoginPage());
            }
            
        }

        private async void MainPageRecordTasksButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecordTaskPage());
        }

        private async void MainPageBrowseAsGuestButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void MainPageSignUpButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

        private async void MainPageUserListButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserList());
        }
    }
}