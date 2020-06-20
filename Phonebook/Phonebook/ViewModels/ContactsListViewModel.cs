using Phonebook.Data;
using Phonebook.Models;
using Phonebook.Services.Interfaces;
using Phonebook.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phonebook.ViewModels
{
    public class ContactsListViewModel : BaseViewModel
    {
        private Database db;
        private IPageService pageService;

        private List<Contact> initialContacts;
        private ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Contacts 
        {
            get
            {
                return contacts;
            }
            set
            {
                SetProperty(ref contacts, value);
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand SelectCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ContactsListViewModel(IPageService pageService)
        {
            this.db = new Database();
            this.pageService = pageService;

            SearchCommand = new Command<string>(Search);
            SelectCommand = new Command<Contact>(async c => await Select(c));
            AddCommand = new Command(async () => await Add());
            EditCommand = new Command<Contact>(async c => await Edit(c));
            DeleteCommand = new Command<Contact>(async c => await Delete(c));
        }

        public async Task LoadData()
        {
            initialContacts = await db.GetItems<Contact>();
            Contacts = new ObservableCollection<Contact>(initialContacts);
        }

        private void Search(string value)
        {
            Contacts = new ObservableCollection<Contact>(
                initialContacts.Where(
                        c => c.Name.ToLower().StartsWith(value.ToLower())
                    )
                );
        }

        private async Task Select(Contact contact)
        {
            await pageService.PushAsync(new ContactDetailsPage(contact));
        }

        private async Task Add()
        {
            int i = 1;
            await pageService.PushAsync(new ContactCreateEditPage(new Contact()));
        }

        private async Task Edit(Contact contact)
        {
            await pageService.PushAsync(new ContactCreateEditPage(contact));
        }

        private async Task Delete(Contact contact)
        {
            bool deletionApproved = await pageService.DisplayAlert(
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
                    await pageService.DisplayAlert(
                        "Deletion failed!",
                        $"The deletion of contact with name {contact.Name} failed.",
                        "Close");
                }
            }
        }
    }
}
