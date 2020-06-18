using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Models
{
    public class Category : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
