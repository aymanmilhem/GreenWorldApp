using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenWorldApp
{ 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public List<UserTask> AllUserCompletedTasks{ get; set; }

        public RecordTaskPage RecordTaskPage { get; set; }

        private RecordTaskPage _recordTaskPage;

        private string _dBPath =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        public ProfilePage()
        {
            InitializeComponent();


            var db = new SQLiteConnection(_dBPath);
            
            //List<UserTask> listToPopulateFrom = db.Table<UserTask>().OrderBy(x => x.Name).ToList();
            this.AllUserCompletedTasks = db.Table<UserTask>().OrderBy(a => a.Name).ToList();



            //for (int i = 0; i < listToPopulateFrom.Count; i++)
            //{
            //    UserTask currentItem = listToPopulateFrom[i];
            //    NameLabel.Text = currentItem.Name;
            //    CompletionDateLabel.Text = currentItem.CompletionDate.ToString();
            //}

            //CompletionDateLabel.Text = listToPopulateFrom[0].CompletionDate.ToString();
            //_recordTaskPage = new RecordTaskPage();
            //String extractedName = _recordTaskPage.FindByName<String>("Name");
            //CompletionDateLabel.Text = extractedName;

            BindingContext = this;
        }

        private async void Button_Delete_All_Tasks(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dBPath);

            var maxPK = db.Table<UserTask>().OrderByDescending(a=>a.Id).FirstOrDefault();

            if (maxPK.Id != 0)
            {
                for (int i = 0; i <= maxPK.Id; i++)
                {
                    db.Table<UserTask>().Delete(a =>a.Id == i);
                }
            }

            ListViewElement.ItemsSource = null;
            ListViewElement.ItemsSource = db.Table<UserTask>().OrderBy(x => x.Name);
        }
    }
}