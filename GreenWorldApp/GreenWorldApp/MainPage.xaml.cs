using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenWorldApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public IList<Tip> Tips { get; private set; }

        public MainPage()
        {
            InitializeComponent();

            Tips = new List<Tip>();

            

            Tips.Add(new Tip
            {
                Title = "Eat Less Meat",
                Text = "Although it might sound unrelated, eating less meat can actually tremendously affect the environment, in a positive way. ",
                ImageUrl = "vegetables",
                LinkUrl = "https://www.theguardian.com/environment/2018/dec/21/lifestyle-change-eat-less-meat-climate-change"
            });

            Tips.Add(new Tip
            {
                Title = "Recycle More",
                Text = "\"The less paper you use, the less paper needs to be produced and the more trees that get to fill our forests.\"",
                ImageUrl = "trees",
                LinkUrl = "https://www.smallfootprintfamily.com/30-ways-to-use-less-paper"
            });

            Tips.Add(new Tip
            {
                Title = "Use Canvas Bags",
                Text = "A canvas bag is sturdier than a traditional plastic or paper bag and can hold more goods. A canvas bag can also be used to store items or pack items when moving – making it useful in more than one way. ",
                ImageUrl = "canvasBag",
                LinkUrl = "https://www.thewarehouse.co.nz/p/uniti-diy-canvas-shopping-bag-natural-30-x-40/R2554505.html?gclsrc=aw.ds&?ds_rl=1268368&gclid=Cj0KCQiAk7TuBRDQARIsAMRrfUZlidbSiCcxOEEJc7yT6bMdX3uZGrkV2S5b3fpIRZ9RgOVcJzL0XsMaAsctEALw_wcB&gclsrc=aw.ds"
            });

            Tips.Add(new Tip
            {
                Title = "Start Composting",
                Text = " Not only do compost bins reduce waste by letting you re - use things you would normally just toss out, but they also save you money and help your plants grow better in your garden!",
                ImageUrl = "compost",
                LinkUrl = "https://www.everydayhealth.com/healthy-home/eco-friendly/composting-101.aspx"
            });

            Tips.Add(new Tip
            {
                Title = "Use LED Bulbs",
                Text = "LED lights are up to 80% more efficient than traditional lighting such as fluorescent and incandescent lights. 95% of the energy in LEDs is converted into light and only 5% is wasted as heat. ",
                ImageUrl = "ledBulb",
                LinkUrl = "https://www.lighting.philips.co.nz/consumer/led-lights/energy-saving-led-light"
            });

            Tips.Add(new Tip
            {
                Title = "Walk More",
                Text = "By walking instead of riding, you help reduce the use of, and the therefore the harm of burning fossil fuels, which helps reduce the amount of green house gases, thus contributing to a smaller carbon foot print. ",
                ImageUrl = "walking",
                LinkUrl = "http://www.iowahealthieststate.com/blog/communities/economic--environmental-benefits-of-walking-and-walkable-communities/"
            });

            Tips.Add(new Tip
            {
                Title = "Give up Disposable Cups",
                Text = "Using a keep cup instead of disposable paper cups helps redcue paper consumption, thus relieveing the pressure on the demand for more tree bark material to produce them. Furthermore, keep cups are made of glass, which has a cleaner feel about it for drinking.",
                ImageUrl = "keepCup",
                LinkUrl = "https://www.mightyape.co.nz/product/keepcup-brew-cork-soft-charcoal-black-12oz-340ml/31125582?gclid=Cj0KCQiAk7TuBRDQARIsAMRrfUZ0Or3KCyrfId-bXgvDItV-0YphmXHWVMppmLLtR5cIiPiQIWwOy-8aAgtTEALw_wcB"
            });

            Tips.Add(new Tip
            {
                Title = "Shop at Charity Shops",
                Text = "Shopping at a charity shop instead of buying new items when possible is very helpful for the environment; it acts as a form of recycling, or reusing of the items, and the reveniew usually goes to support a good cause.",
                ImageUrl = "charityShop",
                LinkUrl = "https://yagoeco.com/blogs/news/environmental-impact-of-charity-shops"
            });
            Tips.Add(new Tip
            {
                Title = "Have a Vegan Meal",
                Text = "Vegan food is not only very healthy for you, but it is healthy for the entire planet as well!, eating vegetarian helps support the farmers and increased the demand for fresh produce from the local farmer markets, all while reducing the need for harmful dairy farming.",
                ImageUrl = "vegetarianMeal",
                LinkUrl = "https://www.tripadvisor.co.nz/Restaurant_Review-g255106-d725021-Reviews-Revive_Vegan_Cafe-Auckland_Central_North_Island.html"
            });
            Tips.Add(new Tip
            {
                Title = "Reduce Water Waste",
                Text = "You can live a more eco-friendly lifestyle by using less water and cutting back on your bottled water purchases. Take a shorter shower in the morning or install a low-flow showerhead. ",
                ImageUrl = "waterWashing",
                LinkUrl = "https://www.wheelsforwishes.org/news/live-a-more-eco-friendly-lifestyle/"
            });


            BindingContext = this;
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Tip selectedItem = e.SelectedItem as Tip;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Tip tappedItem = e.Item as Tip;
        }

        [Obsolete]
        void OnLinkButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            StackLayout parentItem = (StackLayout)button.Parent;
            Label label = (Label)parentItem.Children[0];
            Uri UrlToVisit = new Uri(label.Text);
            Device.OpenUri(UrlToVisit);

            //Tip myTip = new Tip();
            //Navigation.PushAsync(new ProfilePageNew());
            //await Launcher.TryOpenAsync(new Uri("https://www.google.com"));
            //Device.OpenUri(new Uri("www.google.com"));
            //string myUrl = (string)this.FindByName("LinkLabel");
            //DisplayAlert("found", myUrl, "KKK");
            //Device.OpenUri(new Uri(myUrl.ToString()));
        }

        void OnProfileButtonClicked(object sender, EventArgs e)
        {
            App app = (App)Application.Current;
            var currentUserId = app.CurrentUserId;
            Navigation.PushAsync(new ProfilePage(currentUserId));
        }

        void OnBottomBarListButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        void OnBottomBarRecordTaskButton(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecordTaskPage());
        }
    }
}
