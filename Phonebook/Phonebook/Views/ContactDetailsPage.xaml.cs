using Phonebook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phonebook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetailsPage : ContentPage
    {
        public ContactViewModel Contact { get; set; }
        public ContactDetailsPage(ContactViewModel contact)
        {
            InitializeComponent();

            BindingContext = this.Contact = contact;
        }
    }
}