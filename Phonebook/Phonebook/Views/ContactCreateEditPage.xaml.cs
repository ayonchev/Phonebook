using Phonebook.Data;
using Phonebook.Models;
using Phonebook.Services;
using Phonebook.Services.Interfaces;
using Phonebook.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
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