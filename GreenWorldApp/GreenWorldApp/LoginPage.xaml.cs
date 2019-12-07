using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenWorldApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private Entry _emailEntry;
        private Entry _passwordEntry;
        private Button _loginButton;
        private bool _accountExists = false;
        private bool _passwordMathces;
        private int _accountId;


        private string _dBPath =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myBD.db3");

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Button_Login_OnClicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dBPath);
            db.CreateTable<User>();

            _loginButton = sender as Button;
            StackLayout loginButtonParentElement = _loginButton.Parent as StackLayout;
            _emailEntry = loginButtonParentElement.Children[1] as Entry;
            _passwordEntry = loginButtonParentElement.Children[2] as Entry;

            List<User> listToCompare = db.Table<User>().OrderBy(x => x.Email).ToList();

            foreach (var user in listToCompare)
            {
                if ((user.Email) == (_emailEntry.Text))
                {
                    _accountExists = true;
                    _accountId = user.Id;
                }
            }

            foreach (var user in listToCompare)
            {
                if ((user.Id) == (_accountId))
                {
                    if (user.Password == _passwordEntry.Text)
                    {
                        _passwordMathces = true;
                    }
                }
            }

            if (_accountExists && _passwordMathces)
            {
                await Navigation.PushAsync(new ProfilePage());
            }
            else
            {
                await DisplayAlert("Fail", "Wrong Credentials, please try again", "Ok");
            }



        }
    }
}