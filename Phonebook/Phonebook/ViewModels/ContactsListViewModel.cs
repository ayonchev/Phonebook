using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Phonebook.Models;
using Phonebook.Views;

namespace Phonebook.ViewModels
{
    public class ContactsListViewModel : BaseViewModel
    {
        private List<Category> categories;
        private List<ContactViewModel> initialContacts;
        private ObservableCollection<ContactViewModel> contacts;
        public ObservableCollection<ContactViewModel> Contacts 
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

        public ContactsListViewModel()
        {
            SearchCommand = new Command<string>(Search);
            SelectCommand = new Command<ContactViewModel>(async c => await Select(c));
            AddCommand = new Command(async () => await Add());
            EditCommand = new Command<ContactViewModel>(async c => await Edit(c));
            DeleteCommand = new Command<ContactViewModel>(async c => await Delete(c));
        }

        public async Task LoadData()
        {
            categories = await Database.GetItems<Category>();
            var contacts = await Database.GetItems<Contact>();

            initialContacts = contacts.Select(c => new ContactViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                PhoneNumber = c.PhoneNumber,
                PicturePath = c.PicturePath,
                Picture = ImageSource.FromFile(c.PicturePath),
                Category = categories.FirstOrDefault(ct => ct.Id == c.CategoryId)
            }).ToList();

            Contacts = new ObservableCollection<ContactViewModel>(initialContacts);
        }

        private void Search(string value)
        {
            Contacts = new ObservableCollection<ContactViewModel>(
                initialContacts.Where(
                        c => c.Name.ToLower().StartsWith(value.ToLower())
                    )
                );
        }

        private async Task Select(ContactViewModel contact)
        {
            await PageService.PushAsync(new ContactDetailsPage(contact));
        }

        private async Task Add()
        {
            await PageService.PushAsync(new ContactCreateEditPage(new ContactViewModel(), categories));
        }

        private async Task Edit(ContactViewModel contact)
        {

            await PageService.PushAsync(new ContactCreateEditPage(contact, categories));
        }

        private async Task Delete(ContactViewModel contact)
        {
            bool deletionApproved = await PageService.DisplayAlert(
                "Alert",
                $"Do you really want to delete the contact with name {contact.Name}?",
                "Yes",
                "No");

            if (deletionApproved)
            {
                int deletedContacts = await Database.DeleteItem<Contact>(contact.Id);
                if (deletedContacts > 0)
                {
                    initialContacts.Remove(contact);
                    Contacts.Remove(contact);
                }
                else
                {
                    await PageService.DisplayAlert(
                        "Deletion failed!",
                        $"The deletion of contact with name {contact.Name} failed.",
                        "Close");
                }
            }
        }
    }
}
