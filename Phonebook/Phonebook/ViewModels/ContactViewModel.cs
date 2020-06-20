using Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Phonebook.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public ImageSource Picture { get; set; }

        public int CategoryId { get; set; }

        public Category SelectedCategory { get; set; }
    }
}
