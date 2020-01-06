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
        private bool _passwordMatches;
        private int _accountId;


        private string _dBPath =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myBD.db3");
        
        public LoginPage()
        {
            //App app = (App)Application.Current;
            //var currentUserId = app.CurrentUserId;

            InitializeComponent();
        }

        private async void Button_Login_OnClicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dBPath);
            db.Table<User>();

            //_loginButton = sender as Button;
            //Grid loginButtonParentElement = _loginButton.Parent as Grid;
            //Grid loginButtonGrandParentElement = loginButtonParentElement.Parent as Grid;
            
            //_emailEntry = loginButtonGrandParentElement.Children[0] as Entry;
            //_passwordEntry = loginButtonGrandParentElement.Children[1] as Entry;

            //_emailEntry.Text = UsernameEntry.Text;
            //_passwordEntry.Text = PasswordEntry.Text;

            App app = (App)Application.Current;
            //int currentUserId;
            //var currentUserId = app.CurrentUserId;
            //await Navigation.PushAsync(new ProfilePage(currentUserId: currentUserId));

            List<User> listToCompare = db.Table<User>().OrderBy(x => x.Email).ToList();

            //var personList = from p in db.Table<User>()
            //    where p.Email == UsernameEntry.Text
            //    select p;
            //var personInList  = personList.FirstOrDefault();


            foreach (var user in listToCompare)
            {
                if ((user.Email) == (UsernameEntry.Text))
                {
                    _accountExists = true;
                    _accountId = user.Id;
                }
            }

            foreach (var user in listToCompare)
            {
                if ((user.Id) == (_accountId))
                {
                    if (user.Password == PasswordEntry.Text)
                    {
                        _passwordMatches = true;
                    }
                }
            }

            if (_accountExists && _passwordMatches)
            {
                app.IsLoggedIn = true;
                app.CurrentUserId = _accountId;
                await Navigation.PushAsync(new ProfilePage(currentUserId: _accountId));
            }
            else
            {
                await DisplayAlert("Fail", "Wrong Credentials, please try again", "Ok");
            }
        }
    }
}