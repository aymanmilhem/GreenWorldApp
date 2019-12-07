﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenWorldApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordTaskPage : ContentPage
    {
        public IList<UserTask> AllTasks { get; set; }
        public RecordTaskPage()
        {
            InitializeComponent();

            AllTasks= new List<UserTask>();

            AllTasks.Add(new UserTask
            {
                Name = "Eat Less Meat",
                CompletionDate = new DateTime(2019, 11, 3)
            });

            AllTasks.Add(new UserTask
            {
                Name = "Recycle More",
                CompletionDate = new DateTime(2018, 3, 4)
            });

            AllTasks.Add(new UserTask
            {
                Name = "Use Canvas Bags",
            });

            AllTasks.Add(new UserTask
            {
                Name = "Start Composting",
                CompletionDate = new DateTime(2019, 1, 30)
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
    }
}