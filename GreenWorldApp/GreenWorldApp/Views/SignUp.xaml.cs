using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenWorldApp.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenWorldApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        private Entry _emailEntry;
        private Entry _passwordEntry;
        private Entry _passwordVerificationEntry;
        private Button _signUpButton;
        private bool _isInTable = false;
        private bool _passwordMatches;

        private string dBPath =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myBD.db3");

        public SignUp()
        {
            InitializeComponent();
        }

        private async void Button_SignUp_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dBPath);
            db.CreateTable<User>();

            var maxPK = db.Table<User>().OrderByDescending(c => c.Id).FirstOrDefault();

            _signUpButton = sender as Button;
            StackLayout parentElement = _signUpButton.Parent as StackLayout;
            _emailEntry = parentElement.Children[1] as Entry;
            _passwordEntry = parentElement.Children[2] as Entry;
            _passwordVerificationEntry = parentElement.Children[3] as Entry;

            _passwordMatches = (_passwordEntry.Text == _passwordVerificationEntry.Text);

            List<User> listToCompare = db.Table<User>().OrderBy(x => x.Id).ToList();

            for (int i = 0; i < listToCompare.Count(); i++)
            {

                if ((listToCompare[i].Email) == (_emailEntry.Text))
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
                    Email = _emailEntry.Text,
                    Password = _passwordEntry.Text
                };
                db.Insert(user);
                await DisplayAlert("Success", "User with email: " + _emailEntry.Text + " has been added", "Back To Menu");
                await Navigation.PopAsync();
            }
        }
    }
}