using System;
using System.ComponentModel;
using Xamarin.Forms;
using Phonebook.Models;
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
            InitializeComponent();

            ViewModel = new ContactsListViewModel();
        }

        protected async override void OnAppearing()
        {
            await ViewModel.LoadData();
            base.OnAppearing();
        }

        private void contactsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedContact = e.SelectedItem as ContactViewModel;

            ViewModel.SelectCommand.Execute(selectedContact);
        }

        private void DeleteButtonClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as ContactViewModel;

            ViewModel.DeleteCommand.Execute(contact);
        }

        private void EditButtonClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as ContactViewModel;

            ViewModel.EditCommand.Execute(contact);
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string value = e.NewTextValue;

            ViewModel.SearchCommand.Execute(value);
        }
    }
}
