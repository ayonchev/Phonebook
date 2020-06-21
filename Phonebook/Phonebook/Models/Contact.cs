using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Models
{
    public class Contact : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public string PicturePath { get; set; }

        [Indexed]
        public int CategoryId { get; set; }
    }
}
