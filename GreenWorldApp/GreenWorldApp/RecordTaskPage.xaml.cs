using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.XPath;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenWorldApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordTaskPage : ContentPage
    {
        public IList<UserTask> AllTasks { get; set; }

        private string _dBPath =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        public RecordTaskPage()
        {
            InitializeComponent();

            AllTasks= new List<UserTask>();

            AllTasks.Add(new UserTask
            {
                Name = "Eat Less Meat",
            });

            AllTasks.Add(new UserTask
            {
                Name = "Recycle More",
            });

            AllTasks.Add(new UserTask
            {
                Name = "Use Canvas Bags",
            });

            AllTasks.Add(new UserTask
            {
                Name = "Start Composting",
            });

            AllTasks.Add(new UserTask
            {
                Name = "Use LED Bulbs",
            });

            AllTasks.Add(new UserTask
            {
                Name = "Walk More",
            });

            AllTasks.Add(new UserTask
            {
                Name = "Give up Disposable Cups",
            });

            AllTasks.Add(new UserTask
            {
                Name = "Shop at Charity Shops",
            });
            AllTasks.Add(new UserTask
            {
                Name = "Have a Vegan Meal",
            });
            AllTasks.Add(new UserTask
            {
                Name = "Reduce Water Waste",


            });

            BindingContext = this;
        }

        
        private async void Button_Submit_Record_Tasks_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dBPath);
            db.CreateTable<UserTask>();

            var maxPK = db.Table<UserTask>().OrderByDescending(c => c.Id).FirstOrDefault();

            List<UserTask> selectedList = new List<UserTask>();

            for (int i = 0; i < AllTasks.Count; i++)
            {
                UserTask item = AllTasks[i];

                if (item.IsChecked == true)
                {
                    selectedList.Add(item);
                }
            }

            for (int i = 0; i < selectedList.Count; i++)
            {
                UserTask completedTask = new UserTask
                {
                    Name = selectedList[i].Name,
                    CompletionDate = DateTime.Today
                };
                db.Insert(completedTask);
            }
            await DisplayAlert(null, "In total, you have submitted " + selectedList.Count + " tasks today", "OK");
            await Navigation.PopAsync();


        }
    }
}