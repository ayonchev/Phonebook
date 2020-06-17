using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Phonebook.Data
{
    public static class Constants
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
