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
            int submittedTasksCount = db.Table<UserTask>().Count();

            string submittedTasksQualifier = selectedList.Count == 1 ? "task" : "tasks";
            string existingTasksQualifier = submittedTasksCount == 1 ? "task" : "tasks";
            string remainingTasksQualifier = (10 - submittedTasksCount) == 1 ? "task" : "tasks";

            if (selectedList.Count == 0)
            {
                await DisplayAlert("Submission Failed", "No tasks selected, please check some boxes!", "ok");
            }

            else if (submittedTasksCount < 10)
            {
                
                await DisplayAlert(null, "You have just recorded " + selectedList.Count + " " +
                                         submittedTasksQualifier + "." + "\nYou now have accumulated " + submittedTasksCount +
                                         " "+ existingTasksQualifier + ".\n" + (10 - submittedTasksCount) + " " + remainingTasksQualifier +
                                         "  left to qualify for a voucher.", "OK");
            }
            else
            {
                await DisplayAlert("Congratulations!", "You have qualified for a voucher!", "Ok");
            }

            
            //Interesting how you can reach any page in the navigation stack in code!!
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            await Navigation.PushAsync(new RecordTaskPage());
            Navigation.RemovePage(currentPage);



        }

        private async void Profile_Button_OnClicked(object sender, EventArgs e)
        {
            App app = (App)Application.Current;
            var currentUserId = app.CurrentUserId;
            await Navigation.PushAsync(new ProfilePage(currentUserId));
        }

        private async void List_Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void BackToMainPageButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainMenu());
        }
    }
}