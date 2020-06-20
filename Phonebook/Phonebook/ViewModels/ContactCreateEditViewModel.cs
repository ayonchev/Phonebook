using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.ViewModels
{
    public class ContactCreateEditViewModel : BaseViewModel
    {
        private Database db;

        public ContactViewModel Contact { get; set; }

        private List<Category> categories;
        public List<Category> Categories
        {
            get { return categories; }
            set { SetProperty(ref categories, value); }
        }

        public ICommand SelectPictureCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ContactCreateEditViewModel(ContactViewModel contact, List<Category> categories)
        {
            db = new Database();
            Categories = categories;
            Contact = contact;

            SelectPictureCommand = new Command(async () => await SelectPicture());
            SaveCommand = new Command(async () => await Save());
            CancelCommand = new Command(async () => await Cancel());
        }

        //Currently the method is not used because if the categories are loaded later than the contact, the contact, no item is selected initially in the category picker.
        public async Task LoadData()
        {
            Categories = await db.GetItems<Category>();
        }

        private async Task SelectPicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await PageService.DisplayAlert("Error", "Photo picking is not supported!", "Ok");
                return;
            }

            MediaFile picture = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { PhotoSize = PhotoSize.Medium });

            if (picture == null)
            {
                await PageService.DisplayAlert("Error", "No image is selected!", "Ok");
                return;
            }

            Contact.PicturePath = picture.Path;
            Contact.Picture = ImageSource.FromFile(Contact.PicturePath);
        }

        private async Task Save()
        {
            Contact contact = new Contact
            {
                Id = Contact.Id,
                Name = Contact.Name,
                Description = Contact.Description,
                CategoryId = Contact.Category.Id,
                PhoneNumber = Contact.PhoneNumber,
                PicturePath = Contact.PicturePath
            };

            await db.SaveAsync(contact);
            await PageService.PopAsync();
        }

        private async Task Cancel()
        {
            await PageService.PopAsync();
        }
    }
}
