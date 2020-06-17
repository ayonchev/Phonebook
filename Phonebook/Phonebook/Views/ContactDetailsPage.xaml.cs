using Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phonebook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetailsPage : ContentPage
    {
        public Contact Contact { get; set; }
        public ContactDetailsPage(Contact contact)
        {
            InitializeComponent();

            BindingContext = this.Contact = contact;


            DisplayAlert("Contact", $"{contact.Name} - {contact.PhoneNumber}", "OK");
        }
    }
}