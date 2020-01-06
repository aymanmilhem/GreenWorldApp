using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public List<Voucher> UserVoucherList { get; set; }

        public int AllUserCompletedTasksCount => AllUserCompletedTasks.Count;

        public int UserVoucherCount => UserVoucherList.Count;
        
        //addition
        public int CurrentUserId { get; set; }

        private string _dBPath =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        
        //addition
        public ProfilePage(int currentUserId)
        {
            //addition
            CurrentUserId = currentUserId;

            InitializeComponent();

            var db = new SQLiteConnection(_dBPath);
            //addition
            //AllUserCompletedTasks = db.Table<UserTask>().OrderBy(a => a.CompletedByUserId == CurrentUserId).ToList();
            AllUserCompletedTasks = db.Table<UserTask>().Where(x => x.CompletedByUserId == currentUserId).ToList();
            db.Table<UserTask>();
            db.CreateTable<Voucher>();
            UserVoucherList = db.Table<Voucher>().Where(x => x.OwnerId == CurrentUserId).ToList();
            //UserVoucherList = db.Table<Voucher>().OrderBy(x => x.OwnerId == CurrentUserId).ToList();
            var userTableList = db.Table<User>().Where(x => x.Id == currentUserId).ToList();
            var currentUserName = userTableList.FirstOrDefault().Email;
            DisplayAlert(null, $"Current UserName: {currentUserName}", "ok");
            //var currentUserName = firstElementName;
            NameLabel.Text = currentUserName;

            BindingContext = this;
        }

        private async void Button_Claim_Due_Vouchers(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dBPath);

            var maxPK = db.Table<UserTask>().OrderByDescending(a => a.CompletedByUserId == CurrentUserId).FirstOrDefault();
            //change 
            int tableCount = db.Table<UserTask>().OrderByDescending(a => a.CompletedByUserId == CurrentUserId).ToList()
                .Count();

            //int tableCount = db.Table<UserTask>().Count();
            int remainingTasks = tableCount % 10;
            int requiredIterations = tableCount - remainingTasks;
            int voucherTableIterations = (requiredIterations / 10);

            if (tableCount >= 10)
            {

                for (int i = 0; i < requiredIterations ; i++)
                {
                    db.Table<UserTask>();
                    var itemToDelete = db.Table<UserTask>().OrderBy(x => x.CompletedByUserId == CurrentUserId).First();
                    db.Table<UserTask>().Delete(x => x.Id == itemToDelete.Id);
                }

                db.Table<Voucher>();
                for (int i = 0; (i < voucherTableIterations); i++)
                {
                    Voucher voucher = new Voucher
                    {
                        Id = (maxPK == null ? 1 : maxPK.Id + 1),
                        OwnerId = CurrentUserId
                    };
                    db.Insert(voucher);
                    await DisplayAlert("Well done!", "You now have accrued " +db.Table<Voucher>().OrderBy(x => x.OwnerId == CurrentUserId).Count().ToString() + 
                        " vouchers which you can redeem anytime.", "OK");
                }
            }
            else if ((tableCount == 0) || (tableCount <10))
            {
                await DisplayAlert("Sorry",
                    "Not enough recorded tasks for a voucher.\nPlease try again later.", "Ok");
            }

            
            ListViewElement.ItemsSource = null;
            ListViewElement.ItemsSource = db.Table<UserTask>().OrderBy(x => x.CompletedByUserId == CurrentUserId);

            CompletedTasksCountLabel.Text = " ";
            db.Table<UserTask>();
            CompletedTasksCountLabel.Text = db.Table<UserTask>().OrderBy(x =>x.CompletedByUserId == CurrentUserId).Count().ToString();

            VoucherCountLabel.Text = " ";

            // different use *******//
            db.Table<Voucher>().OrderBy(x =>x.OwnerId == CurrentUserId);

            VoucherCountLabel.Text = db.Table<Voucher>().Count().ToString();
        }

        private async void Button_Browse_Tips_List(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void Button_Record_More_Tasks(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecordTaskPage());
        }

        protected override void OnAppearing()
        {
            var db = new SQLiteConnection(_dBPath);
            db.Table<UserTask>();
            ListViewElement.ItemsSource = null;
            ListViewElement.ItemsSource = db.Table<UserTask>().Where(x => x.CompletedByUserId == CurrentUserId);
            //ListViewElement.ItemsSource = db.Table<UserTask>().OrderBy(x => x.CompletedByUserId == CurrentUserId);
            db.Table<Voucher>();
            CompletedTasksCountLabel.Text = " ";
            db.Table<UserTask>();
            CompletedTasksCountLabel.Text = db.Table<UserTask>().Where(x => x.CompletedByUserId == CurrentUserId).Count().ToString();
            //CompletedTasksCountLabel.Text = db.Table<UserTask>().OrderBy(x => x.CompletedByUserId == CurrentUserId).Count().ToString();
            VoucherCountLabel.Text = " ";
            db.Table<Voucher>();
            //VoucherCountLabel.Text = db.Table<Voucher>().OrderBy(x => x.OwnerId == CurrentUserId).Count().ToString();
            VoucherCountLabel.Text = db.Table<Voucher>().Where(x => x.OwnerId == CurrentUserId).Count().ToString();
        }

        private async void Back_To_Main_Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainMenu());
        }
    }
}