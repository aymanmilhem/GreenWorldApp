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
    public partial class SignUp : ContentPage
    {
        private Entry _entry;
        private Entry _emailEntry;
        private Entry _passwordEntry;
        private Entry _passwordVerificationEntry;
        private Button _signUpButton;
        private bool _isInTable = false;
        private bool _passwordMatches;

        private string _dBPath =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myBD.db3");

        public SignUp()
        {
            InitializeComponent();
        }

        private async void Button_SignUp_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dBPath);
            db.CreateTable<User>();
            var userDbCount = db.Table<User>().Count();
            //await DisplayAlert(null, userDbCount.ToString(), "ok");

            var maxPK = db.Table<User>().OrderByDescending(c => c.Id).FirstOrDefault();

            _passwordMatches = (SignUpPasswordEntry.Text == SignUpPasswordVerificationEntry.Text);

            List<User> listToCompare = db.Table<User>().OrderBy(x => x.Id).ToList();

            if (userDbCount > 0)
            {

                for (int i = 0; i < listToCompare.Count(); i++)
                {
                    if ((listToCompare[i].Email) == (SignUpEmailEntry.Text))
                    {
                        _isInTable = true;
                    }
                }

                if (_isInTable)
                {
                    await DisplayAlert("Error",
                        "Cannot sign up at this time, please use a different email as a unique identifier", "Ok");
                }
                else if (!_passwordMatches)
                {
                    await DisplayAlert("Error", "Password does not match, please try again", "OK");
                }
                else
                {
                    User user = new User
                    {
                        Id = (maxPK == null ? 1 : maxPK.Id + 1),
                        Email = SignUpEmailEntry.Text,
                        Password = SignUpPasswordEntry.Text
                    };
                    db.Insert(user);
                    await DisplayAlert("Success", $"User: {SignUpEmailEntry.Text} added", "Back");
                    await Navigation.PopAsync();
                }
            }
            else
            {
                //await DisplayAlert(null, "Dropping Table", "OK");
                db.DropTable<User>();
                db.CreateTable<User>();
                //await DisplayAlert(null, "Table created", "OK");
                if (_passwordMatches)
                {
                    db.Table<User>();
                    User user = new User
                    {
                        Email = SignUpEmailEntry.Text,
                        Password = SignUpPasswordEntry.Text
                    };

                    db.Insert(user);
                    await DisplayAlert("Success", $"User: {SignUpEmailEntry.Text} added", "Back");
                }
                else
                {
                    await DisplayAlert("Error", "Password does not match, please try again", "OK");
                }
            }
            await Navigation.PopAsync();
        }
    }
}