using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Phonebook.Services
{
    public static class DataConstants
    {
        private const string DatabaseFilename = "phonebook.db3";
        public static string DatabasePath
        {
            get
            {
                return Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        DatabaseFilename
                    );
            }
        }
    }
}
