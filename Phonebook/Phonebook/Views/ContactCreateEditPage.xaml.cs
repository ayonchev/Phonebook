using Phonebook.Data;
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
    public partial class ContactCreateEditPage : ContentPage
    {
        private Database db;
        public Contact Contact { get; set; }
        public ContactCreateEditPage(Contact contact)
        {
            InitializeComponent();

            db = new Database();

            Contact = contact;
            BindingContext = Contact;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            await db.SaveAsync(Contact);
            await Navigation.PopAsync();
        }
        //private void CancelButtonClicked(object sender, EventArgs e)
        //{

        //}
    }
}