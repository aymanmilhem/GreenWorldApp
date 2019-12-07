using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RefreshView = Xamarin.Forms.PlatformConfiguration.WindowsSpecific.RefreshView;

namespace GreenWorldApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserList : ContentPage
    {
        private ListView _listView;
        private Button _deleteButton;
        private User _user;
        private string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myBD.db3");
        public UserList()
        {
            InitializeComponent();
            this.Title = "User List";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();
            _listView = new ListView();
            _listView.ItemsSource = db.Table<User>().OrderBy(x => x.Email).ToList();
            _listView.ItemSelected += User_List_Item_Selected;
            stackLayout.Children.Add(_listView);

            _deleteButton = new Button();
            _deleteButton.Text = "Delete";
            _deleteButton.IsEnabled = false;
            _deleteButton.Clicked += User_List_deleteButton_Clicked;
            stackLayout.Children.Add(_deleteButton);

            Content = stackLayout;
        }

        private async void User_List_deleteButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);

            _listView.BeginRefresh();
            db.Table<User>().Delete(x => x.Id == _user.Id);

            _deleteButton.IsEnabled = false;

            /*to get the listview to refresh after deletion, this is the way*/
            _listView.ItemsSource = null;
            _listView.ItemsSource = db.Table<User>().OrderBy(x => x.Email).ToList();

            /*this does it but it flashes the main page for half a second*/
            //Navigation.RemovePage(this);
            //await Navigation.PushAsync(new UserList());
            
            _listView.EndRefresh();
        }

        private void User_List_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            _deleteButton.IsEnabled = true;
            _user = e.SelectedItem as User;

            
        }
    }
}