using System;
using SQLite;
using Phonebook.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Phonebook.Models;
using Xamarin.Forms.Internals;
using System.Collections.Generic;
using System.Threading.Tasks;
using Phonebook.Services.Interfaces;
using Phonebook.Services;

namespace Phonebook
{
    public partial class App : Application
    {
        private Database db;
        public App()
        {
            InitializeComponent();
            db = new Database();
            DependencyService.RegisterSingleton(db);
            DependencyService.Register<IPageService, PageService>();
            MainPage = new NavigationPage(new ContactsListPage());
        }

        private async Task InitializeDatabase()
        {
            await db.Initialize(new Type[] { typeof(Contact), typeof(Category) });

            var categoryList = new List<Category>
            {
                new Category() { Name= "Friends"},
                new Category() { Name= "Family"},
                new Category() { Name= "Colleagues"},
                new Category() { Name= "Other"}
            };

            await db.Seed(categoryList, categoryList.Count);
        }

        protected async override void OnStart()
        {
            await InitializeDatabase();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
