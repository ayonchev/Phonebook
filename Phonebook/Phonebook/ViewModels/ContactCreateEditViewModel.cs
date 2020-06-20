using Phonebook.Data;
using Phonebook.Models;
using Phonebook.Services.Interfaces;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phonebook.ViewModels
{
    public class ContactCreateEditViewModel : BaseViewModel
    {
        private Database db;
        private IPageService pageService;

        public Contact Contact { get; set; }

        private List<Category> categories;
        public List<Category> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                SetProperty(ref categories, value);
            }
        }

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                SetProperty(ref selectedCategory, value);
                Contact.CategoryId = selectedCategory.Id;
            }
        }

        public ICommand SelectPictureCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ContactCreateEditViewModel(IPageService pageService, Contact contact)
        {
            db = new Database();
            Contact = contact;
            this.pageService = pageService;

            SelectPictureCommand = new Command(async () => await SelectPicture());
            SaveCommand = new Command(async () => await Save());
            CancelCommand = new Command(async () => await Cancel());
        }

        public async Task LoadData()
        {
            Categories = await db.GetItems<Category>();
            SelectedCategory =
                Categories.FirstOrDefault(c => c.Id == Contact.CategoryId) ??
                new Category();
        }

        private async Task SelectPicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await pageService.DisplayAlert("Error", "Photo picking is not supported!", "Ok");
                return;
            }

            MediaFile picture = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { PhotoSize = PhotoSize.Medium });

            if (picture == null)
            {
                await pageService.DisplayAlert("Error", "No image is selected!", "Ok");
                return;
            }

            //selectedImage.Source = ImageSource.FromFile(picture.Path);
        }

        private async Task Save()
        {
            await db.SaveAsync(Contact);
            await pageService.PopAsync();
        }

        private async Task Cancel()
        {
            await pageService.PopAsync();
        }
    }
}
