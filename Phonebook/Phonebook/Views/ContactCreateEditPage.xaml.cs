using Phonebook.Models;
using Phonebook.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phonebook.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactCreateEditPage : ContentPage
    {
        public ContactCreateEditViewModel ViewModel 
        { 
            get { return BindingContext as ContactCreateEditViewModel; }
            set { BindingContext = value; }
        }
        public ContactCreateEditPage(ContactViewModel contact, List<Category> categories)
        {
            InitializeComponent();
            ViewModel = new ContactCreateEditViewModel(contact, categories);
        }
    }
}