using Phonebook.Data;
using Phonebook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phonebook.Views
{

    [DesignTimeVisible(false)]
    public partial class ContactsListPage : ContentPage
    {
        private Database db;
        private List<Contact> initialContacts;
        public ObservableCollection<Contact> Contacts { get; set; }
        public ContactsListPage()
        {
            InitializeComponent();
            db = new Database();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await LoadContacts();
            Contacts = new ObservableCollection<Contact>(initialContacts);
            contactsList.ItemsSource = Contacts;
        }

        private void contactsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Contact selectedContact = e.SelectedItem as Contact;
            Navigation.PushAsync(new ContactDetailsPage(selectedContact));
        }

        private async void DeleteButtonClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as Contact;

            bool deletionApproved = await DisplayAlert(
                "Alert",
                $"Do you really want to delete the contact with name {contact.Name}?",
                "Yes",
                "No");

            if (deletionApproved)
            {
                int deletedContacts = await db.DeleteItem(contact);
                if (deletedContacts > 0)
                {
                    initialContacts.Remove(contact);
                    Contacts.Remove(contact);
                }
                else
                {
                    await DisplayAlert(
                        "Deletion failed!",
                        $"The deletion of contact with name {contact.Name} failed.",
                        "Close");
                }
            }
        }

        private void EditButtonClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as Contact;

            //TODO: Decide whether to make all calls to Navigation.PushAsync awaited.
            Navigation.PushAsync(new ContactCreateEditPage(contact));
        }

        private void AddItemClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContactCreateEditPage(new Contact()));
        }
        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string value = e.NewTextValue;
            Search(value);
        }

        private async Task LoadContacts()
        {
            initialContacts = await db.GetItems<Contact>();
        }

        private void Search(string value)
        {
            Contacts = new ObservableCollection<Contact>(
                initialContacts.Where(
                        c => c.Name.ToLower().StartsWith(value.ToLower())
                    )
                );

            contactsList.ItemsSource = Contacts;
        }
    }
}
