using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Task = System.Threading.Tasks.Task;

namespace GreenWorldApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientsCompletedTaskList : ContentPage
    {
        private ListView _listView;

        private string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myBD.db3");

        public ClientsCompletedTaskList()
        {
            InitializeComponent();

            this.Title = "Completed Task List";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();
            _listView = new ListView();
            _listView.ItemsSource = db.Table<UserTask>().OrderBy(x => x.Name).ToList();

            stackLayout.Children.Add(_listView);

            Content = stackLayout;
        }
    }
}