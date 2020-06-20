using Phonebook.Models;
using Xamarin.Forms;

namespace Phonebook.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string PicturePath { get; set; }


        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }

        private string description;
        public string Description 
        { 
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private ImageSource picture;
        public ImageSource Picture
        {
            get { return picture; }
            set 
            {
                if (value.IsEmpty)
                {
                    value = ImageSource.FromResource(
                        "Phonebook.Assets.Images.user-profile.png");
                }

                SetProperty(ref picture, value); 
            }
        }

        private Category category;
        public Category Category
        {
            get { return category; }
            set { SetProperty(ref category, value); }
        }
    }
}
