using Phonebook.Data;
using Phonebook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phonebook.Views
{
    public class ViewModel : INotifyPropertyChanged
    {
        private List<Category> categories;
        private Category selectedCategory;
        public Contact Contact { get; set; }
        public List<Category> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }
        public Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                Contact.CategoryId = selectedCategory.Id;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactCreateEditPage : ContentPage
    {
        private Database db;

        public ViewModel ViewModel { get; set; }
        public ContactCreateEditPage(Contact contact)
        {
            InitializeComponent();

            db = new Database();

            ViewModel = new ViewModel();
            ViewModel.Contact = contact;
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            ViewModel.Categories = await db.GetItems<Category>();
            ViewModel.SelectedCategory = 
                ViewModel.Categories.FirstOrDefault(c => c.Id == ViewModel.Contact.CategoryId) ?? 
                new Category();

            base.OnAppearing();
        }

        private async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            await db.SaveAsync(ViewModel.Contact);
            await Navigation.PopAsync();
        }
    }
}