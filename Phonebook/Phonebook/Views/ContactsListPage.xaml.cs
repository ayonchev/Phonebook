using System;
using System.ComponentModel;
using Xamarin.Forms;
using Phonebook.Models;
using Phonebook.Services;
using Phonebook.Services.Interfaces;
using Phonebook.ViewModels;

namespace Phonebook.Views
{

    [DesignTimeVisible(false)]
    public partial class ContactsListPage : ContentPage
    {
        public ContactsListViewModel ViewModel 
        {
            get { return BindingContext as ContactsListViewModel; } 
            set { BindingContext = value; }
        }
        public ContactsListPage()
        {
            IPageService pageService = new PageService();
            ViewModel = new ContactsListViewModel(pageService);

            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await ViewModel.LoadData();
            base.OnAppearing();
        }

        private void contactsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Contact selectedContact = e.SelectedItem as Contact;

            ViewModel.SelectCommand.Execute(selectedContact);
        }

        private void DeleteButtonClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as Contact;

            ViewModel.DeleteCommand.Execute(contact);
        }

        private void EditButtonClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as Contact;

            ViewModel.EditCommand.Execute(contact);
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string value = e.NewTextValue;

            ViewModel.SearchCommand.Execute(value);
        }
    }
}
